using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Steamworks;
using System;

namespace Silkipelago.HarmonyPatches.Steam
{
    [HarmonyPatch(typeof(SteamAPI))]
    [HarmonyPatch(nameof(SteamAPI.RestartAppIfNecessary))]
    public static class SteamValidationPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        // public static bool RestartAppIfNecessary(AppId_t unOwnAppID)
        public static bool Prefix(AppId_t unOwnAppID, ref bool __result)
        {
            try
            {
                Logger.LogDebugPatchIsRunning(nameof(SteamAPI), nameof(SteamAPI.RestartAppIfNecessary), nameof(SteamValidationPatch), nameof(Prefix));

                __result = false;
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(SteamValidationPatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }
}
