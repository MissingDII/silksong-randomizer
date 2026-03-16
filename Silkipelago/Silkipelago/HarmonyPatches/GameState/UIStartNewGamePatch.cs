using GlobalEnums;
using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using KaitoKid.Utilities.Interfaces;
using Silkipelago.Settings;
using System;
using System.Collections;

namespace Silkipelago.HarmonyPatches.NewGame
{
    [HarmonyPatch(typeof(UIManager))]
    [HarmonyPatch(nameof(UIManager.StartNewGame))]
    public static class UIStartNewGamePatch
    {
        private static ILogger Logger => ArchipelagoPlugin.App.Logger;
        private static bool _shouldShowArchipelagoMenu = true;

        public static void SkipMenuNextCall()
        {
            _shouldShowArchipelagoMenu = false;
        }

        //  public void StartNewGame(bool permaDeath = false, bool bossRush = false)
        public static bool Prefix(UIManager __instance, bool permaDeath, bool bossRush)
        {
            try
            {
                Logger.LogDebugPatchIsRunning(nameof(UIManager), nameof(UIManager.StartNewGame), nameof(UIStartNewGamePatch), nameof(Prefix));
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
                Logger.LogErrorException(nameof(UIStartNewGamePatch), nameof(Prefix), ex);
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
            ArchipelagoPlugin.App.UIContext.MenuUI.shouldLaunchStartCutscene = true;
            var returnButtonshown = false;
            ArchipelagoPlugin.App.UIContext.MenuUI.InitUI(returnButtonshown);
            ArchipelagoPlugin.App.UIContext.MenuUI.Toggle();
        }
    }
}
