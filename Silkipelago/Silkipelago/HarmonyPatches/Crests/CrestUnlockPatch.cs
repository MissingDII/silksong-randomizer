using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using Silkipelago.Archipelago;
using Silkipelago.Archipelago.ItemHandlers;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Crests
{
    [HarmonyPatch(typeof(ToolCrest), nameof(ToolCrest.Unlock))]
    public static class CrestUnlockPatch
    {
        static bool Prefix(ToolCrest __instance)
        {
            return BasePatch.SafeExecute(() => HandleCrestUnlock(__instance), nameof(CrestUnlockPatch), nameof(Prefix));
        }

        private static bool HandleCrestUnlock(ToolCrest __instance)
        {
            if (SilksongItemManager.ItemToReceive == 0)
            {
                var locationChecker = ArchipelagoPlugin.App.LocationChecker;
                BasePatch.Logger.LogInfo($"[ToolCrest] Unlock called for Crest: {__instance.name}");

                if (ShouldBlockUnlock(__instance, locationChecker))
                {
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }
            }
            SilksongItemManager.ItemToReceive--;
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }

        private static bool ShouldBlockUnlock(ToolCrest crest, SilksongLocationChecker locationChecker)
        {
            // Block eva crest upgrades if they're randomized
            if (IsEvaUpgradeCrest(crest) && locationChecker.LocationExists(LocationConstants.EvaUpgradeLocation))
                return true;

            // Block regular crests if they're in the randomizer
            if (IsRandomizedCrest(crest, locationChecker))
                return true;

            return false;
        }

        private static bool IsEvaUpgradeCrest(ToolCrest crest)
            => CrestIds.CRESTS_UPGRADE.Contains(crest.name);

        private static bool IsRandomizedCrest(ToolCrest crest, SilksongLocationChecker locationChecker)
        {
            if (!CrestIds.CRESTS.Contains(crest.name))
                return false;

            var locationId = ArchipelagoLocationIds.GetArchipelagoName(crest.name);

            if (locationChecker.LocationExists(locationId))
            {
                locationChecker.AddCheckedLocation(locationId);
                return true;
            }

            return false;
        }
    }
}
