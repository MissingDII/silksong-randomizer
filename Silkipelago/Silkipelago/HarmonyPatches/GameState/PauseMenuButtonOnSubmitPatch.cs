using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Constants;
using UnityEngine.EventSystems;

namespace Silkipelago.HarmonyPatches.GameState
{
    /// <summary>
    /// Handles custom pause menu button interactions for Archipelago.
    /// </summary>
    [HarmonyPatch(typeof(UnityEngine.UI.PauseMenuButton))]
    [HarmonyPatch(nameof(UnityEngine.UI.PauseMenuButton.OnSubmit))]
    public static class PauseMenuButtonOnSubmitPatch
    {
        /// <summary>
        /// Prefix that intercepts pause menu button submissions.
        /// </summary>
        public static bool Prefix(UnityEngine.UI.PauseMenuButton __instance, BaseEventData eventData)
        {
            return BasePatch.SafeExecute(
                () => HandlePauseMenuButtonSubmit(__instance),
                nameof(PauseMenuButtonOnSubmitPatch),
                nameof(Prefix)
            );
        }

        private static bool HandlePauseMenuButtonSubmit(UnityEngine.UI.PauseMenuButton button)
        {
            if (button.gameObject.name == "ArchipelagoButton")
            {
                if (ArchipelagoPlugin.App?.UIContext?.MenuUI != null)
                {
                    ArchipelagoPlugin.App.UIContext.MenuUI.shouldLaunchStartCutscene = false;
                    ArchipelagoPlugin.App.UIContext.MenuUI.Show();
                }
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }

            if (button.gameObject.name == "ToggleAct3Button")
            {
                PlayerData.instance.blackThreadWorld = !PlayerData.instance.blackThreadWorld;
                BasePatch.Logger.LogInfo("Act3 toggled, reload a new room for change to take place");
                return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
            }

            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }
}
