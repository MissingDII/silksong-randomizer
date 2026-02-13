using HarmonyLib;
using KaitoKid.ArchipelagoUtilities.Net.Client;
using Silkipelago.HarmonyPatches;
using Silkipelago.HarmonyPatches.NewGame;
using Silkipelago.Items;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ILogger = KaitoKid.Utilities.Interfaces.ILogger;
using UIImage = UnityEngine.UI.Image;
using UIText = UnityEngine.UI.Text;


namespace Silkipelago.Archipelago.UI
{
    public static class ArchipelagoMenuUI
    {
        private static Canvas _canvas;
        private static bool _visible;
        private static ILogger _logger;
        private static Harmony _harmony;
        private static SilksongArchipelagoClient _archipelagoClient;
        private static SilksongLocationChecker _silksongLocationChecker;
        private static ClickOnlyInputField _hostname;
        private static ClickOnlyInputField _port;
        private static ClickOnlyInputField _slot;
        private static ArchipelagoConnectionInfo APConnectionInfo { get; set; }

        // ---------- Public API ----------

        public static void Init(ILogger logger, Harmony harmony, SilksongArchipelagoClient archipelagoClient, SilksongLocationChecker silksongLocationChecker)
        {
            if (_canvas != null)
                return; // already initialized
            _logger = logger;
            _harmony = harmony;
            _archipelagoClient = archipelagoClient;
            _silksongLocationChecker = silksongLocationChecker;
            CreateUI();
            Hide();
        }

        public static void Toggle()
        {
            if (_canvas == null)
                return;

            if (_visible) Hide();
            else Show();
        }

