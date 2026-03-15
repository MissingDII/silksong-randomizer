using HarmonyLib;
using Silkipelago.Archipelago;
using Silkipelago.HarmonyPatches.FSM;
using Silkipelago.HarmonyPatches.GameState;
using Silkipelago.HarmonyPatches.Item;
using Silkipelago.HarmonyPatches.NewGame;
using Silkipelago.HarmonyPatches.SaveUtility;
using Silkipelago.HarmonyPatches.Shrine;
using Silkipelago.HarmonyPatches.Steam;
using Silkipelago.HarmonyPatches.Tools;
using Silkipelago.HarmonyPatches.Unity;
using Silkipelago.Items;
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
            CollectableItemPatch.Initialize(logger);
            StateChangeSequencePatch.Initialize(logger);
            ToolItemPatch.Initialize(logger);
            ToolItemAlternatePatch.Initialize(logger);
            ToolEquipPatch.Initialize(logger);
            SkillMessagePatch.Initialize(logger);
            SavaDataSetToNullHook.Initialize(logger);
            ShrineBellHandler.Initialize(logger);
            HealthManagerDiePatch.Initialize(logger);
            SharedUtilPatch.Initialize(logger);
            FSMUtilityPatch.Initialize(logger);
        }

        public void InitializeEarlyPatchesWithArchipelagoData(ILogger logger, Harmony harmony, SilksongArchipelagoClient archipelago, SilksongLocationChecker locationChecker)
        {
            PlayerDataPatch.Initialize(logger, archipelago, locationChecker);
            EndingCompletedPatch.Initialize(logger, archipelago, locationChecker);
            UIStartNewGamePatch.Initialize(logger, harmony, archipelago, locationChecker);
            GameManagerPatch.Initialize(logger);
            PauseMenuButtonPatch.Initialize(logger, archipelago);
            PauseMenuButtonOnSubmitPatch.Initialize(logger);
        }


        public void InitializeConnectedPatches(ILogger logger, Harmony harmony, SilksongArchipelagoClient archipelago, SilksongLocationChecker locationChecker)
        {

        }
    }
}
