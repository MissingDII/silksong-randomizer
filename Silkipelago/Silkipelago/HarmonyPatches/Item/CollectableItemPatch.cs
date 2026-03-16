using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Item
{
    [HarmonyPatch(typeof(CollectableItemManager))]
    [HarmonyPatch(nameof(CollectableItemManager.AddItem))]
    public static class CollectableItemPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        //   public static void AddItem(CollectableItem item, int amount = 1)
        public static bool Prefix(CollectableItemManager __instance, CollectableItem item, int amount)
        {
            try
            {
                Logger.LogDebugPatchIsRunning(nameof(CollectableItemManager), nameof(CollectableItemManager.AddItem), nameof(CollectableItemPatch), nameof(Prefix));
                if (CollectablesStrings.COLLECTABLESKEYS.Contains(item.name) || CollectablesStrings.ITEMS.Contains(item.name))
                {
                    var archipelagoLocationName = ArchipelagoLocationIds.GetArchipelagoName(item.name);
                    Logger.LogInfo("sending location for " + archipelagoLocationName);
                    ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(archipelagoLocationName);
                    Logger.LogInfo("sent location for " + archipelagoLocationName);
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }
                else
                {
                    return MethodPrefix.RUN_ORIGINAL_METHOD;
                }
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(PlayerDataPatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }
}
