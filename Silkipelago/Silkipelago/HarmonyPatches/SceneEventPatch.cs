using Silkipelago.Archipelago.ItemHandlers;
using Silkipelago.Constants;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using ILogger = KaitoKid.Utilities.Interfaces.ILogger;

namespace Silkipelago.HarmonyPatches
{
    public static class SceneEventPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        public static void addSceneEvent()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            try
            {
                if (scene.name == "Coral_Judge_Arena")
                {
                    HandleCoralJudgeArena();
                }
                else if (scene.name == "Weave_10")
                {
                    HandleWeave10();
                }
            }
            catch (Exception ex)
            {
                Logger?.LogErrorException(nameof(SceneEventPatch), nameof(OnSceneLoaded), ex);
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

        private static void HandleWeave10()
        {
            try
            {
                Logger?.LogInfo("[Scene Event] Weave_10 loaded, setting up Crest Upgrade Shrine location tracking...");

                // Find the "Crest Upgrade Shrine" GameObject in the scene
                var gameObject = GameObject.Find("Crest Upgrade Shrine");

                if (gameObject == null)
                {
                    Logger?.LogInfo("[Scene Event] Could not find 'Crest Upgrade Shrine' GameObject");
                    return;
                }

                // Get the PlayMakerFSM component named "Dialogue"
                var dialogueFsm = FSMUtility.LocateFSM(gameObject, "Dialogue");

                if (dialogueFsm == null)
                {
                    Logger?.LogInfo("[Scene Event] Could not find 'Dialogue' FSM on Crest Upgrade Shrine");
                    return;
                }

                Logger?.LogInfo("[Scene Event] Found Dialogue FSM - Crest Upgrade Shrine location tracking will be handled by FsmUpdatePatch");
            }
            catch (Exception ex)
            {
                Logger?.LogErrorException(nameof(SceneEventPatch), nameof(HandleWeave10), ex);
            }
        }
    }
}
