using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Settings;

namespace Silkipelago.HarmonyPatches.Item
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch(nameof(GameManager.IsGameplayScene))]
    public static class GameManagerPatch
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        //   public bool IsGameplayScene()
        public static void Postfix(GameManager __instance, bool __result)
        {
            if (__result && ArchipelagoPlugin.App.ArchipelagoClient._shouldDoInitialLoad)
            {
                // Do something when IsGameplayScene returns true
                _logger.LogDebug("Entering Gameplay Scene and loading items");
                var itemManager = ArchipelagoPlugin.App.ItemManager;
                itemManager.ReceiveAllNewItems();
                var slotId = __instance.profileID;
                var saveSettingsData = ArchipelagoPlugin.App.SettingsContext.saveSettingsData;
                SaveSettings.saveGlobalSaveDataSettings(slotId);

                ArchipelagoPlugin.App.ArchipelagoClient._shouldDoInitialLoad = false;
            }
        }
    }
}
