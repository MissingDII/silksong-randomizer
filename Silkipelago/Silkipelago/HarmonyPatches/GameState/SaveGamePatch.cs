using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
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
        //    public void SaveGame(int saveSlot, Action<bool> ogCallback, bool withAutoSave = false, AutoSaveName autoSaveName = AutoSaveName.NONE)
        public static bool Prefix(GameManager __instance, int saveSlot, Action<bool> ogCallback, bool withAutoSave, AutoSaveName autoSaveName)
        {
            return BasePatch.SafeExecute(() => HandleSaveGame(saveSlot), nameof(SaveGamePatch), nameof(Prefix));
        }

        private static bool HandleSaveGame(int saveSlot)
        {
            BasePatch.Logger.LogDebugPatchIsRunning(nameof(GameManager), nameof(GameManager.SaveGame), nameof(SaveGamePatch), nameof(Prefix));
            SaveSettings.saveGlobalSaveDataSettings(saveSlot);
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }
}
