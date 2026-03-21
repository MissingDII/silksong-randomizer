using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using Silkipelago.Archipelago;
using Silkipelago.Constants;

namespace Silkipelago.HarmonyPatches.Tools
{
    [HarmonyPatch(typeof(ToolItemManager), nameof(ToolItemManager.AutoEquip), typeof(ToolItem))]
    public static class ToolEquipPatch
    {
        static bool Prefix(ToolItem tool)
        {
            return BasePatch.SafeExecute(() => HandleAutoEquip(tool), nameof(ToolEquipPatch), nameof(Prefix));
        }

        private static bool HandleAutoEquip(ToolItem tool)
        {
            var locationChecker = ArchipelagoPlugin.App.LocationChecker;
            BasePatch.Logger.LogInfo($"[ToolItemManager] AutoEquip called for: {tool.name}");

            if (ShouldBlockEquip(tool, locationChecker))
            {
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }

            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }

        private static bool ShouldBlockEquip(ToolItem tool, SilksongLocationChecker locationChecker)
        {
            if (IsBossLockedTool(tool) && locationChecker.LocationExists(PlayerDataIds.FIRST_WEAVER_DEFEATED))
                return true;

            if (IsSilkAbility(tool))
                return true;

            return false;
        }

        private static bool IsBossLockedTool(ToolItem tool)
            => tool.name is ToolsIds.RUNE_RAGE or ToolsIds.CROSS_STITCH;

        private static bool IsSilkAbility(ToolItem tool)
            => ToolsIds.SILK_ABILITIES.Contains(tool.name);
    }
}
