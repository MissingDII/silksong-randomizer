using Silkipelago.Constants;
using Silkipelago.Items;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using ILogger = KaitoKid.Utilities.Interfaces.ILogger;

namespace Silkipelago.HarmonyPatches
{
    public static class SceneEventPatch
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

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
                _logger?.LogErrorException(nameof(SceneEventPatch), nameof(OnSceneLoaded), ex);
            }
        }

        private static void HandleCoralJudgeArena()
        {
            var bellcount = ArchipelagoPlugin.App.ArchipelagoClient.GetReceivedItemCount("Grand Gate Bell");
            var i = 1;
            foreach (var shrineName in PlayerDataStrings.SHRINES)
            {
                if (i <= bellcount)
                {
                    SilksongItemManager._itemToReceive++;
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
                _logger?.LogInfo("[Scene Event] Weave_10 loaded, setting up Crest Upgrade Shrine location tracking...");

                // Find the "Crest Upgrade Shrine" GameObject in the scene
                var gameObject = GameObject.Find("Crest Upgrade Shrine");

                if (gameObject == null)
                {
                    _logger?.LogInfo("[Scene Event] Could not find 'Crest Upgrade Shrine' GameObject");
                    return;
                }

                // Get the PlayMakerFSM component named "Dialogue"
                var dialogueFsm = FSMUtility.LocateFSM(gameObject, "Dialogue");

                if (dialogueFsm == null)
                {
                    _logger?.LogInfo("[Scene Event] Could not find 'Dialogue' FSM on Crest Upgrade Shrine");
                    return;
                }

                _logger?.LogInfo("[Scene Event] Found Dialogue FSM - Crest Upgrade Shrine location tracking will be handled by FsmUpdatePatch");
            }
            catch (Exception ex)
            {
                _logger?.LogErrorException(nameof(SceneEventPatch), nameof(HandleWeave10), ex);
            }
        }
    }
}
