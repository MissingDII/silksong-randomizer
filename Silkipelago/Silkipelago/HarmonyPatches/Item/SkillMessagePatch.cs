using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Item
{
    [HarmonyPatch(typeof(SkillGetMsg), "Setup")]
    public static class SkillMessagePatch
    {
        static bool Prefix(SkillGetMsg __instance, ToolItemSkill skill)
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
    }

    [HarmonyPatch(typeof(SkillGetMsg), nameof(SkillGetMsg.Spawn))]
    public static class SkillGetMsg_Spawn_Patch
    {
        static bool Prefix(SkillGetMsg prefab, ToolItemSkill skill, Action afterMsg)
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
    }
}