        public static void UpdateCursorState()
        {
            if (_visible)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        // ---------- Visibility ----------

        private static void Show()
        {
            _canvas.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _visible = true;
            SelectionGuard.Instance?.EnableGuard();
            _logger.LogInfo("Menu shown");
        }

        private static void Hide()
        {
            SelectionGuard.Instance?.DisableGuard();
            _canvas.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _visible = false;
            _logger.LogInfo("Menu hidden");
        }

        // ---------- UI Creation ----------

        private static void CreateUI()
        {
            EnsureEventSystem();

            var canvasGO = new GameObject("ArchipelagoCanvas");
            _canvas = canvasGO.AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _canvas.sortingOrder = 10000;

            canvasGO.AddComponent<CanvasScaler>().uiScaleMode =
                CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasGO.AddComponent<GraphicRaycaster>();

            var panel = CreatePanel(canvasGO.transform);

            CreateText(
                panel.transform,
                "Please enter your Archipelago\nconnection information",
                new Vector2(0, 110),
                20,
                TextAnchor.MiddleCenter
            );

            float y = 40;

            var defaultHostname = "archipelago.gg";
            var defaultPort = "00000";
            var defaultSlotName = "";
#if DEBUG
            defaultHostname = "localhost";
            defaultPort = "38281";
            defaultSlotName = "Hornet";
#endif

            CreateLabeledInput(panel.transform, "Hostname:", y, defaultHostname, out _hostname);
            CreateLabeledInput(panel.transform, "Port:", y - 45, defaultPort, out _port);
            CreateLabeledInput(panel.transform, "Slot Name:", y - 90, defaultSlotName, out _slot);

            // Set up tab navigation between input fields
            SetupTabNavigation(_hostname, _port, _slot);

            CreateButton(
                panel.transform,
                "Connect to Archipelago",
                new Vector2(0, -140),
                OnConnectClicked
            );
        }

        // ---------- Panel ----------

        private static GameObject CreatePanel(Transform parent)
        {
            var panel = new GameObject("Panel");
            panel.transform.SetParent(parent, false);

            var img = panel.AddComponent<UIImage>();
            img.color = new Color(0.05f, 0.05f, 0.05f, 0.85f);

            panel.AddComponent<CanvasGroup>();

            var rt = panel.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(520, 360);
            rt.anchoredPosition = Vector2.zero;

            return panel;
        }

        // ---------- UI Elements ----------

        private static void CreateLabeledInput(
            Transform parent,
            string label,
            float y,
            string defaultValue,
            out ClickOnlyInputField input)
        {
            // Create label with appropriate sizing
            var labelText = CreateText(parent, label, new Vector2(-170, y), 16, TextAnchor.MiddleLeft);
            labelText.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 32);

            var fieldGO = new GameObject(label + "Input");
            fieldGO.transform.SetParent(parent, false);

            var bg = fieldGO.AddComponent<UIImage>();
            bg.color = new Color(0.15f, 0.15f, 0.15f, 1f);

            var rt = fieldGO.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(260, 32);
            rt.anchoredPosition = new Vector2(60, y);

            input = fieldGO.AddComponent<ClickOnlyInputField>();
            fieldGO.AddComponent<InputFieldSelectionFix>();
            input.contentType = ClickOnlyInputField.ContentType.Standard;

            input.lineType = ClickOnlyInputField.LineType.SingleLine;

            var nav = input.navigation;
            nav.mode = Navigation.Mode.None;
            input.navigation = nav;

            // Create input text display
            var textGO = new GameObject("Text");
            textGO.transform.SetParent(fieldGO.transform, false);

            var text = textGO.AddComponent<UIText>();
            text.text = defaultValue;
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = 14;
            text.alignment = TextAnchor.MiddleLeft;
            text.color = new Color(0.9f, 0.85f, 0.75f);
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.verticalOverflow = VerticalWrapMode.Truncate;

            var textRt = text.GetComponent<RectTransform>();
            textRt.anchorMin = Vector2.zero;
            textRt.anchorMax = Vector2.one;
            textRt.offsetMin = new Vector2(8, 0);
            textRt.offsetMax = new Vector2(-8, 0);

            input.textComponent = text;

            // Create placeholder
            var placeholderGO = new GameObject("Placeholder");
            placeholderGO.transform.SetParent(fieldGO.transform, false);

            var placeholder = placeholderGO.AddComponent<UIText>();
            placeholder.text = "Enter " + label.ToLower().Replace(":", "");
            placeholder.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            placeholder.fontSize = 14;
            placeholder.fontStyle = FontStyle.Italic;
            placeholder.alignment = TextAnchor.MiddleLeft;
            placeholder.color = new Color(0.6f, 0.6f, 0.6f, 0.6f);
            placeholder.horizontalOverflow = HorizontalWrapMode.Overflow;
            placeholder.verticalOverflow = VerticalWrapMode.Truncate;

            var placeholderRt = placeholder.GetComponent<RectTransform>();
            placeholderRt.anchorMin = Vector2.zero;
            placeholderRt.anchorMax = Vector2.one;
            placeholderRt.offsetMin = new Vector2(8, 0);
            placeholderRt.offsetMax = new Vector2(-8, 0);

            input.placeholder = placeholder;

            // Add click-only selection handler
            fieldGO.AddComponent<ClickToSelectInputField>().Setup(input);
        }

        private static void SetupTabNavigation(ClickOnlyInputField first, ClickOnlyInputField second, ClickOnlyInputField third)
        {
            // Add custom Tab handler component to each field
            var ClickOnlyInputFields = new List<ClickOnlyInputField> { first, second, third };

            first.gameObject.AddComponent<TabNavigationHandler>().Setup(ClickOnlyInputFields, 0);
            second.gameObject.AddComponent<TabNavigationHandler>().Setup(ClickOnlyInputFields, 1);
            third.gameObject.AddComponent<TabNavigationHandler>().Setup(ClickOnlyInputFields, 2);
        }

        private static UIText CreateText(
            Transform parent,
            string content,
            Vector2 pos,
            int size,
            TextAnchor anchor)
        {
            var go = new GameObject("Text");
            go.transform.SetParent(parent, false);

            var txt = go.AddComponent<UIText>();
            txt.text = content;
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            txt.fontSize = size;
            txt.fontStyle = FontStyle.Normal;
            txt.alignment = anchor;
            txt.color = new Color(0.9f, 0.85f, 0.75f);
            txt.horizontalOverflow = HorizontalWrapMode.Wrap;
            txt.verticalOverflow = VerticalWrapMode.Overflow;

            var rt = txt.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(400, 80);
            rt.anchoredPosition = pos;

            return txt;
        }

