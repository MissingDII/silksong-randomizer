using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;
using Silkipelago.HarmonyPatches.FsmGarbage;
using Silkipelago.HarmonyPatches.Item;
using Silkipelago.HarmonyPatches.Steam;

namespace Silkipelago.HarmonyPatches
{
    public class PatchInitializer
    {
        public PatchInitializer()
        {
        }

        public void InitializeEarlyPatches(ILogger logger, Harmony harmony)
        {
            SteamValidationPatch.Initialize(logger);
            PlayerDataPatch.Initialize(logger);
            FsmPatcher.Initialize(logger);
        }

        public void InitializeConnectedPatches(ILogger logger, Harmony harmony, SilksongArchipelagoClient archipelago, LocationChecker locationChecker)
        {
        }
    }
}
