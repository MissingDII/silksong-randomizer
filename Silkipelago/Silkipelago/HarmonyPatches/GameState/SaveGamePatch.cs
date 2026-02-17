using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Settings;
using System;

namespace Silkipelago.HarmonyPatches.GameState
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch(nameof(GameManager.SaveGame),
        new Type[] {
        typeof(int),
        typeof(Action<bool>),
        typeof(bool),
        typeof(AutoSaveName)
    })]
    public class SaveGamePatch
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }
        //    public void SaveGame(int saveSlot, Action<bool> ogCallback, bool withAutoSave = false, AutoSaveName autoSaveName = AutoSaveName.NONE)
        public static bool Prefix(GameManager __instance, int saveSlot, Action<bool> ogCallback, bool withAutoSave, AutoSaveName autoSaveName)
        {
            try
            {
                _logger.LogDebugPatchIsRunning(nameof(GameManager), nameof(GameManager.SaveGame), nameof(SaveGamePatch), nameof(Prefix));
                SaveSettings.saveGlobalSaveDataSettings(saveSlot);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(nameof(SaveGamePatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }
}
