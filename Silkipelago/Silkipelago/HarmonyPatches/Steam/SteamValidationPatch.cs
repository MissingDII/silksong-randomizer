using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Steamworks;
using System;

namespace Silkipelago.HarmonyPatches.Steam
{
    [HarmonyPatch(typeof(SteamAPI))]
    [HarmonyPatch(nameof(SteamAPI.RestartAppIfNecessary))]
    public static class SteamValidationPatch
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        // public static bool RestartAppIfNecessary(AppId_t unOwnAppID)
        public static bool Prefix(AppId_t unOwnAppID, ref bool __result)
        {
            try
            {
                _logger.LogDebugPatchIsRunning(nameof(SteamAPI), nameof(SteamAPI.RestartAppIfNecessary), nameof(SteamValidationPatch), nameof(Prefix));

                __result = false;
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(nameof(SteamValidationPatch), nameof(Prefix), ex);
                return true;
            }
        }
    }
}
