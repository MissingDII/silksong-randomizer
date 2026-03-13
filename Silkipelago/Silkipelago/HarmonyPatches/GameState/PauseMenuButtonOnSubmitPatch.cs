using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using System;
using UnityEngine.EventSystems;
using ILogger = KaitoKid.Utilities.Interfaces.ILogger;

namespace Silkipelago.HarmonyPatches.GameState
{
    [HarmonyPatch(typeof(UnityEngine.UI.PauseMenuButton))]
    [HarmonyPatch(nameof(UnityEngine.UI.PauseMenuButton.OnSubmit))]
    public static class PauseMenuButtonOnSubmitPatch
    {
        private static ILogger _logger;

        public static void Initialize(ILogger logger)
        {
            _logger = logger;
        }

        public static bool Prefix(UnityEngine.UI.PauseMenuButton __instance, BaseEventData eventData)
        {
            try
            {
                if (__instance.gameObject.name == "ArchipelagoButton")
                {
                    if (ArchipelagoPlugin.App?.UIContext?.MenuUI != null)
                    {
                        ArchipelagoPlugin.App.UIContext.MenuUI.shouldLaunchStartCutscene = false;
                        ArchipelagoPlugin.App.UIContext.MenuUI.Show();
                    }

                    return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
                }

                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
            catch (Exception ex)
            {
                _logger?.LogError($"Error in PauseMenuButtonOnSubmitPatch: {ex.Message}\n{ex.StackTrace}");
                return MethodPrefix.RUN_ORIGINAL_METHOD;
            }
        }
    }
}
