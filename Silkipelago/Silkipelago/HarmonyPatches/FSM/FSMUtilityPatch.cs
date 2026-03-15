using HarmonyLib;
using HutongGames.PlayMaker;
using System;
using System.Linq;
using ILogger = KaitoKid.Utilities.Interfaces.ILogger;

namespace Silkipelago.HarmonyPatches.FSM
{
    [HarmonyPatch(typeof(Fsm))]
    [HarmonyPatch(nameof(Fsm.Update))]
    public static class FSMUtilityPatch
    {
        public static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        public static void Postfix(Fsm __instance)
        {
            try
            {
                if (__instance == null)
                    return;

                // Get the PlayMakerFSM component that owns this Fsm
                var playMakerFsm = __instance.Owner as PlayMakerFSM;
                if (playMakerFsm == null || playMakerFsm.gameObject == null)
                    return;

                // Only track Crest Upgrade Shrine Dialogue FSM
                if (__instance.Name != "Dialogue" || playMakerFsm.gameObject.name != "Crest Upgrade Shrine")
                    return;

                var currentState = __instance.ActiveStateName ?? "";


                FSMUtilityPatch._logger?.LogInfo($"[FSM State Change] Crest Upgrade Shrine Dialogue: {currentState}");
                handleEva(currentState);
            }
            catch (Exception ex)
            {
                FSMUtilityPatch._logger?.LogErrorException(nameof(FSMUtilityPatch), nameof(Postfix), ex);
            }
        }

        private static void handleEva(string currentState)
        {
            if (!IsDialogueInteractionState(currentState))
                return;

            _logger?.LogInfo("[FSM Hook] Crest Upgrade Shrine dialogue interaction detected!");

            var unlockedSlots = CountUnlockedCrestSlots();

            for (var i = 0; i <= unlockedSlots; i++)
            {
                ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation($"Eva: {i} Slots");
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



