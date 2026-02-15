using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;
using Silkipelago.Items;

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
        public static void Postfix(bool __result)
        {
            if (__result && _silksongArchipelagoClient._shouldDoInitialLoad)
            {
                // Do something when IsGameplayScene returns true
                _logger.LogDebug("Entering Gameplay Scene and loading items");
                var instance = SilksongItemManager.Instance;
                instance.ReceiveAllNewItems();
                _silksongArchipelagoClient._shouldDoInitialLoad = false;
            }
        }
    }
}
