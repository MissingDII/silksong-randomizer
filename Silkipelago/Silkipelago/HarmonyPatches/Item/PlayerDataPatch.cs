using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;
using Silkipelago.Constants;
using Silkipelago.Items;
using System;

namespace Silkipelago.HarmonyPatches.Item
{
    [HarmonyPatch(typeof(PlayerData))]
    [HarmonyPatch(nameof(PlayerData.SetBool))]
    public static class PlayerDataPatch
    {
        private static ILogger _logger;
        private static SilksongArchipelagoClient _silksongArchipelagoClient;
        private static SilksongLocationChecker _silksongLocationChecker;

        public static void Initialize(ILogger logger, SilksongArchipelagoClient silksongArchipelagoClient, SilksongLocationChecker silksongLocationChecker)
        {
            _logger = logger;
            _silksongArchipelagoClient = silksongArchipelagoClient;
            _silksongLocationChecker = silksongLocationChecker;
        }

        // public void SetBool(string boolName, bool value)
        public static bool Prefix(PlayerData __instance, string boolName, bool value)
        {
            try
            {
                _logger.LogDebugPatchIsRunning(nameof(PlayerData), nameof(PlayerData.SetBool), nameof(PlayerDataPatch), nameof(Prefix));
                if (SilksongItemManager._itemToReceive == 0)
                {
                    if (PlayerDataStrings.ABILITIES.Contains(boolName) || PlayerDataStrings.BOSSES.Contains(boolName))
                    {
                        var archipelagoItemName = ArchipelagoIds.GetArchipelagoName(boolName);
                        _logger.LogInfo("sending location for " + archipelagoItemName);
                        _silksongLocationChecker.AddCheckedLocation(archipelagoItemName);
                        _logger.LogInfo("sent location for " + archipelagoItemName);
                        return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                    }
                    else
                    {
                        return MethodPrefix.RUN_ORIGINAL_METHOD;
                    }
                }
                SilksongItemManager._itemToReceive--;
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(nameof(PlayerDataPatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }
}
