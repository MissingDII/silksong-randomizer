using HarmonyLib;
using Silkipelago.Settings;
using System;

namespace Silkipelago.HarmonyPatches.GameState
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch(nameof(GameManager.ReturnToMainMenu))]
    public static class SavaDataSetToNullHook
    {
        private static void Prefix(GameManager __instance, ref System.Action<bool> callback)
        {
            BasePatch.SafeExecuteVoid(() => HandleReturnToMainMenu(__instance), nameof(SavaDataSetToNullHook), nameof(Prefix));
        }

        private static void HandleReturnToMainMenu(GameManager __instance)
        {
            SaveSettings.saveGlobalSaveDataSettings(__instance.profileID);
            var archipelagoClient = ArchipelagoPlugin.App.ArchipelagoClient;
            archipelagoClient.DisconnectPermanently();
            //reset archipelago classes
            ArchipelagoPlugin.App.ArchipelagoContext._archipelago = null;
            ArchipelagoPlugin.App.ArchipelagoContext._locationChecker = null;
            ArchipelagoPlugin.App.ArchipelagoContext._itemManager = null;
        }
    }
}
