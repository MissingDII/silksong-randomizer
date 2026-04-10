using HarmonyLib;
using HutongGames.PlayMaker;
using Silkipelago.Constants;
using Silkipelago.Constants.FSM;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Silkipelago.HarmonyPatches.FSM
{
    [HarmonyPatch(typeof(Fsm))]
    [HarmonyPatch(nameof(Fsm.Update))]
    public static class FSMUtilityPatch
    {
        // Dictionary to track all states for each FSM GameObject
        private static Dictionary<string, HashSet<string>> fsmStates = new Dictionary<string, HashSet<string>>();

        public static void Postfix(Fsm __instance)
        {
            BasePatch.SafeExecuteVoid(() => HandleFsmUpdate(__instance), nameof(FSMUtilityPatch), nameof(Postfix));
        }

        private static void HandleFsmUpdate(Fsm fsmInstance)
        {
            if (fsmInstance == null)
                return;

            // Get the PlayMakerFSM component that owns this Fsm
            var playMakerFsm = fsmInstance.Owner as PlayMakerFSM;
            if (playMakerFsm == null || playMakerFsm.gameObject == null)
                return;

            var currentScene = SceneManager.GetActiveScene().name;

            // Route to appropriate handler based on scene and FSM characteristics
            if (IsEvaDialogueFsm(fsmInstance, playMakerFsm) && currentScene == SceneNames.Weave_10)
            {
                //HandleEvaFsmUpdate(fsmInstance);
            }
            if (currentScene.Equals(SceneNames.Tut_01) && (playMakerFsm.gameObject.name.ToLower().Contains("vine") || fsmInstance.Name.ToLower().Equals("control")))
            {
                if (!playMakerFsm.gameObject.name.Equals("Tutorial Intro Cutscene"))
                {
                    HandleTutVineClusterFsm(fsmInstance);
                }
            }
        }

        private static void HandleTutVineClusterFsm(Fsm fsmInstance)
        {
            var playMakerFsm = fsmInstance.Owner as PlayMakerFSM;
            if (playMakerFsm == null)
                return;

            var gameObjectName = playMakerFsm.gameObject.name;
            var currentState = fsmInstance.ActiveStateName ?? "";

            // Track this state for this FSM
            if (!fsmStates.ContainsKey(gameObjectName))
            {
                fsmStates[gameObjectName] = new HashSet<string>();
                BasePatch.Logger.LogInfo($"[FSM Tracking] Started tracking FSM states for: {gameObjectName}");
            }

            // Add the current state if we haven't seen it before
            if (fsmStates[gameObjectName].Add(currentState))
            {
                BasePatch.Logger.LogInfo($"[FSM State] Found new state '{currentState}' for {gameObjectName}");
                BasePatch.Logger.LogInfo($"[FSM States] {gameObjectName} states: {string.Join(", ", fsmStates[gameObjectName])}");
            }

            // Handle the End state as a location check
            if (currentState.Equals("End"))
            {
                BasePatch.Logger.LogInfo($"[FSM State Change] Vine Cluster Destroyed: {gameObjectName}");
                var archipelagoId = ArchipelagoLocationIds.GetArchipelagoName(gameObjectName);
                if (!string.IsNullOrEmpty(archipelagoId))
                {
                    BasePatch.Logger.LogInfo($"[FSM Location Check] {gameObjectName} -> {archipelagoId}");
                    ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(archipelagoId);
                }
                else
                {
                    BasePatch.Logger.LogWarning($"[FSM Error] No location ID found for: {gameObjectName}");
                }
            }
        }

        /// <summary>
        /// Checks if this FSM is the Eva dialogue FSM
        /// </summary>
        private static bool IsEvaDialogueFsm(Fsm fsmInstance, PlayMakerFSM playMakerFsm)
        {
            return fsmInstance.Name == EvaDialogueConstants.DialogueFsmName &&
                   playMakerFsm.gameObject.name == EvaDialogueConstants.OwnerName;
        }

        /// <summary>
        /// Handles Eva's dialogue FSM updates for the Crest Upgrade Shrine (Weave_10)
        /// </summary>
        private static void HandleEvaFsmUpdate(Fsm fsmInstance)
        {
            var currentState = fsmInstance.ActiveStateName ?? "";
            if (currentState != "Pause" && currentState != "Idle")
            {
                BasePatch.Logger.LogInfo($"[FSM State Change] Crest Upgrade Shrine Dialogue {currentState}");
                HandleEvaUpgradeInteraction(currentState);
            }
        }

        private static void HandleEvaUpgradeInteraction(string currentState)
        {
            if (!IsEvaDialogueInteractionState(currentState))
                return;

            BasePatch.Logger.LogInfo("[FSM Hook] Crest Upgrade Shrine dialogue interaction detected!");

            var unlockedSlots = CountUnlockedCrestSlots();

            for (var i = 0; i <= unlockedSlots; i++)
            {
                ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation($"{LocationConstants.EvaSlotLocationPrefix}{i} Slots");
            }
        }

        private static bool IsEvaDialogueInteractionState(string state)
        {
            return state is EvaDialogueConstants.MeetDlgState or
                          EvaDialogueConstants.RepeatDlgState or
                          EvaDialogueConstants.GetUpgradePointsState or
                          EvaDialogueConstants.UpgradeSlot1PreDlgName or
                          EvaDialogueConstants.CrestUpgrade1dlg;
        }

        private static int CountUnlockedCrestSlots()
        {
            try
            {
                var unlockedCount = 0;
                var toolCrestList = ToolItemManager.GetAllCrests();

                if (toolCrestList == null)
                    return 0;

                foreach (var crest in toolCrestList)
                {
                    // Match base game logic: filter hidden, non-base versions, and upgraded crests
                    if (crest.IsHidden || !crest.IsBaseVersion || crest.IsUpgradedVersionUnlocked)
                        continue;

                    // Only count if crest is unlocked
                    if (!crest.IsUnlocked)
                        continue;

                    // Count unlocked slots for this crest
                    var slots = crest.Slots;
                    var saveData = crest.SaveData;

                    if (slots == null || saveData.Slots == null)
                        continue;

                    for (var i = 0; i < slots.Length; i++)
                    {
                        // Increment if slot is not locked OR if savedata slot is unlocked
                        if (!slots[i].IsLocked || (i < saveData.Slots.Count && saveData.Slots[i].IsUnlocked))
                            unlockedCount++;
                    }
                }

                return unlockedCount;
            }
            catch (Exception ex)
            {
                BasePatch.Logger.LogErrorException(nameof(FSMUtilityPatch), nameof(CountUnlockedCrestSlots), ex);
                return 0;
            }
        }
    }
}



