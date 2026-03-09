using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;
using Silkipelago.Items;
using Silkipelago.Settings;

namespace Silkipelago.HarmonyPatches.Item
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch(nameof(GameManager.IsGameplayScene))]
    public static class GameManagerPatch
    {
        private static ILogger _logger;
        private static SilksongArchipelagoClient _silksongArchipelagoClient;

        public static void Initialize(ILogger logger, SilksongArchipelagoClient silksongArchipelagoClient)
        {
            _logger = logger;
            _silksongArchipelagoClient = silksongArchipelagoClient;
        }

        //   public bool IsGameplayScene()
        public static void Postfix(GameManager __instance, bool __result)
        {
            if (__result && _silksongArchipelagoClient._shouldDoInitialLoad)
            {
                // Do something when IsGameplayScene returns true
                _logger.LogDebug("Entering Gameplay Scene and loading items");
                var itemManager = SilksongItemManager.Instance;
                itemManager.ReceiveAllNewItems();
                var slotId = __instance.profileID;
                var saveSettingsData = ArchipelagoPlugin.App.SettingsContext.saveSettingsData;
                SaveSettings.saveGlobalSaveDataSettings(slotId);

                _silksongArchipelagoClient._shouldDoInitialLoad = false;
            }
        }
    }
}
