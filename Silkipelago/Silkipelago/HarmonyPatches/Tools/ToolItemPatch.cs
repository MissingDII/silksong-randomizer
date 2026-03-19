using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using Silkipelago.Archipelago;
using Silkipelago.Archipelago.ItemHandlers;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Tools
{
    [HarmonyPatch(typeof(ToolItem), nameof(ToolItem.Unlock))]
    public static class ToolItemPatch
    {
        static bool Prefix(ToolItem __instance, Action afterTutorialMsg, ToolItem.PopupFlags popupFlags)
        {
            return BasePatch.SafeExecute(() => HandleUnlock(__instance), nameof(ToolItemPatch), nameof(Prefix));
        }

        private static bool HandleUnlock(ToolItem __instance)
        {
            var locationChecker = ArchipelagoPlugin.App.LocationChecker;

            if (SilksongItemManager.ItemToReceive == 0 && ShouldBlockUnlock(__instance, locationChecker))
            {
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }
            SilksongItemManager.ItemToReceive--;
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }

        private static bool ShouldBlockUnlock(ToolItem tool, SilksongLocationChecker locationChecker)
        {
            if (IsBossLockedTool(tool) && locationChecker.LocationExists(PlayerDataIds.FIRST_WEAVER_DEFEATED))
                return true;

            if (IsEvaLockedTool(tool) && locationChecker.LocationExists(LocationConstants.EvaUpgradeLocation))
                return true;

            if (IsSilkAbility(tool))
                return CheckAndTrackSilkAbilityLocation(tool, locationChecker);

            return false;
        }

        private static bool IsBossLockedTool(ToolItem tool)
            => tool.name is ToolsIds.RUNE_RAGE or ToolsIds.CROSS_STITCH;

        private static bool IsEvaLockedTool(ToolItem tool)
            => tool.name == ToolsIds.SYLPHSONG;

        private static bool IsSilkAbility(ToolItem tool)
            => ToolsIds.SILK_ABILITIES.Contains(tool.name);

        private static bool CheckAndTrackSilkAbilityLocation(ToolItem tool, SilksongLocationChecker locationChecker)
        {
            var locationId = ArchipelagoLocationIds.GetArchipelagoName(tool.name);

            if (locationChecker.LocationExists(locationId))
            {
                BasePatch.Logger.LogInfo($"[ToolItem] {nameof(ToolItem.Unlock)} called, item={tool.name}");
                locationChecker.AddCheckedLocation(locationId);
                return true;
            }

            return false;
        }
    }
}
