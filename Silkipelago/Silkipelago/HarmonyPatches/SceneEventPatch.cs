using Silkipelago.Archipelago.ItemHandlers;
using Silkipelago.Constants;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Silkipelago.HarmonyPatches
{
    public static class SceneEventPatch
    {
        public static void addSceneEvent()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            BasePatch.SafeExecuteVoid(() => HandleSceneLoaded(scene), nameof(SceneEventPatch), nameof(OnSceneLoaded));
        }

        private static void HandleSceneLoaded(Scene scene)
        {
            if (scene.name == "Coral_Judge_Arena")
            {
                HandleCoralJudgeArena();
            }
        }

        private static void HandleCoralJudgeArena()
        {
            var bellcount = ArchipelagoPlugin.App.ArchipelagoClient.GetReceivedItemCount("Grand Gate Bell");
            var i = 1;
            foreach (var shrineName in PlayerDataIds.SHRINES)
            {
                if (i <= bellcount)
                {
                    SilksongItemManager.ItemToReceive++;
                    PlayerData.instance.SetBool(shrineName, true);
                }
                else
                {
                    PlayerData.instance.SetBool(shrineName, false);
                }
                i++;
            }
        }
    }
}
