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
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        private static void Prefix(GameManager __instance, ref System.Action<bool> callback)
        {
            try
            {
                SaveSettings.saveGlobalSaveDataSettings(__instance.profileID);
                var archipelagoClient = ArchipelagoPlugin.App.ArchipelagoClient;
                archipelagoClient.DisconnectPermanently();
                //reset archipelago classes
                ArchipelagoPlugin.App.ArchipelagoContext._archipelago = null;
                ArchipelagoPlugin.App.ArchipelagoContext._locationChecker = null;
                ArchipelagoPlugin.App.ArchipelagoContext._itemManager = null;
            }
            catch (Exception ex)
            {
                Logger?.LogErrorException(nameof(SavaDataSetToNullHook), nameof(Prefix), ex);
            }
        }
    }
}
