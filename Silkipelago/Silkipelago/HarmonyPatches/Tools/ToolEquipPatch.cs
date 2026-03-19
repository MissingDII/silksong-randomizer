using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Tools
{
    [HarmonyPatch(typeof(ToolItemManager), nameof(ToolItemManager.AutoEquip), typeof(ToolItem))]
    public static class ToolEquipPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        static bool Prefix(ToolItem tool)
        {
            try
            {
                var locationChecker = ArchipelagoPlugin.App.LocationChecker;
                Logger.LogInfo($"[ToolItemManager] AutoEquip called for: {tool.name}");

                if (ShouldBlockEquip(tool, locationChecker))
                {
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }

                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(ToolEquipPatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }

        private static bool ShouldBlockEquip(ToolItem tool, SilksongLocationChecker locationChecker)
        {
            if (IsBossLockedTool(tool) && locationChecker.LocationExists(PlayerDataIds.FIRST_WEAVER_DEFEATED))
                return true;

            if (IsEvaLockedTool(tool) && locationChecker.LocationExists("Eva: 0 Slots"))
                return true;

            if (IsSilkAbility(tool))
                return true;

            return false;
        }

        private static bool IsBossLockedTool(ToolItem tool)
            => tool.name is ToolsIds.RUNE_RAGE or ToolsIds.CROSS_STITCH;

        private static bool IsEvaLockedTool(ToolItem tool)
            => tool.name == ToolsIds.SYLPHSONG;

        private static bool IsSilkAbility(ToolItem tool)
            => ToolsIds.SILK_ABILITIES.Contains(tool.name);
    }
}
