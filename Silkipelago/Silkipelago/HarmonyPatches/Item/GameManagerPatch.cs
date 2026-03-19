using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago.ItemHandlers;
using Silkipelago.Constants;
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
                var archipelagoClient = ArchipelagoPlugin.App.ArchipelagoClient;

                if (__result && archipelagoClient != null && archipelagoClient._shouldDoInitialLoad)
                {
                    if (ArchipelagoPlugin.App.SettingsContext.saveSettingsData.ProcessedLocations.IsNullOrEmpty())
                    {
                        //in all cases we want to lock hunter crest we always receive a crest from server
                        var hunterCrest = ToolItemManager.GetCrestByName(CrestIds.HUNTER);
                        var saveData = hunterCrest.SaveData;
                        saveData.IsUnlocked = false;
                        hunterCrest.SaveData = saveData;
                        CrestHandler.autoEquipCrest = true;
                    }
                    Logger.LogDebug("Entering Gameplay Scene and loading items");
                    var itemManager = ArchipelagoPlugin.App.ItemManager;
                    itemManager.ReceiveAllNewItems();
                    PlayerDataHandler.keepChapelsOpen();
                    var slotId = __instance.profileID;
                    var saveSettingsData = ArchipelagoPlugin.App.SettingsContext.saveSettingsData;
                    SaveSettings.saveGlobalSaveDataSettings(slotId);
                    archipelagoClient._shouldDoInitialLoad = false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(GameManagerPatch), nameof(Postfix), ex);
            }
        }
    }
}
