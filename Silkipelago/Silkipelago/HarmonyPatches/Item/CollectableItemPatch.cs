using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using Silkipelago.Constants;
using System;

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
