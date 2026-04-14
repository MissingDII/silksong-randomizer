using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using Silkipelago.Constants;
using UnityEngine.SceneManagement;

namespace Silkipelago.HarmonyPatches.Item
{
    [HarmonyPatch(typeof(CollectableItemManager))]
    [HarmonyPatch(nameof(CollectableItemManager.AddItem))]
    public static class CollectableItemPatch
    {
        //   public static void AddItem(CollectableItem item, int amount = 1)
        public static bool Prefix(CollectableItemManager __instance, CollectableItem item, int amount)
        {
            return BasePatch.SafeExecute(() => HandleAddItem(item), nameof(CollectableItemPatch), nameof(Prefix));
        }

        private static bool HandleAddItem(CollectableItem item)
        {
            BasePatch.Logger.LogDebugPatchIsRunning(nameof(CollectableItemManager), nameof(CollectableItemManager.AddItem), nameof(CollectableItemPatch), nameof(Prefix));
            if (item.name.Equals(CollectablesIds.MEMORY_LOCKET))
            {
                var scene = SceneManager.GetActiveScene().name;
                var locationChecker = ArchipelagoPlugin.App.LocationChecker;
                var inGameName = $"Crest-{scene}";
                var locationId = ArchipelagoLocationIds.GetArchipelagoName(inGameName);
                if (inGameName.Equals(CollectablesIds.MEMORY_LOCKET_FREY))
                {
                    if (locationChecker.IsLocationChecked(locationId))
                    {
                        //change memoryLocket id so that the other one is checked
                        inGameName = CollectablesIds.MEMORY_LOCKET_BELLHART_CEILING;
                    }
                }
                if (locationId != null && locationChecker.LocationExists(locationId))
                {
                    locationChecker.AddCheckedLocation(locationId);
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }
            }
            if (CollectablesIds.COLLECTABLESKEYS.Contains(item.name) || CollectablesIds.ITEMS.Contains(item.name))
            {
                var archipelagoLocationName = ArchipelagoLocationIds.GetArchipelagoName(item.name);
                BasePatch.Logger.LogInfo("sending location for " + archipelagoLocationName);
                ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(archipelagoLocationName);
                BasePatch.Logger.LogInfo("sent location for " + archipelagoLocationName);
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }
            else
            {
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }
}
