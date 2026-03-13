using GlobalEnums;
using HarmonyLib;
using Silkipelago.Archipelago;
using System;
using UnityEngine;
using ILogger = KaitoKid.Utilities.Interfaces.ILogger;

namespace Silkipelago.HarmonyPatches.GameState
{
    [HarmonyPatch(typeof(UIManager))]
    [HarmonyPatch(nameof(UIManager.SetMenuState))]
    public static class PauseMenuButtonPatch
    {
        private static ILogger _logger;
        private static SilksongArchipelagoClient _archipelago;

        public static void Initialize(ILogger logger, SilksongArchipelagoClient archipelago)
        {
            _logger = logger;
            _archipelago = archipelago;
        }

        public static void Postfix(UIManager __instance, MainMenuState newState)
        {
            try
            {
                if (newState != MainMenuState.PAUSE_MENU)
                {
                    return;
                }
                var pauseMenuScreen = __instance.pauseMenuScreen;
                var menuObject = pauseMenuScreen.gameObject;
                var containerTransform = menuObject.transform.Find("Container");
                var controlsTransform = containerTransform.Find("Controls");

                var firstChildTransform = controlsTransform.GetChild(0);
                var firstButtonRect = firstChildTransform.GetComponent<RectTransform>();
                var buttonSpacing = firstButtonRect.sizeDelta.y + 10f;

                // Remove existing custom buttons
                var existingArchipelagoButton = controlsTransform.Find("ArchipelagoButton");
                if (existingArchipelagoButton != null)
                {
                    UnityEngine.Object.Destroy(existingArchipelagoButton.gameObject);
                }

                var existingToggleAct3Button = controlsTransform.Find("ToggleAct3Button");
                if (existingToggleAct3Button != null)
                {
                    UnityEngine.Object.Destroy(existingToggleAct3Button.gameObject);
                }

                // Create Archipelago button
                CreateCustomButton(
                    firstChildTransform,
                    controlsTransform,
                    "ArchipelagoButton",
                    "Archipelago",
                    firstButtonRect,
                    buttonSpacing,
                    controlsTransform.childCount
                );

                // Create Toggle Act 3 button (positioned below Archipelago button)
                CreateCustomButton(
                    firstChildTransform,
                    controlsTransform,
                    "ToggleAct3Button",
                    "Toggle Act 3",
                    firstButtonRect,
                    buttonSpacing,
                    controlsTransform.childCount
                );
            }
            catch (Exception ex)
            {
                _logger?.LogError($"Error in PauseMenuButtonPatch: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private static void CreateCustomButton(
            Transform templateTransform,
            Transform parentTransform,
            string buttonName,
            string buttonText,
            RectTransform templateRect,
            float buttonSpacing,
            int currentChildCount)
        {
            // Clone the button
            var buttonGO = UnityEngine.Object.Instantiate(
                templateTransform.gameObject,
                parentTransform,
                worldPositionStays: false
            );
            buttonGO.name = buttonName;

            // Update position to be below all existing buttons
            var buttonRect = buttonGO.GetComponent<RectTransform>();
            var yOffset = templateRect.anchoredPosition.y - (buttonSpacing * currentChildCount);
            buttonRect.anchoredPosition = new Vector2(templateRect.anchoredPosition.x, yOffset);

            // Update the text
            var textComponent = buttonGO.GetComponentInChildren<UnityEngine.UI.Text>();
            if (textComponent != null)
            {
                textComponent.text = buttonText;
            }
        }
    }
}