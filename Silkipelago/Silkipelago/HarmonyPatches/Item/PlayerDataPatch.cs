using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.HarmonyPatches.Steam;
using System;
using System.Collections.Generic;

namespace Silkipelago.HarmonyPatches.Item
{
    [HarmonyPatch(typeof(PlayerData))]
    [HarmonyPatch(nameof(PlayerData.SetBool))]
    public static class PlayerDataPatch
    {
        private static ILogger _logger;
        private static List<String> randomizedItem;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        // public void SetBool(string boolName, bool value)
        public static bool Prefix(PlayerData __instance, string boolName, bool value)
        {
            try
            {
                _logger.LogDebugPatchIsRunning(nameof(PlayerData), nameof(PlayerData.SetBool), nameof(PlayerDataPatch), nameof(Prefix));
                _logger.LogInfo($"Modified value is {boolName}");
                if (boolName.Equals("hasDash"))
                {
                   // _logger.LogInfo(Environment.StackTrace);
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }
                else
                {
                    return MethodPrefix.RUN_ORIGINAL_METHOD;
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(nameof(SteamValidationPatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }
}
