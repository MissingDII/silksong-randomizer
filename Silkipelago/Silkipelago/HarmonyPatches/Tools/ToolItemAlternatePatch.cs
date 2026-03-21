using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using Silkipelago.Archipelago;
using Silkipelago.Archipelago.ItemHandlers;
using Silkipelago.Constants;

namespace Silkipelago.HarmonyPatches.Tools
{
    [HarmonyPatch(typeof(ToolItem), nameof(ToolItem.SetUnlockedTestsComplete))]
    public static class ToolItemAlternatePatch
    {
        static bool Prefix(ToolItem __instance)
        {
            return BasePatch.SafeExecute(() => HandleSetUnlockedTestsComplete(__instance), nameof(ToolItemAlternatePatch), nameof(Prefix));
        }

        private static bool HandleSetUnlockedTestsComplete(ToolItem __instance)
        {
            var locationChecker = ArchipelagoPlugin.App.LocationChecker;

            if (SilksongItemManager.ItemToReceive == 0 && ShouldBlockCompletion(__instance, locationChecker))
            {
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }
            SilksongItemManager.ItemToReceive--;
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }

        private static bool ShouldBlockCompletion(ToolItem tool, SilksongLocationChecker locationChecker)
        {
            if (IsBossLockedTool(tool) && locationChecker.LocationExists(PlayerDataIds.FIRST_WEAVER_DEFEATED))
                return true;

            if (IsSilkAbility(tool) && locationChecker.LocationExists(ArchipelagoLocationIds.GetArchipelagoName(tool.name)))
                return true;

            return false;
        }

        private static bool IsBossLockedTool(ToolItem tool)
            => tool.name is ToolsIds.RUNE_RAGE or ToolsIds.CROSS_STITCH;

        private static bool IsSilkAbility(ToolItem tool)
            => ToolsIds.SILK_ABILITIES.Contains(tool.name);
    }
}
