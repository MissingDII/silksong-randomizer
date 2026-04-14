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
            var archipelagoClient = ArchipelagoPlugin.App.ArchipelagoClient;
            if (isNormalTool(tool))
            {
                return CheckAndTrackLocation(tool, locationChecker);
            }
            if (IsBossLockedTool(tool) && archipelagoClient.SlotData.CombatAbilitiesRandomized)
                return true;

            if (IsSilkAbility(tool))
                return CheckAndTrackLocation(tool, locationChecker);

            return false;
        }
        private static bool isNormalTool(ToolItem tool)
           => ToolsIds.TOOLs.Contains(tool.name);
        private static bool IsBossLockedTool(ToolItem tool)
            => tool.name is ToolsIds.RUNE_RAGE or ToolsIds.CROSS_STITCH;

        private static bool IsSilkAbility(ToolItem tool)
            => ToolsIds.SILK_ABILITIES.Contains(tool.name);

        private static bool CheckAndTrackLocation(ToolItem tool, SilksongLocationChecker locationChecker)
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
