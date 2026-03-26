using BepInEx;
using GlobalEnums;
using Newtonsoft.Json;
using Silkipelago.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Silkipelago.Utils
{
    public static class SceneHealthManagerScanner
    {
        private static KaitoKid.Utilities.Interfaces.ILogger Logger => ArchipelagoPlugin.App.Logger;
        private static bool _sceneLoadingComplete = false;

        /// <summary>
        /// Scans all scenes and logs HealthManager instances found in each scene.
        /// Uses GameManager.BeginSceneTransition for proper Silksong scene loading.
        /// Must be called as a coroutine.
        /// Automatically hooks/unhooks GameManager.OnFinishedSceneTransition event.
        /// </summary>
        public static IEnumerator ScanAllScenesForHealthManagers()
        {
            Logger.LogInfo("[SceneHealthManagerScanner] Starting scan of all scenes...");

            // Hook the event
            if (GameManager.instance != null)
            {
                GameManager.instance.OnFinishedSceneTransition += OnSceneLoadComplete;
            }

            var originalScene = SceneManager.GetActiveScene().name;
            var sceneHealthCounts = new Dictionary<string, List<string>>();

            try
            {
                foreach (var sceneName in SceneNames.AllScenes)
                {
                    Logger.LogInfo($"[SceneHealthManagerScanner] Loading scene: {sceneName}");
                    yield return ProcessSceneLoad(sceneName, sceneHealthCounts);
                    yield return new WaitForSeconds(0.2f);
                }

                // Return to original scene
                Logger.LogInfo($"[SceneHealthManagerScanner] Returning to original scene: {originalScene}");
                _sceneLoadingComplete = false;

                GameManager.instance.BeginSceneTransition(new GameManager.SceneLoadInfo
                {
                    SceneName = originalScene,
                    EntryGateName = "",
                    HeroLeaveDirection = GatePosition.left,
                    EntryDelay = 0,
                    WaitForSceneTransitionCameraFade = true,
                    PreventCameraFadeOut = false,
                    Visualization = GameManager.SceneLoadVisualizations.Default,
                    AlwaysUnloadUnusedAssets = false,
                    ForceWaitFetch = false
                });

                var returnTimeout = 0f;
                while (!_sceneLoadingComplete && returnTimeout < 30f)
                {
                    returnTimeout += Time.deltaTime;
                    yield return null;
                }

                // Log summary
                Logger.LogInfo("[SceneHealthManagerScanner] === SCAN COMPLETE ===");
                Logger.LogInfo($"[SceneHealthManagerScanner] Scenes with HealthManagers: {sceneHealthCounts.Count}");
                foreach (var kvp in sceneHealthCounts)
                {
                    Logger.LogInfo($"  {kvp.Key}: {kvp.Value.Count} manager(s)");
                }

                // Export to JSON
                ExportResultsToJson(sceneHealthCounts);
            }
            finally
            {
                // Always unhook the event, even if an exception occurs
                if (GameManager.instance != null)
                {
                    GameManager.instance.OnFinishedSceneTransition -= OnSceneLoadComplete;
                    Logger.LogInfo("[SceneHealthManagerScanner] Event unhooked");
                }
            }
        }

        /// <summary>
        /// Processes loading a single scene and collecting HealthManager data.
        /// Handles errors during scene initialization gracefully.
        /// </summary>
        private static IEnumerator ProcessSceneLoad(string sceneName, Dictionary<string, List<string>> sceneHealthCounts)
        {
            var sceneProcessed = false;
            try
            {
                // Load scene through GameManager with error suppression
                _sceneLoadingComplete = false;

                GameManager.instance.BeginSceneTransition(new GameManager.SceneLoadInfo
                {
                    SceneName = sceneName,
                    EntryGateName = "",
                    HeroLeaveDirection = GatePosition.left,
                    EntryDelay = 0,
                    WaitForSceneTransitionCameraFade = true,
                    PreventCameraFadeOut = false,
                    Visualization = GameManager.SceneLoadVisualizations.Default,
                    AlwaysUnloadUnusedAssets = false,
                    ForceWaitFetch = false
                });
                sceneProcessed = true;
            }
            catch (Exception sceneLoadEx)
            {
                Logger.LogInfo($"[SceneHealthManagerScanner] Error loading scene {sceneName}: {sceneLoadEx.Message}");
            }

            if (sceneProcessed)
            {
                // Wait for scene to load with timeout
                var timeout = 0f;
                var maxTimeout = 30f;
                while (!_sceneLoadingComplete && timeout < maxTimeout)
                {
                    timeout += Time.deltaTime;
                    yield return null;
                }

                if (timeout >= maxTimeout)
                {
                    Logger.LogInfo($"[SceneHealthManagerScanner] Timeout loading {sceneName} - scene may not exist");
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    try
                    {
                        // Verify the scene actually loaded by checking if it's valid and matches the scene we requested
                        var scene = SceneManager.GetActiveScene();
                        if (scene.IsValid() && scene.name == sceneName)
                        {
                            // Collect health managers from the currently active scene
                            var healthManagers = GetHealthManagersInScene(scene);

                            if (healthManagers.Count > 0)
                            {
                                sceneHealthCounts[sceneName] = healthManagers;
                                Logger.LogInfo($"[SceneHealthManagerScanner] Found {healthManagers.Count} HealthManager(s) in {sceneName}:");
                                foreach (var managerName in healthManagers)
                                {
                                    Logger.LogInfo($"  - {managerName}");
                                }
                            }
                            else
                            {
                                Logger.LogInfo($"[SceneHealthManagerScanner] No HealthManagers found in {sceneName}");
                            }
                        }
                        else
                        {
                            Logger.LogInfo($"[SceneHealthManagerScanner] Scene {sceneName} failed to load or is not the active scene");
                        }
                    }
                    catch (Exception sceneIterationEx)
                    {
                        Logger.LogInfo($"[SceneHealthManagerScanner] Exception processing scene {sceneName}: {sceneIterationEx.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// Hook this to GameManager's scene transition complete event.
        /// Call this when scene loading is finished.
        /// </summary>
        public static void OnSceneLoadComplete()
        {
            _sceneLoadingComplete = true;
        }

        /// <summary>
        /// Exports the health manager scan results to a JSON file.
        /// </summary>
        private static void ExportResultsToJson(Dictionary<string, List<string>> sceneHealthCounts)
        {
            try
            {
                var json = JsonConvert.SerializeObject(sceneHealthCounts, Formatting.Indented);
                var filePath = Path.Combine(Paths.PluginPath, "health_managers.json");
                File.WriteAllText(filePath, json);
                Logger.LogInfo($"[SceneHealthManagerScanner] Health manager data exported to: {filePath}");
            }
            catch (Exception ex)
            {
                Logger.LogInfo($"[SceneHealthManagerScanner] Failed to export JSON: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets all HealthManager instances in a specific scene.
        /// </summary>
        private static List<string> GetHealthManagersInScene(Scene scene)
        {
            var managerNames = new List<string>();

            if (!scene.IsValid())
                return managerNames;

            var rootObjects = scene.GetRootGameObjects();
            foreach (var root in rootObjects)
            {
                var healthManagers = root.GetComponentsInChildren<HealthManager>(true);
                foreach (var manager in healthManagers)
                {
                    managerNames.Add(manager.gameObject.name);
                }
            }

            return managerNames;
        }

        /// <summary>
        /// Alternative: Find all HealthManager instances currently in memory (without loading scenes).
        /// Useful for quick checks of already-loaded content.
        /// </summary>
        public static void LogHealthManagersInMemory()
        {
            Logger.LogInfo("[SceneHealthManagerScanner] Logging all HealthManagers in memory...");

            var allManagers = Resources.FindObjectsOfTypeAll<HealthManager>();
            var groupedByScene = new Dictionary<string, List<string>>();

            foreach (var manager in allManagers)
            {
                if (manager.gameObject.scene.IsValid())
                {
                    var sceneName = manager.gameObject.scene.name;
                    if (!groupedByScene.ContainsKey(sceneName))
                    {
                        groupedByScene[sceneName] = new List<string>();
                    }
                    groupedByScene[sceneName].Add(manager.gameObject.name);
                }
            }

            Logger.LogInfo($"[SceneHealthManagerScanner] Found HealthManagers in {groupedByScene.Count} scene(s):");
            foreach (var kvp in groupedByScene)
            {
                Logger.LogInfo($"  {kvp.Key}: {kvp.Value.Count} manager(s)");
                foreach (var managerName in kvp.Value)
                {
                    Logger.LogInfo($"    - {managerName}");
                }
            }
        }
    }
}