        private static void CreateButton(
            Transform parent,
            string label,
            Vector2 pos,
            System.Action onClick)
        {
            var btnGO = new GameObject("Button");
            btnGO.transform.SetParent(parent, false);

            var img = btnGO.AddComponent<UIImage>();
            img.color = new Color(0.25f, 0.22f, 0.18f);

            var btn = btnGO.AddComponent<Button>();
            btn.onClick.AddListener(() => onClick());
            btn.navigation = new Navigation { mode = Navigation.Mode.None };

            var rt = btnGO.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(280, 42);
            rt.anchoredPosition = pos;

            // Create button text with proper sizing
            var textGO = new GameObject("Text");
            textGO.transform.SetParent(btnGO.transform, false);

            var txt = textGO.AddComponent<UIText>();
            txt.text = label;
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            txt.fontSize = 14;
            txt.alignment = TextAnchor.MiddleCenter;
            txt.color = new Color(0.9f, 0.85f, 0.75f);
            txt.horizontalOverflow = HorizontalWrapMode.Overflow;
            txt.verticalOverflow = VerticalWrapMode.Overflow;

            var textRt = textGO.GetComponent<RectTransform>();
            textRt.anchorMin = Vector2.zero;
            textRt.anchorMax = Vector2.one;
            textRt.offsetMin = Vector2.zero;
            textRt.offsetMax = Vector2.zero;
        }

        // ---------- Infrastructure ----------

        private static void EnsureEventSystem()
        {
            var existingSystem = UnityEngine.Object.FindFirstObjectByType<EventSystem>();
            if (existingSystem != null)
            {
                if (existingSystem != null)
                {
                    if (existingSystem.GetComponent<SelectionGuard>() == null)
                        existingSystem.gameObject.AddComponent<SelectionGuard>();
                    return;
                }

                return;
            }

            var eventSystemGO = new GameObject("EventSystem");

            var eventSystem = eventSystemGO.AddComponent<EventSystem>();

            eventSystemGO.AddComponent<StandaloneInputModule>();
            eventSystemGO.AddComponent<SelectionGuard>();
        }



        // ---------- Actions ----------

        private static void OnConnectClicked()
        {
            _logger.LogInfo(
                $"Connect requested: {_hostname.text}:{_port.text} ({_slot.text})"
            );

            if (!int.TryParse(_port.text, out var port))
            {
                _logger.LogError("Port must be a valid number");
                return;
            }

            APConnectionInfo = new ArchipelagoConnectionInfo(_hostname.text, port, _slot.text, false);
            ConnectToArchipelago(() => InitializeAfterConnection());
            Hide();
            StartNewGamePatch.SkipMenuNextCall();
            UIManager.instance.StartNewGame();
        }

        private static void InitializeAfterConnection()
        {
            var locationChecker = SilksongLocationChecker.Instance;
            var itemManager = SilksongItemManager.Instance;
            var archipelago = SilksongArchipelagoClient.Instance;

            locationChecker.VerifyNewLocationChecksWithArchipelago();
            locationChecker.SendAllLocationChecks();
            itemManager.ReceiveAllNewItems();
            var patchInitializer = new PatchInitializer();
            patchInitializer.InitializeConnectedPatches(_logger, _harmony, _archipelagoClient, _silksongLocationChecker);
        }

        private static void ConnectToArchipelago(Action actionAfterConnection)
        {
            var archipelago = SilksongArchipelagoClient.Instance;

            if (APConnectionInfo == null)
            {
                _logger.LogMessage($"Tried to connect, but no information provided!");
                return;
            }

            if (archipelago.IsConnected)
            {
                _logger.LogMessage($"Tried to connect, but already connected!");
                return;
            }

            var connectionResult = archipelago.ConnectToMultiworld(APConnectionInfo);
            if (!connectionResult.Success || !archipelago.IsConnected)
            {
                APConnectionInfo = null;
                var userMessage =
                    $"Could not connect to archipelago.{Environment.NewLine}Message: {connectionResult.Message}{Environment.NewLine}Please verify the connection info and that the server is available.{Environment.NewLine}";
                _logger.LogError(userMessage);
                return;
            }

            _logger.LogMessage($"Connected to Archipelago as {archipelago.SlotData.SlotName}.");
            actionAfterConnection?.Invoke();
        }
    }
}
