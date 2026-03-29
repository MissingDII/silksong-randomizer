using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using ILogger = KaitoKid.Utilities.Interfaces.ILogger;

namespace Silkipelago.HarmonyPatches.Scenes
{
    /// <summary>
    /// Hooks into GameManager.BeginScene which is called after a scene is fully loaded and activated.
    /// This runs AFTER the scene load is complete and all scene objects are initialized.
    /// </summary>
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch(nameof(GameManager.BeginScene))]
    public static class SceneLoadedPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        public static void Postfix(GameManager __instance)
        {
            BasePatch.SafeExecuteVoid(() => HandleSceneLoaded(__instance), nameof(SceneLoadedPatch), nameof(Postfix));
        }

        private static void HandleSceneLoaded(GameManager gameManager)
        {
            var sceneName = SceneManager.GetActiveScene().name;
            Logger.LogInfo($"[SceneLoadedPatch] Scene fully loaded and initialized: {sceneName}");

            // Add your post-load logic here
            if (SceneNames.Bone_East_08.Equals(sceneName))
            {
                HandleBoneEast08();
            }
            // This executes AFTER the scene is fully initialized and active
        }

        private static void HandleBoneEast08()
        {
            Logger.LogInfo("[SceneLoadedPatch] Handling Bone_East_08 setup");

            var scene = SceneManager.GetActiveScene();
            var rootObjects = scene.GetRootGameObjects();

            // Find and modify ALL instances of the two Song Golem Floor objects
            ModifyAllSongGolemFloors(rootObjects, "Song Golem Floor (8)");
            ModifyAllSongGolemFloors(rootObjects, "Song Golem Floor (9)");
        }

        private static void ModifyAllSongGolemFloors(GameObject[] rootObjects, string objectName)
        {
            var foundInstances = new System.Collections.Generic.List<GameObject>();

            // Search through all root objects and collect ALL matching instances
            foreach (var root in rootObjects)
            {
                FindAllGameObjectsByName(root, objectName, foundInstances);
            }

            if (foundInstances.Count == 0)
            {
                Logger.LogWarning($"[SceneLoadedPatch] Could not find any instances of {objectName} in scene");
                return;
            }

            Logger.LogInfo($"[SceneLoadedPatch] Found {foundInstances.Count} instance(s) of {objectName}");

            // Modify each instance
            foreach (var target in foundInstances)
            {
                ModifySongGolemFloor(target, objectName);
            }
        }

        private static void ModifySongGolemFloor(GameObject target, string objectName)
        {
            Logger.LogInfo($"[SceneLoadedPatch] Processing {objectName}");

            // Remove the PlayMakerFSM component called "control" (case-insensitive)
            var fsm = target.GetComponent<PlayMakerFSM>();
            if (fsm != null && fsm.FsmName.Equals("control", System.StringComparison.OrdinalIgnoreCase))
            {
                Logger.LogInfo($"[SceneLoadedPatch] Removing PlayMakerFSM 'control' from {objectName}");
                Object.Destroy(fsm);
            }
            else if (fsm != null)
            {
                Logger.LogWarning($"[SceneLoadedPatch] Found PlayMakerFSM but with different name: {fsm.FsmName}");
            }
            else
            {
                Logger.LogWarning($"[SceneLoadedPatch] No PlayMakerFSM found on {objectName}");
            }

            // Set ActiveSelf to true if not already
            if (!target.activeSelf)
            {
                Logger.LogInfo($"[SceneLoadedPatch] Activating {objectName}");
                target.SetActive(true);
            }
            else
            {
                Logger.LogInfo($"[SceneLoadedPatch] {objectName} already active");
            }
        }

        private static GameObject FindGameObjectByName(GameObject parent, string name)
        {
            if (parent.name == name)
                return parent;

            foreach (Transform child in parent.transform)
            {
                var result = FindGameObjectByName(child.gameObject, name);
                if (result != null)
                    return result;
            }

            return null;
        }

        private static void FindAllGameObjectsByName(GameObject parent, string name, System.Collections.Generic.List<GameObject> results)
        {
            if (parent.name == name)
            {
                results.Add(parent);
            }

            foreach (Transform child in parent.transform)
            {
                FindAllGameObjectsByName(child.gameObject, name, results);
            }
        }
    }
}

