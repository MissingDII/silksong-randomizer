using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using QuestPlaymakerActions;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Quest
{
    [HarmonyPatch(typeof(GetQuestRewardV2))]
    [HarmonyPatch(nameof(GetQuestRewardV2.DoQuestAction))]
    public class GetQuestReward2Patch
    {
        public static bool Prefix(GetQuestRewardV2 __instance, FullQuestBase quest)
        {
            return BasePatch.SafeExecute(() => HandleGetQuestReward(__instance, quest), nameof(GetQuestReward2Patch), nameof(Prefix));
        }

        private static bool HandleGetQuestReward(GetQuestRewardV2 __instance, FullQuestBase quest)
        {
            BasePatch.Logger.LogInfo($"[Quest] get reward called for: {quest.name}");
            if (QuestIds.ALL_QUESTS.Contains(quest.name))
            {
                var archipelagoLocationId = ArchipelagoLocationIds.GetArchipelagoName(quest.name);
                var locationChecker = ArchipelagoPlugin.App.LocationChecker;
                if (locationChecker.LocationExists(archipelagoLocationId))
                {
                    __instance.StoreReward.Value = null;
                    __instance.StoreAmount.Value = 0;
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }
            }
            return MethodPrefix.RUN_ORIGINAL_METHOD; // Continue with original method
        }
    }
}
