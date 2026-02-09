using HarmonyLib;
using HutongGames.PlayMaker.Actions;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;

namespace Silkipelago.HarmonyPatches.Goal
{
    [HarmonyPatch(typeof(SetEndingCompleted), nameof(SetEndingCompleted.OnEnter))]
    public class EndingCompletedPatch
    {
        private static ILogger _logger;
        private static SilksongArchipelagoClient _silksongArchipelagoClient;
        private static SilksongLocationChecker _silksongLocationChecker;

        public static void Initialize(ILogger logger, SilksongArchipelagoClient silksongArchipelagoClient, SilksongLocationChecker silksongLocationChecker)
        {
            _logger = logger;
            _silksongArchipelagoClient = silksongArchipelagoClient;
            _silksongLocationChecker = silksongLocationChecker;
        }

        static void Prefix(SetEndingCompleted __instance)
        {
            //if(_silksongArchipelagoClient.SlotData.Goal.Equals(SilksongSlotData.Goal.GrandMotherSilk))
        }
    }

}
