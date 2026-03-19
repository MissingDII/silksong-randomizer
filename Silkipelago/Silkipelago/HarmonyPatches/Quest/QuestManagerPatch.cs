using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Quest
{
    [HarmonyPatch(typeof(FullQuestBase))]
    [HarmonyPatch(nameof(FullQuestBase.TryEndQuest))]
    public static class QuestManagerPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        public static bool Prefix(FullQuestBase __instance, Action afterPrompt, bool consumeCurrency, bool forceEnd = false, bool showPrompt = true)
        {
            try
            {
                if (__instance.CanComplete)
                {
                    Logger.LogInfo($"[Quest] TryEndQuest called for: {__instance.name}");
                    if (QuestIds.ALL_QUESTS.Contains(__instance.name))
                    {
                        __instance.rewardItem = null;
                        var locationId = ArchipelagoLocationIds.GetArchipelagoName(__instance.name);
                        ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(locationId);
                    }
                }
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(QuestManagerSilentPatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }

    [HarmonyPatch(typeof(FullQuestBase))]
    [HarmonyPatch(nameof(FullQuestBase.SilentlyComplete))]
    public static class QuestManagerSilentPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        public static bool Prefix(FullQuestBase __instance)
        {
            try
            {
                Logger.LogInfo($"[Quest] SilentlyComplete called for: {__instance.name}");
                if (QuestIds.ALL_QUESTS.Contains(__instance.name))
                {
                    var locationId = ArchipelagoLocationIds.GetArchipelagoName(__instance.name);
                    if (locationId == null)
                    {
                        Logger.LogWarning($"this quest is not mapped to a location but exists in AllQuest {__instance.name}, please report a bug");
                    }
                    else
                    {
                        __instance.rewardItem = null;
                        ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(locationId);
                    }
                }
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(QuestManagerSilentPatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }

    [HarmonyPatch(typeof(FullQuestBase))]
    [HarmonyPatch(nameof(FullQuestBase.BeginQuest))]
    public static class QuestManagerBeginQuestPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        public static bool Prefix(FullQuestBase __instance, Action afterPrompt, bool showPrompt = true)
        {
            try
            {
                Logger.LogInfo($"[Quest] BeginQuest called for: {__instance.name}");
                if (QuestIds.LOCKED_QUEST.Contains(__instance.name))
                {
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(QuestManagerBeginQuestPatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }

    [HarmonyPatch(typeof(FullQuestBase), nameof(FullQuestBase.Completion), MethodType.Setter)]
    public static class QuestManagerCompletionSetterPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        public static bool Prefix(FullQuestBase __instance, ref QuestCompletionData.Completion value)
        {
            try
            {
                if (value.IsCompleted && QuestIds.ALL_QUESTS.Contains(__instance.name))
                {
                    Logger.LogInfo($"[Quest] Completion setter called for: {__instance.name}");
                    var locationId = ArchipelagoLocationIds.GetArchipelagoName(__instance.name);
                    if (locationId != null)
                    {
                        __instance.rewardItem = null;
                        ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(locationId);
                    }
                }
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(QuestManagerCompletionSetterPatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }
}
