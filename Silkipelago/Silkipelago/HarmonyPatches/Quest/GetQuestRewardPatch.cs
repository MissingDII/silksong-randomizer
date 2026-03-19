using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using QuestPlaymakerActions;
using Silkipelago.Constants;

namespace Silkipelago.HarmonyPatches.Quest
{
    /// <summary>
    /// Prevents reward display for randomized quests.
    /// </summary>
    [HarmonyPatch(typeof(GetQuestReward))]
    [HarmonyPatch(nameof(GetQuestReward.DoQuestAction))]
    public class GetQuestRewardPatch
    {
        /// <summary>
        /// Prefix that blocks rewards if quest is randomized in Archipelago.
        /// </summary>
        public static bool Prefix(GetQuestReward __instance, FullQuestBase quest)
        {
            return BasePatch.SafeExecute(
                () => HandleQuestReward(__instance, quest),
                nameof(GetQuestRewardPatch),
                nameof(Prefix)
            );
        }

        private static bool HandleQuestReward(GetQuestReward instance, FullQuestBase quest)
        {
            BasePatch.Logger.LogInfo($"[Quest] get reward called for: {quest.name}");
            if (QuestIds.ALL_QUESTS.Contains(quest.name))
            {
                var archipelagoLocationId = ArchipelagoLocationIds.GetArchipelagoName(quest.name);
                var locationChecker = ArchipelagoPlugin.App.LocationChecker;
                if (locationChecker.LocationExists(archipelagoLocationId))
                {
                    instance.StoreReward = null;
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }
            }
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }
}
