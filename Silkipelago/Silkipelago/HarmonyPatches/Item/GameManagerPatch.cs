using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Settings;
using System;

namespace Silkipelago.HarmonyPatches.Item
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch(nameof(GameManager.IsGameplayScene))]
    public static class GameManagerPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        //   public bool IsGameplayScene()
        public static void Postfix(GameManager __instance, bool __result)
        {
            try
            {
                if (__result && ArchipelagoPlugin.App.ArchipelagoClient._shouldDoInitialLoad)
                {
                    // Do something when IsGameplayScene returns true
                    Logger.LogDebug("Entering Gameplay Scene and loading items");
                    var itemManager = ArchipelagoPlugin.App.ItemManager;
                    itemManager.ReceiveAllNewItems();
                    var slotId = __instance.profileID;
                    var saveSettingsData = ArchipelagoPlugin.App.SettingsContext.saveSettingsData;
                    SaveSettings.saveGlobalSaveDataSettings(slotId);

                    ArchipelagoPlugin.App.ArchipelagoClient._shouldDoInitialLoad = false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(GameManagerPatch), nameof(Postfix), ex);
            }
        }
    }
}
