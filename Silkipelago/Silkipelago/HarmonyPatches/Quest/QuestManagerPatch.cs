using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Quest
{
    /// <summary>
    /// Patches for quest completion via TryEndQuest method.
    /// </summary>
    [HarmonyPatch(typeof(FullQuestBase))]
    [HarmonyPatch(nameof(FullQuestBase.TryEndQuest))]
    public static class QuestManagerPatch
    {
        public static bool Prefix(FullQuestBase __instance, Action afterPrompt, bool consumeCurrency, bool forceEnd = false, bool showPrompt = true)
        {
            return BasePatch.SafeExecute(
                () => HandleQuestCompletion(__instance),
                nameof(QuestManagerPatch),
                nameof(Prefix)
            );
        }

        private static bool HandleQuestCompletion(FullQuestBase quest)
        {
            if (quest.CanComplete)
            {
                BasePatch.Logger.LogInfo($"[Quest] TryEndQuest called for: {quest.name}");
                if (QuestIds.ALL_QUESTS.Contains(quest.name))
                {
                    quest.rewardItem = null;
                    var locationId = ArchipelagoLocationIds.GetArchipelagoName(quest.name);
                    ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(locationId);
                }
            }
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }

    /// <summary>
    /// Patches for silent quest completion.
    /// </summary>
    [HarmonyPatch(typeof(FullQuestBase))]
    [HarmonyPatch(nameof(FullQuestBase.SilentlyComplete))]
    public static class QuestManagerSilentPatch
    {
        // patch upgrade Quest for both final soul snare quest and bell quest
        public static bool Prefix(FullQuestBase __instance)
        {
            return BasePatch.SafeExecute(
                () => HandleSilentCompletion(__instance),
                nameof(QuestManagerSilentPatch),
                nameof(Prefix)
            );
        }

        private static bool HandleSilentCompletion(FullQuestBase quest)
        {
            BasePatch.Logger.LogInfo($"[Quest] SilentlyComplete called for: {quest.name}");
            if (QuestIds.ALL_QUESTS.Contains(quest.name))
            {
                var locationId = ArchipelagoLocationIds.GetArchipelagoName(quest.name);
                if (locationId == null)
                {
                    BasePatch.Logger.LogWarning($"this quest is not mapped to a location but exists in AllQuest {quest.name}, please report a bug");
                }
                else
                {
                    quest.rewardItem = null;
                    ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(locationId);
                }
            }
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }

    /// <summary>
    /// Patches for quest acceptance/beginning.
    /// </summary>
    [HarmonyPatch(typeof(FullQuestBase))]
    [HarmonyPatch(nameof(FullQuestBase.BeginQuest))]
    public static class QuestManagerBeginQuestPatch
    {
        public static bool Prefix(FullQuestBase __instance, Action afterPrompt, bool showPrompt = true)
        {
            return BasePatch.SafeExecute(
                () => HandleQuestBegin(__instance),
                nameof(QuestManagerBeginQuestPatch),
                nameof(Prefix)
            );
        }

        private static bool HandleQuestBegin(FullQuestBase quest)
        {
            BasePatch.Logger.LogInfo($"[Quest] BeginQuest called for: {quest.name}");
            if (QuestIds.LOCKED_QUEST.Contains(quest.name))
            {
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }


    /// <summary>
    /// Patches for quest completion property setter.
    /// </summary>
    [HarmonyPatch(typeof(FullQuestBase), nameof(FullQuestBase.Completion), MethodType.Setter)]
    public static class QuestManagerCompletionSetterPatch
    {
        public static bool Prefix(FullQuestBase __instance, ref QuestCompletionData.Completion value)
        {
            try
            {
                if (value.IsCompleted && QuestIds.ALL_QUESTS.Contains(__instance.name))
                {
                    BasePatch.Logger.LogInfo($"[Quest] Completion setter called for: {__instance.name}");
                    var locationId = ArchipelagoLocationIds.GetArchipelagoName(__instance.name);
                    if (locationId != null)
                    {
                        __instance.rewardItem = null;
                        ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(locationId);
                    }
                }
                return KaitoKid.ArchipelagoUtilities.Net.Constants.MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (System.Exception ex)
            {
                BasePatch.Logger.LogErrorException(nameof(QuestManagerCompletionSetterPatch), nameof(Prefix), ex);
                return KaitoKid.ArchipelagoUtilities.Net.Constants.MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }
}
