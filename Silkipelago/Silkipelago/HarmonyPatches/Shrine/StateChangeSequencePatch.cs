using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Constants;
using System;

namespace Silkipelago.HarmonyPatches.Shrine
{
    [HarmonyPatch(typeof(StateChangeSequence))]
    [HarmonyPatch(nameof(StateChangeSequence.SetIsCompleteBool))]
    public static class StateChangeSequencePatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;
        public static void Postfix(StateChangeSequence __instance)
        {
            try
            {
                Logger.LogInfo(__instance.isCompleteBool);
                if (PlayerDataIds.SHRINES.Contains(__instance.isCompleteBool))
                {
                    var locationId = ArchipelagoLocationIds.GetArchipelagoName(__instance.isCompleteBool);
                    ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(locationId);
                }
            }
            catch (Exception ex)
            {
                Logger.LogErrorException(nameof(StateChangeSequencePatch), nameof(Postfix), ex);
            }
        }
    }
}
