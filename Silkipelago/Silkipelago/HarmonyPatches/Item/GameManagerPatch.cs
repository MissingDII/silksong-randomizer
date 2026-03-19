using HarmonyLib;
using Silkipelago.Archipelago.ItemHandlers;
using Silkipelago.Constants;
using Silkipelago.Settings;

namespace Silkipelago.HarmonyPatches.Item
{
    /// <summary>
    /// Handles initial item loading when entering gameplay scenes.
    /// </summary>
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch(nameof(GameManager.IsGameplayScene))]
    public static class GameManagerPatch
    {
        /// <summary>
        /// Postfix that loads items when entering a gameplay scene for the first time.
        /// </summary>
        public static void Postfix(GameManager __instance, bool __result)
        {
            BasePatch.SafeExecuteVoid(
                () => HandleGameplaySceneEntry(__instance, __result),
                nameof(GameManagerPatch),
                nameof(Postfix)
            );
        }

        private static void HandleGameplaySceneEntry(GameManager __instance, bool isGameplayScene)
        {
            var archipelagoClient = ArchipelagoPlugin.App.ArchipelagoClient;

            if (!isGameplayScene || archipelagoClient == null || !archipelagoClient._shouldDoInitialLoad)
                return;

            if (ArchipelagoPlugin.App.SettingsContext.saveSettingsData.ProcessedLocations.IsNullOrEmpty())
            {
                LockHunterCrest();
            }

            LoadItemsForGameplay(__instance, archipelagoClient);
        }

        private static void LockHunterCrest()
        {
            var hunterCrest = ToolItemManager.GetCrestByName(CrestIds.HUNTER);
            var saveData = hunterCrest.SaveData;
            saveData.IsUnlocked = false;
            hunterCrest.SaveData = saveData;
            CrestHandler.autoEquipCrest = true;
        }

        private static void LoadItemsForGameplay(GameManager gameManager, Silkipelago.Archipelago.SilksongArchipelagoClient archipelagoClient)
        {
            BasePatch.Logger.LogDebug("Entering Gameplay Scene and loading items");
            var itemManager = ArchipelagoPlugin.App.ItemManager;
            itemManager.ReceiveAllNewItems();
            PlayerDataHandler.keepChapelsOpen();
            var slotId = gameManager.profileID;
            var saveSettingsData = ArchipelagoPlugin.App.SettingsContext.saveSettingsData;
            SaveSettings.saveGlobalSaveDataSettings(slotId);
            archipelagoClient._shouldDoInitialLoad = false;
        }
    }
}
