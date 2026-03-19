using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using QuestPlaymakerActions;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Quest
{
    [HarmonyPatch(typeof(GetQuestReward))]
    [HarmonyPatch(nameof(GetQuestReward.DoQuestAction))]
    public class GetQuestRewardPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        public static bool Prefix(GetQuestReward __instance, FullQuestBase quest)
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
                        __instance.StoreReward = null;
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
