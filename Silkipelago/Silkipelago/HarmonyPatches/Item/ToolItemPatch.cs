using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Item
{
    [HarmonyPatch(typeof(ToolItem), nameof(ToolItem.Unlock))]
    public static class ToolItemPatch
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        static bool Prefix(ToolItem __instance, Action afterTutorialMsg, ToolItem.PopupFlags popupFlags)
        {
            if (ToolsStrings.SILK_ABILITIES.Contains(__instance.name))
            {
                var locationChecker = ArchipelagoPlugin.App.LocationChecker;
                _logger.LogInfo($"[ToolItem] {nameof(ToolItem.Unlock)} called, item={__instance.name}");
                var archipelagoId = ArchipelagoIds.GetArchipelagoName(__instance.name);
                if (locationChecker.LocationExists(archipelagoId))
                {
                    locationChecker.AddCheckedLocation(archipelagoId);
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }
            }
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }
}
