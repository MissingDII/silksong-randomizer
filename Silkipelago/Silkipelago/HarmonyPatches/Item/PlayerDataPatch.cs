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
            return PlayerDataPatchHelper.ExecutePatchLogic(_logger, nameof(PlayerDataPatch), nameof(Prefix), () =>
            {
                _logger.LogInfo(boolName);
                _logger.LogDebugPatchIsRunning(nameof(PlayerData), nameof(PlayerData.SetBool), nameof(PlayerDataPatch), nameof(Prefix));

                if (SilksongItemManager._itemToReceive == 0)
                {
                    var result = PlayerDataPatchHelper.HandlePlayerDataFieldChange(boolName, _silksongLocationChecker);
                    if (result != MethodPrefix.RUN_ORIGINAL_METHOD)
                    {
                        return result;
                    }
                }

                SilksongItemManager._itemToReceive--;
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }, MethodPrefix.RUN_ORIGINAL_METHOD);
        }
    }
}
