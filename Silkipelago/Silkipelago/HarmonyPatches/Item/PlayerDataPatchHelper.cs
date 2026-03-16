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
            // Block crest changes if Eva is randomized
            if (IsCrestField(fieldName) && locationChecker.LocationExists("Eva: 0 Slots"))
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;

            // Block chapel changes if  crest are randomized
            if (IsChapelField(fieldName) && locationChecker.LocationExists(ArchipelagoLocationIds.GetArchipelagoName(CrestStrings.REAPER)))
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;

            // Block silk abilities
            if (IsSilkAbility(fieldName))
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;

            // Track cutscenes and bosses
            if (IsTrackableLocation(fieldName))
            {
                TrackLocation(fieldName, locationChecker, useLocationIds: false);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }

            // Track randomized abilities, keys, melodies
            if (IsRandomizableContent(fieldName))
            {
                if (TrackLocation(fieldName, locationChecker, useLocationIds: true))
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }

            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }

        private static bool IsCrestField(string fieldName)
            => PlayerDataStrings.CREST.Contains(fieldName);

        private static bool IsChapelField(string fieldName)
            => PlayerDataStrings.CHAPELS.Contains(fieldName);

        private static bool IsSilkAbility(string fieldName)
            => PlayerDataStrings.SILK_ABILITIES.Contains(fieldName);

        private static bool IsTrackableLocation(string fieldName)
            => PlayerDataStrings.CUTSCENES.Contains(fieldName) || PlayerDataStrings.BOSSES.Contains(fieldName);

        private static bool IsRandomizableContent(string fieldName)
            => PlayerDataStrings.ABILITIES.Contains(fieldName) ||
               PlayerDataStrings.KEYS.Contains(fieldName) ||
               PlayerDataStrings.MELODIES.Contains(fieldName);

        private static bool TrackLocation(string fieldName, SilksongLocationChecker locationChecker, bool useLocationIds)
        {
            var locationId = useLocationIds
                ? ArchipelagoItemIds.GetArchipelagoName(fieldName)
                : ArchipelagoLocationIds.GetArchipelagoName(fieldName);

            if (locationChecker.LocationExists(locationId))
            {
                locationChecker.AddCheckedLocation(locationId);
                return true;
            }

            return false;
        }
    }
}

