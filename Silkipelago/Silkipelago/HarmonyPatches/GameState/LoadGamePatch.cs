using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;
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
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }
        // public void SetLoadedGameData(SaveGameData saveGameData, int saveSlot)
        public static void Postfix(GameManager __instance, SaveGameData saveGameData, int saveSlot)
        {
            try
            {
                _logger.LogDebugPatchIsRunning(nameof(GameManager), nameof(GameManager.SetLoadedGameData), nameof(LoadGamePatch), nameof(Postfix));
                GlobalSaveSettingsData.saveSettingsData = SaveSettings.LoadSaveDataSettings(saveSlot);
                var APConnectionInfo = new ArchipelagoConnectionInfo(GlobalSaveSettingsData.saveSettingsData.HostName, GlobalSaveSettingsData.saveSettingsData.Port, GlobalSaveSettingsData.saveSettingsData.SlotName, false);
                ArchipelagoConnectionHandler.ConnectToArchipelago(APConnectionInfo);
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(nameof(SaveGamePatch), nameof(Postfix), ex);
            }
        }
    }
}
