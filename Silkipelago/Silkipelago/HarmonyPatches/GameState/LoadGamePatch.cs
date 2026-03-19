using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Settings;
using System;

namespace Silkipelago.HarmonyPatches.GameState
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch(nameof(GameManager.SetLoadedGameData),
    new Type[] {
        typeof(SaveGameData),
        typeof(int)
})]
    public class LoadGamePatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;
        // public void SetLoadedGameData(SaveGameData saveGameData, int saveSlot)
        public static bool Prefix(GameManager __instance, SaveGameData saveGameData, int saveSlot)
        {
            try
            {
                Logger.LogDebugPatchIsRunning(nameof(GameManager), nameof(GameManager.SetLoadedGameData), nameof(LoadGamePatch), nameof(Prefix));
                ArchipelagoPlugin.App.SettingsContext.saveSettingsData = SaveSettings.LoadSaveDataSettings(saveSlot);
                var saveSettingsData = ArchipelagoPlugin.App.SettingsContext.saveSettingsData;
                var APConnectionInfo = new ArchipelagoConnectionInfo(saveSettingsData.HostName, saveSettingsData.Port, saveSettingsData.SlotName, false);
                ArchipelagoPlugin.App.UIContext.ConnectionHandler.ConnectToArchipelago(APConnectionInfo);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(SaveGamePatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }
}
