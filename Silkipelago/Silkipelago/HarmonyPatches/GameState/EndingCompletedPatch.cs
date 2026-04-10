using HarmonyLib;
using HutongGames.PlayMaker.Actions;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using Silkipelago.Archipelago.SlotData;
using Silkipelago.Constants;

namespace Silkipelago.HarmonyPatches.GameState
{
    [HarmonyPatch(typeof(SetEndingCompleted), nameof(SetEndingCompleted.OnEnter))]
    public class EndingCompletedPatch
    {
        static bool Prefix(SetEndingCompleted __instance)
        {
            return BasePatch.SafeExecute(() => HandleEndingCompleted(__instance), nameof(EndingCompletedPatch), nameof(Prefix));
        }

        private static bool HandleEndingCompleted(SetEndingCompleted __instance)
        {
            if (__instance?.EndingType?.Value == null)
                return MethodPrefix.RUN_ORIGINAL_METHOD;

            var state = (SaveSlotCompletionIcons.CompletionState)__instance.EndingType.Value;
            var archipelagoClient = ArchipelagoPlugin.App.ArchipelagoClient;
            var goal = archipelagoClient.SlotData.Goal;

            switch (state)
            {
                case SaveSlotCompletionIcons.CompletionState.Act2Regular:
                case SaveSlotCompletionIcons.CompletionState.Act2Cursed:
                    HandleAct2RegularOrCursed(goal, archipelagoClient);
                    break;

                case SaveSlotCompletionIcons.CompletionState.Act2SoulSnare:
                    HandleAct2SoulSnare(goal, archipelagoClient);
                    break;

                case SaveSlotCompletionIcons.CompletionState.Act3Ending:
                    if (goal == Goal.LostLace)
                        archipelagoClient.ReportGoalCompletion();
                    break;
            }

            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }

        private static void HandleAct2RegularOrCursed(Goal goal, ArchipelagoClient archipelagoClient)
        {
            if (goal == Goal.GrandMotherSilk)
            {
                archipelagoClient.ReportGoalCompletion();
                return;
            }

            if (goal == Goal.LostLace)
            {
                var name = ArchipelagoLocationIds.GetArchipelagoName(
                    SaveSlotCompletionIcons.CompletionState.Act2Regular.ToString());

                ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(name);
            }
        }

        private static void HandleAct2SoulSnare(Goal goal, ArchipelagoClient archipelagoClient)
        {
            if (goal == Goal.GrandMotherSilk || goal == Goal.SnaredGrandMotherSilk)
            {
                archipelagoClient.ReportGoalCompletion();
            }
            if (goal == Goal.LostLace)
            {
                var name = ArchipelagoLocationIds.GetArchipelagoName(
                    SaveSlotCompletionIcons.CompletionState.Act2SoulSnare.ToString());

                ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(name);
            }
        }
    }
}
