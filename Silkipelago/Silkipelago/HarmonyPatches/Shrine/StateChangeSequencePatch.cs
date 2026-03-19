using HarmonyLib;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Shrine
{
    [HarmonyPatch(typeof(StateChangeSequence))]
    [HarmonyPatch(nameof(StateChangeSequence.SetIsCompleteBool))]
    public static class StateChangeSequencePatch
    {
        public static void Postfix(StateChangeSequence __instance)
        {
            BasePatch.SafeExecuteVoid(() => HandleStateChange(__instance), nameof(StateChangeSequencePatch), nameof(Postfix));
        }

        private static void HandleStateChange(StateChangeSequence __instance)
        {
            BasePatch.Logger.LogInfo(__instance.isCompleteBool);
            if (PlayerDataIds.SHRINES.Contains(__instance.isCompleteBool))
            {
                var locationId = ArchipelagoLocationIds.GetArchipelagoName(__instance.isCompleteBool);
                ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(locationId);
            }
        }
    }
}
