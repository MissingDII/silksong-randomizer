using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Tools
{
    [HarmonyPatch(typeof(ToolItem), nameof(ToolItem.SetUnlockedTestsComplete))]
    public static class ToolItemAlternatePatch
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        static bool Prefix(ToolItem __instance)
        {
            try
            {
                var locationChecker = ArchipelagoPlugin.App.LocationChecker;

                if (ShouldBlockCompletion(__instance, locationChecker))
                {
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }

                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(nameof(ToolItemAlternatePatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }

        private static bool ShouldBlockCompletion(ToolItem tool, SilksongLocationChecker locationChecker)
        {
            if (IsBossLockedTool(tool) && locationChecker.LocationExists(PlayerDataStrings.FIRST_WEAVER_DEFEATED))
                return true;

            if (IsEvaLockedTool(tool) && locationChecker.LocationExists("Eva: 0 Slots"))
                return true;

            if (IsSilkAbility(tool) && locationChecker.LocationExists(ArchipelagoLocationIds.GetArchipelagoName(tool.name)))
                return true;

            return false;
        }

        private static bool IsBossLockedTool(ToolItem tool)
            => tool.name is ToolsStrings.RUNE_RAGE or ToolsStrings.CROSS_STITCH;

        private static bool IsEvaLockedTool(ToolItem tool)
            => tool.name == ToolsStrings.SYLPHSONG;

        private static bool IsSilkAbility(ToolItem tool)
            => ToolsStrings.SILK_ABILITIES.Contains(tool.name);
    }
}
