using HarmonyLib;
using HutongGames.PlayMaker.Actions;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.GameState
{
    [HarmonyPatch(typeof(SetEndingCompleted), nameof(SetEndingCompleted.OnEnter))]
    public class EndingCompletedPatch
    {
        private static ILogger _logger;
        private static SilksongArchipelagoClient _archipelagoClient;
        private static SilksongLocationChecker _locationChecker;

        public static void Initialize(
            ILogger logger,
            SilksongArchipelagoClient client,
            SilksongLocationChecker locationChecker)
        {
            _logger = logger;
            _archipelagoClient = client;
            _locationChecker = locationChecker;
        }

        static bool Prefix(SetEndingCompleted __instance)
        {
            try
            {
                if (__instance?.EndingType?.Value == null)
                    return MethodPrefix.RUN_ORIGINAL_METHOD;

                var state = (SaveSlotCompletionIcons.CompletionState)__instance.EndingType.Value;
                var goal = _archipelagoClient.SlotData.Goal;

                switch (state)
                {
                    case SaveSlotCompletionIcons.CompletionState.Act2Regular:
                    case SaveSlotCompletionIcons.CompletionState.Act2Cursed:
                        HandleAct2RegularOrCursed(goal);
                        break;

                    case SaveSlotCompletionIcons.CompletionState.Act2SoulSnare:
                        HandleAct2SoulSnare(goal);
                        break;

                    case SaveSlotCompletionIcons.CompletionState.Act3Ending:
                        if (goal == Goal.LostLace)
                            _archipelagoClient.ReportGoalCompletion();
                        break;
                }

                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(nameof(EndingCompletedPatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }

        private static void HandleAct2RegularOrCursed(Goal goal)
        {
            if (goal == Goal.GrandMotherSilk)
            {
                _archipelagoClient.ReportGoalCompletion();
                return;
            }

            if (goal == Goal.LostLace)
            {
                var name = ArchipelagoLocationIds.GetArchipelagoName(
                    SaveSlotCompletionIcons.CompletionState.Act2Regular.ToString());

                _locationChecker.AddCheckedLocation(name);
            }
        }

        private static void HandleAct2SoulSnare(Goal goal)
        {
            if (goal == Goal.GrandMotherSilk || goal == Goal.SnaredGrandMotherSilk)
            {
                _archipelagoClient.ReportGoalCompletion();
            }
        }
    }


}
