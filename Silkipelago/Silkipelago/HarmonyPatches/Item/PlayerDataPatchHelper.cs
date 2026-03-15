using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Item
{
    public static class PlayerDataPatchHelper
    {
        public static void ExecutePatchLogic(ILogger logger, string patchName, string methodName, Action logic)
        {
            try
            {
                logic?.Invoke();
            }
            catch (Exception ex)
            {
                logger?.LogErrorException(patchName, methodName, ex);
            }
        }

        public static T ExecutePatchLogic<T>(ILogger logger, string patchName, string methodName, Func<T> logic, T defaultReturn = default)
        {
            try
            {
                return logic != null ? logic.Invoke() : defaultReturn;
            }
            catch (Exception ex)
            {
                logger?.LogErrorException(patchName, methodName, ex);
                return defaultReturn;
            }
        }

        public static bool HandlePlayerDataFieldChange(string fieldName, SilksongLocationChecker locationChecker)
        {
            if (PlayerDataStrings.CREST.Contains(fieldName))
            {
                // check if eva is randomized
                if (locationChecker.LocationExists("Eva: 0 Slots"))
                {
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            if (PlayerDataStrings.SILK_ABILITIES.Contains(fieldName))
            {
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }
            if (PlayerDataStrings.CUTSCENES.Contains(fieldName) || PlayerDataStrings.BOSSES.Contains(fieldName))
            {
                var archipelagoLocationName = ArchipelagoLocationIds.GetArchipelagoName(fieldName);
                if (locationChecker.LocationExists(archipelagoLocationName))
                {
                    locationChecker.AddCheckedLocation(archipelagoLocationName);
                }
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            if (PlayerDataStrings.ABILITIES.Contains(fieldName) ||
                PlayerDataStrings.KEYS.Contains(fieldName) ||
                PlayerDataStrings.MELODIES.Contains(fieldName))
            {
                var archipelagoLocationName = ArchipelagoItemIds.GetArchipelagoName(fieldName);
                if (locationChecker.LocationExists(archipelagoLocationName))
                {
                    locationChecker.AddCheckedLocation(archipelagoLocationName);
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }
            }

            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }
}

