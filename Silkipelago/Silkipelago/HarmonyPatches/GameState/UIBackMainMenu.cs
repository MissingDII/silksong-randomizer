using HarmonyLib;
using Silkipelago.Settings;

namespace Silkipelago.HarmonyPatches.GameState
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch(nameof(GameManager.ReturnToMainMenu))]
    public static class SavaDataSetToNullHook
    {
        private static void Prefix(GameManager __instance, ref System.Action<bool> callback)
        {
            SaveSettings.saveGlobalSaveDataSettings(__instance.profileID);
            var archipelagoClient = ArchipelagoPlugin.App.ArchipelagoClient;
            archipelagoClient.DisconnectPermanently();
        }
    }
}
