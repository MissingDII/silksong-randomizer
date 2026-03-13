using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Item
{
    [HarmonyPatch(typeof(SkillGetMsg), "Setup")]
    public static class SkillMessagePatch
    {
        public static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        static bool Prefix(SkillGetMsg __instance, ToolItemSkill skill)
        {
            try
            {
                if (ToolsStrings.SILK_ABILITIES.Contains(skill.name))
                {
                    var data = skill.SavedData;
                    data.IsUnlocked = false;
                    data.AmountLeft = 0;
                    skill.SavedData = data;
                    ToolItemManager.UnequipTool(skill);
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD; // skip the popup
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogErrorException(nameof(SkillMessagePatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }

    [HarmonyPatch(typeof(SkillGetMsg), nameof(SkillGetMsg.Spawn))]
    public static class SkillGetMsg_Spawn_Patch
    {
        static bool Prefix(SkillGetMsg prefab, ToolItemSkill skill, Action afterMsg)
        {
            try
            {
                if (ToolsStrings.SILK_ABILITIES.Contains(skill.name))
                {
                    var data = skill.SavedData;
                    data.IsUnlocked = false;
                    data.AmountLeft = 0;
                    skill.SavedData = data;
                    ToolItemManager.UnequipTool(skill);
                    afterMsg?.Invoke(); // make sure the FSM continues
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                SkillMessagePatch._logger?.LogErrorException(nameof(SkillGetMsg_Spawn_Patch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }
}
