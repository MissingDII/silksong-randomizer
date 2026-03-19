using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Tools
{
    [HarmonyPatch(typeof(SkillGetMsg), "Setup")]
    public static class SkillMessagePatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        static bool Prefix(SkillGetMsg __instance, ToolItemSkill skill)
        {
            try
            {
                var locationChecker = ArchipelagoPlugin.App.LocationChecker;

                if (ShouldBlockSkillDisplay(skill, locationChecker))
                {
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }

                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                Logger?.LogErrorException(nameof(SkillMessagePatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }

        private static bool ShouldBlockSkillDisplay(ToolItemSkill skill, SilksongLocationChecker locationChecker)
        {
            if (IsBossLockedSkill(skill) && locationChecker.LocationExists(PlayerDataIds.FIRST_WEAVER_DEFEATED))
                return true;

            if (IsSilkAbility(skill))
            {
                UnlockAndUnequipSkill(skill);
                return true;
            }

            return false;
        }

        private static bool IsBossLockedSkill(ToolItemSkill skill)
            => skill.name is ToolsIds.RUNE_RAGE or ToolsIds.CROSS_STITCH;

        private static bool IsSilkAbility(ToolItemSkill skill)
            => ToolsIds.SILK_ABILITIES.Contains(skill.name);

        private static void UnlockAndUnequipSkill(ToolItemSkill skill)
        {
            var data = skill.SavedData;
            data.IsUnlocked = false;
            data.AmountLeft = 0;
            skill.SavedData = data;
            ToolItemManager.UnequipTool(skill);
        }
    }

    [HarmonyPatch(typeof(SkillGetMsg), nameof(SkillGetMsg.Spawn))]
    public static class SkillGetMsgSpawnPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;
        static bool Prefix(SkillGetMsg prefab, ToolItemSkill skill, Action afterMsg)
        {
            try
            {
                var locationChecker = ArchipelagoPlugin.App.LocationChecker;

                if (ShouldBlockSkillSpawn(skill, locationChecker))
                {
                    UnlockAndUnequipSkill(skill);
                    afterMsg?.Invoke();
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }

                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(SkillGetMsgSpawnPatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }

        private static bool ShouldBlockSkillSpawn(ToolItemSkill skill, SilksongLocationChecker locationChecker)
        {
            if (IsBossLockedSkill(skill) && locationChecker.LocationExists(PlayerDataIds.FIRST_WEAVER_DEFEATED))
                return true;

            if (IsSilkAbility(skill))
                return true;

            return false;
        }

        private static bool IsBossLockedSkill(ToolItemSkill skill)
            => skill.name is ToolsIds.RUNE_RAGE or ToolsIds.CROSS_STITCH;

        private static bool IsSilkAbility(ToolItemSkill skill)
            => ToolsIds.SILK_ABILITIES.Contains(skill.name);

        private static void UnlockAndUnequipSkill(ToolItemSkill skill)
        {
            var data = skill.SavedData;
            data.IsUnlocked = false;
            data.AmountLeft = 0;
            skill.SavedData = data;
            ToolItemManager.UnequipTool(skill);
        }
    }
}
