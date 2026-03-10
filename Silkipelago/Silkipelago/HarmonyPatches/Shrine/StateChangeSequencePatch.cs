using HarmonyLib;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Constants;

namespace Silkipelago.HarmonyPatches.Shrine
{
    [HarmonyPatch(typeof(StateChangeSequence))]
    [HarmonyPatch(nameof(StateChangeSequence.SetIsCompleteBool))]
    public static class StateChangeSequencePatch
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }
        public static void Postfix(StateChangeSequence __instance)
        {
            _logger.LogInfo(__instance.isCompleteBool);
            if (PlayerDataStrings.SHRINES.Contains(__instance.isCompleteBool))
            {
                var archipelagoItemName = ArchipelagoIds.GetArchipelagoName(__instance.isCompleteBool);
                ArchipelagoPlugin.App.LocationChecker.AddCheckedLocation(archipelagoItemName);
            }
        }
    }
}
