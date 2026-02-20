using HarmonyLib;
using Silkipelago.Archipelago;
using Silkipelago.HarmonyPatches.GameState;
using Silkipelago.HarmonyPatches.Item;
using Silkipelago.HarmonyPatches.NewGame;
using Silkipelago.HarmonyPatches.SaveUtility;
using Silkipelago.HarmonyPatches.Steam;
using Silkipelago.HarmonyPatches.Unity;
using ILogger = KaitoKid.Utilities.Interfaces.ILogger;

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
            SaveSerializePatch.Initialize(logger);
            UnityConverterInitializerPatch.Initialize(logger);
            LoadGamePatch.Initialize(logger);
            SaveGamePatch.Initialize(logger);
            GmStartNewGamePatch.Initialize(logger);
        }

        public void InitializeEarlyPatchesWithArchipelagoData(ILogger logger, Harmony harmony, SilksongArchipelagoClient archipelago, SilksongLocationChecker locationChecker)
        {
            PlayerDataPatch.Initialize(logger, archipelago, locationChecker);
            EndingCompletedPatch.Initialize(logger, archipelago, locationChecker);
            UIStartNewGamePatch.Initialize(logger, harmony, archipelago, locationChecker);
            GameManagerPatch.Initialize(logger, archipelago);

        }


        public void InitializeConnectedPatches(ILogger logger, Harmony harmony, SilksongArchipelagoClient archipelago, SilksongLocationChecker locationChecker)
        {

        }
    }
}
