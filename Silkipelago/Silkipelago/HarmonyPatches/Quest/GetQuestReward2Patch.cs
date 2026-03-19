using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using QuestPlaymakerActions;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Quest
{
    [HarmonyPatch(typeof(GetQuestRewardV2))]
    [HarmonyPatch(nameof(GetQuestRewardV2.DoQuestAction))]
    public class GetQuestReward2Patch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        public static bool Prefix(GetQuestRewardV2 __instance, FullQuestBase quest)
        {
            try
            {
                Logger.LogInfo($"[Quest] get reward called for: {quest.name}");
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
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(QuestManagerSilentPatch), nameof(Prefix), ex);
                return true; // Continue with original method on error
            }
        }
    }
}
