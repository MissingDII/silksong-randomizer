using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago.ItemHandlers;

namespace Silkipelago.HarmonyPatches.Item
{
    [HarmonyPatch(typeof(PlayerData))]
    [HarmonyPatch(nameof(PlayerData.SetBool))]
    public static class PlayerDataPatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        // public void SetBool(string boolName, bool value)
        public static bool Prefix(PlayerData __instance, string boolName, bool value)
        {
            return PlayerDataPatchHelper.ExecutePatchLogic(nameof(PlayerDataPatch), nameof(Prefix), () =>
            {
                Logger.LogInfo(boolName);
                Logger.LogDebugPatchIsRunning(nameof(PlayerData), nameof(PlayerData.SetBool), nameof(PlayerDataPatch), nameof(Prefix));

                if (SilksongItemManager.ItemToReceive == 0)
                {
                    return PlayerDataPatchHelper.HandlePlayerDataFieldChange(boolName, ArchipelagoPlugin.App.LocationChecker);
                }

                SilksongItemManager.ItemToReceive--;
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }, MethodPrefix.RUN_ORIGINAL_METHOD);
        }


    }

    [HarmonyPatch(typeof(PlayerData))]
    [HarmonyPatch(nameof(PlayerData.SetInt))]
    public static class PlayerDataPatchInt
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;

        // public void SetBool(string boolName, bool value)
        public static bool Prefix(PlayerData __instance, string intName, int value)
        {
            return PlayerDataPatchHelper.ExecutePatchLogic(nameof(PlayerDataPatch), nameof(Prefix), () =>
            {
                Logger.LogDebugPatchIsRunning(nameof(PlayerData), nameof(PlayerData.SetBool), nameof(PlayerDataPatch), nameof(Prefix));

                if (SilksongItemManager.ItemToReceive == 0)
                {
                    return PlayerDataPatchHelper.HandlePlayerDataFieldChange(intName, ArchipelagoPlugin.App.LocationChecker);
                }

                SilksongItemManager.ItemToReceive--;
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }, MethodPrefix.RUN_ORIGINAL_METHOD);
        }


    }
}
