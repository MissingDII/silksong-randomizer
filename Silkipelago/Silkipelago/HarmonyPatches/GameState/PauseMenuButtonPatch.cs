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
                // Check if we already added the button and remove it so we can create a fresh one
                var existingArchipelagoButton = controlsTransform.Find("ArchipelagoButton");
                if (existingArchipelagoButton != null)
                {
                    UnityEngine.Object.Destroy(existingArchipelagoButton.gameObject);
                }
                var firstChildTransform = controlsTransform.GetChild(0);
                var firstButtonRect = firstChildTransform.GetComponent<RectTransform>();
                // Clone the first button (gets all proper styling automatically)
                var archipelagoButtonGO = UnityEngine.Object.Instantiate(
                    firstChildTransform.gameObject,
                    controlsTransform,
                    worldPositionStays: false
                );
                archipelagoButtonGO.name = "ArchipelagoButton";

                // Update position to be below all existing buttons
                var newButtonRect = archipelagoButtonGO.GetComponent<RectTransform>();
                var buttonSpacing = firstButtonRect.sizeDelta.y + 10f;
                var yOffset = firstButtonRect.anchoredPosition.y - (buttonSpacing * controlsTransform.childCount);
                newButtonRect.anchoredPosition = new Vector2(firstButtonRect.anchoredPosition.x, yOffset);

                // Update the text to "Archipelago"
                var textComponent = archipelagoButtonGO.GetComponentInChildren<UnityEngine.UI.Text>();
                if (textComponent != null)
                {
                    textComponent.text = "Archipelago";
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError($"Error in PauseMenuButtonPatch: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}