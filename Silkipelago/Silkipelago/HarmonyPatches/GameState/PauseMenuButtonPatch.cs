using GlobalEnums;
using HarmonyLib;
using UnityEngine;

namespace Silkipelago.HarmonyPatches.GameState
{
    [HarmonyPatch(typeof(UIManager))]
    [HarmonyPatch(nameof(UIManager.SetMenuState))]
    public static class PauseMenuButtonPatch
    {
        private struct ButtonConfig
        {
            public Transform TemplateTransform { get; set; }
            public RectTransform TemplateRect { get; set; }
            public float ButtonSpacing { get; set; }
            public int StartChildIndex { get; set; }
        }

        public static void Postfix(UIManager __instance, MainMenuState newState)
        {
            BasePatch.SafeExecuteVoid(() => HandleSetMenuState(__instance, newState), nameof(PauseMenuButtonPatch), nameof(Postfix));
        }

        private static void HandleSetMenuState(UIManager __instance, MainMenuState newState)
        {
            if (newState != MainMenuState.PAUSE_MENU)
                return;

            if (!TryGetControlsTransform(__instance, out var controlsTransform))
                return;

            var buttonConfig = GetButtonConfiguration(controlsTransform);
            RemoveExistingCustomButtons(controlsTransform);
            CreateArchipelagoButtons(controlsTransform, ref buttonConfig);
        }

        private static bool TryGetControlsTransform(UIManager uiManager, out Transform controlsTransform)
        {
            try
            {
                var pauseMenuScreen = uiManager.pauseMenuScreen;
                var menuObject = pauseMenuScreen.gameObject;
                var containerTransform = menuObject.transform.Find("Container");
                controlsTransform = containerTransform?.Find("Controls");
                return controlsTransform != null;
            }
            catch
            {
                controlsTransform = null;
                return false;
            }
        }

        private static ButtonConfig GetButtonConfiguration(Transform controlsTransform)
        {
            var firstChildTransform = controlsTransform.GetChild(0);
            var firstButtonRect = firstChildTransform.GetComponent<RectTransform>();
            var buttonSpacing = firstButtonRect.sizeDelta.y + 10f;

            return new ButtonConfig
            {
                TemplateTransform = firstChildTransform,
                TemplateRect = firstButtonRect,
                ButtonSpacing = buttonSpacing,
                StartChildIndex = controlsTransform.childCount
            };
        }

        private static void RemoveExistingCustomButtons(Transform controlsTransform)
        {
            RemoveButtonIfExists(controlsTransform, "ArchipelagoButton");
            RemoveButtonIfExists(controlsTransform, "ToggleAct3Button");
        }

        private static void RemoveButtonIfExists(Transform parent, string buttonName)
        {
            var existing = parent.Find(buttonName);
            if (existing != null)
                UnityEngine.Object.Destroy(existing.gameObject);
        }

        private static void CreateArchipelagoButtons(Transform controlsTransform, ref ButtonConfig config)
        {
            var templateTransform = config.TemplateTransform;
            var templateRect = config.TemplateRect;
            var buttonSpacing = config.ButtonSpacing;
            var startIndex = config.StartChildIndex;

            CreateCustomButton(
                templateTransform,
                controlsTransform,
                "ArchipelagoButton",
                "Archipelago",
                templateRect,
                buttonSpacing,
                startIndex
            );

            CreateCustomButton(
                templateTransform,
                controlsTransform,
                "ToggleAct3Button",
                "Toggle Act 3",
                templateRect,
                buttonSpacing,
                startIndex + 1
            );
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