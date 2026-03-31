using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Item
{
    public static class PlayerDataPatchHelper
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        public static void ExecutePatchLogic(string patchName, string methodName, Action logic)
        {
            try
            {
                logic?.Invoke();
            }
            catch (Exception ex)
            {
                Logger?.LogErrorException(patchName, methodName, ex);
            }
        }

        public static T ExecutePatchLogic<T>(string patchName, string methodName, Func<T> logic, T defaultReturn = default)
        {
            try
            {
                return logic != null ? logic.Invoke() : defaultReturn;
            }
            catch (Exception ex)
            {
                Logger?.LogErrorException(patchName, methodName, ex);
                return defaultReturn;
            }
        }

        public static bool HandlePlayerDataFieldChange(string fieldName, SilksongLocationChecker locationChecker)
        {
            Logger.LogInfo(fieldName);
            if (IsSilkHeart(fieldName) && locationChecker.LocationExists(ArchipelagoLocationIds.GetArchipelagoName(BossIds.BELL_BEAST)))
            {
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }
            if (IsCrestField(fieldName) && locationChecker.LocationExists("Eva: 0 Slots"))
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;

            if (IsChapelField(fieldName) && locationChecker.LocationExists(ArchipelagoLocationIds.GetArchipelagoName(CrestIds.REAPER)))
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;

            if (IsSilkAbility(fieldName))
            {
                TrackLocation(fieldName, locationChecker);
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }

            if (IsTrackableLocation(fieldName))
            {
                TrackLocation(fieldName, locationChecker);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }

            // Track randomized abilities, keys, melodies
            if (IsRandomizableContent(fieldName))
            {
                if (TrackLocation(fieldName, locationChecker))
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }

            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
        private static bool IsSilkHeart(string fieldName)
            => PlayerDataIds.SILK_HEART.Equals(fieldName);


        private static bool IsCrestField(string fieldName)
            => PlayerDataIds.EVA_UPGRADES.Contains(fieldName);

        private static bool IsChapelField(string fieldName)
            => PlayerDataIds.CHAPELS.Contains(fieldName);

        private static bool IsSilkAbility(string fieldName)
            => PlayerDataIds.SILK_ABILITIES.Contains(fieldName);

        private static bool IsTrackableLocation(string fieldName)
            => PlayerDataIds.CUTSCENES.Contains(fieldName) || PlayerDataIds.BOSSES.Contains(fieldName);

        private static bool IsRandomizableContent(string fieldName)
            => PlayerDataIds.ABILITIES.Contains(fieldName) ||
               PlayerDataIds.KEYS.Contains(fieldName) ||
               PlayerDataIds.MELODIES.Contains(fieldName);

        private static bool TrackLocation(string fieldName, SilksongLocationChecker locationChecker)
        {
            var locationId = ArchipelagoLocationIds.GetArchipelagoName(fieldName);

            if (locationChecker.LocationExists(locationId))
            {
                locationChecker.AddCheckedLocation(locationId);
                return true;
            }

            return false;
        }
    }
}

