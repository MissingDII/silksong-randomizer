using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Constants;

namespace Silkipelago.HarmonyPatches.Item
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
            if (ToolsStrings.SILK_ABILITIES.Contains(__instance.name))
            {
                var locationChecker = ArchipelagoPlugin.App.LocationChecker;
                _logger.LogInfo($"[ToolItem] {nameof(ToolItem.Unlock)} called, item={__instance.name}");
                var archipelagoId = ArchipelagoIds.GetArchipelagoName(__instance.name);
                if (locationChecker.LocationExists(archipelagoId))
                {
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }

            }
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }
}
