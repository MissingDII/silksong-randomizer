using HarmonyLib;
using HutongGames.PlayMaker;
using System;
using System.Linq;

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
            if (fsmInstance.Name != "Dialogue" || playMakerFsm.gameObject.name != "Crest Upgrade Shrine")
                return;

            var currentState = fsmInstance.ActiveStateName ?? "";

            BasePatch.Logger.LogInfo($"[FSM State Change] Crest Upgrade Shrine Dialogue: {currentState}");
            HandleEvaUpgradeInteraction(currentState);
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
            return state is "Meet Dlg" or "Repeat Dlg" or "Start Talk";
        }

        private static int CountUnlockedCrestSlots()
        {
            return ToolItemManager.GetAllCrests()
                .Where(crest => crest.name != "Hunter" && crest.name != "Hunter_v2" && crest.name != "Hunter_v3")
                .SelectMany(crest => crest.SaveData.Slots)
                .Count(slot => slot.IsUnlocked);
        }
    }
}



