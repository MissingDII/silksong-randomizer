using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Crests
{
    [HarmonyPatch(typeof(ToolCrest), nameof(ToolCrest.Unlock))]
    public static class CrestUnlockPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        static bool Prefix(ToolCrest __instance)
        {
            try
            {
                var locationChecker = ArchipelagoPlugin.App.LocationChecker;
                Logger.LogInfo($"[ToolCrest] Unlock called for Crest: {__instance.name}");

                if (ShouldBlockUnlock(__instance, locationChecker))
                {
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }

                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(CrestUnlockPatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }

        private static bool ShouldBlockUnlock(ToolCrest crest, SilksongLocationChecker locationChecker)
        {
            // Block eva crest upgrades if they're randomized
            if (IsEvaUpgradeCrest(crest) && locationChecker.LocationExists("Eva: 0 Slots"))
                return true;

            // Block regular crests if they're in the randomizer
            if (IsRandomizedCrest(crest, locationChecker))
                return true;

            return false;
        }

        private static bool IsEvaUpgradeCrest(ToolCrest crest)
            => CrestStrings.CRESTS_UPGRADE.Contains(crest.name);

        private static bool IsRandomizedCrest(ToolCrest crest, SilksongLocationChecker locationChecker)
        {
            if (!CrestStrings.CRESTS.Contains(crest.name))
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
