using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Tools
{
    [HarmonyPatch(typeof(ToolItem), nameof(ToolItem.Unlock))]
    public static class ToolItemPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        static bool Prefix(ToolItem __instance, Action afterTutorialMsg, ToolItem.PopupFlags popupFlags)
        {
            try
            {
                var locationChecker = ArchipelagoPlugin.App.LocationChecker;

                if (ShouldBlockUnlock(__instance, locationChecker))
                {
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }

                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(ToolItemPatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }

        private static bool ShouldBlockUnlock(ToolItem tool, SilksongLocationChecker locationChecker)
        {
            if (IsBossLockedTool(tool) && locationChecker.LocationExists(PlayerDataStrings.FIRST_WEAVER_DEFEATED))
                return true;

            if (IsEvaLockedTool(tool) && locationChecker.LocationExists("Eva: 0 Slots"))
                return true;

            if (IsSilkAbility(tool))
                return CheckAndTrackSilkAbilityLocation(tool, locationChecker);

            return false;
        }

        private static bool IsBossLockedTool(ToolItem tool)
            => tool.name is ToolsStrings.RUNE_RAGE or ToolsStrings.CROSS_STITCH;

        private static bool IsEvaLockedTool(ToolItem tool)
            => tool.name == ToolsStrings.SYLPHSONG;

        private static bool IsSilkAbility(ToolItem tool)
            => ToolsStrings.SILK_ABILITIES.Contains(tool.name);

        private static bool CheckAndTrackSilkAbilityLocation(ToolItem tool, SilksongLocationChecker locationChecker)
        {
            var locationId = ArchipelagoLocationIds.GetArchipelagoName(tool.name);

            if (locationChecker.LocationExists(locationId))
            {
                Logger.LogInfo($"[ToolItem] {nameof(ToolItem.Unlock)} called, item={tool.name}");
                locationChecker.AddCheckedLocation(locationId);
                return true;
            }

            return false;
        }
    }
}
