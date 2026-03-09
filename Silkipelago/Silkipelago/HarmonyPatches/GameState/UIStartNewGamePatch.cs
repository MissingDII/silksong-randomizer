using GlobalEnums;
using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Archipelago;
using Silkipelago.Settings;
using System;
using System.Collections;

namespace Silkipelago.HarmonyPatches.NewGame
{
    [HarmonyPatch(typeof(UIManager))]
    [HarmonyPatch(nameof(UIManager.StartNewGame))]
    public static class UIStartNewGamePatch
    {
        private static ILogger _logger;
        private static Harmony _harmony;
        private static SilksongArchipelagoClient _archipelago;
        private static SilksongLocationChecker _locationChecker;
        private static bool _shouldShowArchipelagoMenu = true;

        public static void Initialize(ILogger logger, Harmony harmony, SilksongArchipelagoClient silksongArchipelagoClient, SilksongLocationChecker silksongLocationChecker)
        {
            _logger = logger;
            _harmony = harmony;
            _archipelago = silksongArchipelagoClient;
            _locationChecker = silksongLocationChecker;
        }

        public static void SkipMenuNextCall()
        {
            _shouldShowArchipelagoMenu = false;
        }

        //  public void StartNewGame(bool permaDeath = false, bool bossRush = false)
        public static bool Prefix(UIManager __instance, bool permaDeath, bool bossRush)
        {
            try
            {
                _logger.LogDebugPatchIsRunning(nameof(UIManager), nameof(UIManager.StartNewGame), nameof(UIStartNewGamePatch), nameof(Prefix));
                if (_shouldShowArchipelagoMenu)
                {
                    __instance.StartCoroutine(HideMenusAndShowArchipelagoUI(__instance));
                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }
                _shouldShowArchipelagoMenu = true;
                // Allow the original method to run on subsequent calls
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                _logger.LogErrorException(nameof(UIStartNewGamePatch), nameof(Prefix), ex);
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }

        private static IEnumerator HideMenusAndShowArchipelagoUI(UIManager uiManager)
        {
            if (uiManager.menuState == MainMenuState.SAVE_PROFILES)
                yield return uiManager.StartCoroutine(uiManager.HideSaveProfileMenu(false));
            else
                yield return uiManager.StartCoroutine(uiManager.HideCurrentMenu());
            SaveSettings.ClearSaveData(uiManager.gm.profileID);
            ArchipelagoPlugin.App.SettingsContext.saveSettingsData = new SaveSettingsData();
            ArchipelagoPlugin.App.UIContext.MenuUI.InitUI();
            ArchipelagoPlugin.App.UIContext.MenuUI.Toggle();
        }
    }
}
