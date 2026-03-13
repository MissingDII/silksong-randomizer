using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Settings;
using System;

namespace Silkipelago.HarmonyPatches.GameState
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch(nameof(GameManager.ReturnToMainMenu))]
    public static class SavaDataSetToNullHook
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        private static void Prefix(GameManager __instance, ref System.Action<bool> callback)
        {
            try
            {
                SaveSettings.saveGlobalSaveDataSettings(__instance.profileID);
                var archipelagoClient = ArchipelagoPlugin.App.ArchipelagoClient;
                archipelagoClient.DisconnectPermanently();
            }
            catch (Exception ex)
            {
                _logger?.LogErrorException(nameof(SavaDataSetToNullHook), nameof(Prefix), ex);
            }
        }
    }
}
