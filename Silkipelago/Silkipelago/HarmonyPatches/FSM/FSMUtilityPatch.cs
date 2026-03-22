using HarmonyLib;
using HutongGames.PlayMaker;
using Silkipelago.Constants.FSM;
using System;

namespace Silkipelago.HarmonyPatches.FSM
{
    [HarmonyPatch(typeof(Fsm))]
    [HarmonyPatch(nameof(Fsm.Update))]
    public static class FSMUtilityPatch
    {
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

            // Only track Crest Upgrade Shrine Dialogue FSM
            if (fsmInstance.Name != EvaDialogueConstants.DialogueFsmName ||
                playMakerFsm.gameObject.name != EvaDialogueConstants.OwnerName)
                return;

            var currentState = fsmInstance.ActiveStateName ?? "";
            if (currentState != "Pause" && currentState != "Idle")
            {
                BasePatch.Logger.LogInfo($"[FSM State Change] Crest Upgrade Shrine Dialogue: {currentState}");
                HandleEvaUpgradeInteraction(currentState);
            }
        }

        private static void HandleEvaUpgradeInteraction(string currentState)
        {
            if (!IsDialogueInteractionState(currentState))
                return;

            BasePatch.Logger.LogInfo("[FSM Hook] Crest Upgrade Shrine dialogue interaction detected!");

            var unlockedSlots = CountUnlockedCrestSlots();

            for (var i = 0; i <= unlockedSlots; i++)
            {
                ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation($"{Silkipelago.Constants.LocationConstants.EvaSlotLocationPrefix}{i} Slots");
            }
        }

        private static bool IsDialogueInteractionState(string state)
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



