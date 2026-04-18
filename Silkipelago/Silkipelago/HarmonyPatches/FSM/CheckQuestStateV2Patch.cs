using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using QuestPlaymakerActions;
using Silkipelago.Constants;

namespace Silkipelago.HarmonyPatches.FSM
{
    [HarmonyPatch(typeof(CheckQuestStateV2))]
    [HarmonyPatch(nameof(CheckQuestStateV2.DoQuestAction))]
    public class CheckQuestStateV2Patch
    {
        public static bool Prefix(CheckQuestStateV2 __instance, FullQuestBase quest)
        {
            return BasePatch.SafeExecute(() => HandleDoQuest(__instance, quest), nameof(CheckQuestStateV2Patch), nameof(Prefix));
        }

        private static bool HandleDoQuest(CheckQuestStateV2 instance, FullQuestBase questBase)
        {
            if (questBase.name.Equals(QuestIds.SAVE_COURIER_SHORT))
            {
                var saveData = ArchipelagoPlugin.App.SettingsContext.saveSettingsData;
                if (saveData.Tipp)
                {
                    instance.Fsm.Event(instance.CompletedEvent);
                }
                else
                {
                    instance.Fsm.Event(instance.IncompleteEvent);
                }
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }
}
