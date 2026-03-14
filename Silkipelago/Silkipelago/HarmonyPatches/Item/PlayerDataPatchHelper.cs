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
            if (PlayerDataStrings.SILK_ABILITIES.Contains(fieldName))
            {
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }
            if (PlayerDataStrings.CUTSCENES.Contains(fieldName) || PlayerDataStrings.BOSSES.Contains(fieldName))
            {
                var archipelagoItemName = ArchipelagoIds.GetArchipelagoName(fieldName);
                locationChecker.AddCheckedLocation(archipelagoItemName);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            if (PlayerDataStrings.ABILITIES.Contains(fieldName) ||
                PlayerDataStrings.KEYS.Contains(fieldName) ||
                PlayerDataStrings.MELODIES.Contains(fieldName))
            {
                var archipelagoItemName = ArchipelagoIds.GetArchipelagoName(fieldName);
                if (locationChecker.LocationExists(archipelagoItemName))
                {
                    locationChecker.AddCheckedLocation(archipelagoItemName);
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }
            }

            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }
}

