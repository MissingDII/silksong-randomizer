using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UIImage = UnityEngine.UI.Image;
using UIText = UnityEngine.UI.Text;

namespace Silkipelago.Archipelago.UI
{
    public static class ArchipelagoMenuBuilder
    {
        public static Canvas BuildUI(
            Action onConnectClicked,
            out ClickOnlyInputField hostInput,
            out ClickOnlyInputField portInput,
            out ClickOnlyInputField slotInput)
        {
            EnsureEventSystem();

            var canvasGO = new GameObject("ArchipelagoCanvas");
            var canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 1000;

            canvasGO.AddComponent<CanvasScaler>().uiScaleMode =
                CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasGO.AddComponent<GraphicRaycaster>();

            // Ensure SelectionGuard exists
            EnsureSelectionGuard();

            // Root panel
            var panel = CreatePanel(canvas.transform);

            float y = 110;

            hostInput = CreateLabeledInput(panel, "Host", new Vector2(0, y));
            y -= 55;

            portInput = CreateLabeledInput(panel, "Port", new Vector2(0, y));
            y -= 55;

            slotInput = CreateLabeledInput(panel, "Slot", new Vector2(0, y));
            y -= 55;


            CreateButton(
                panel,
                "Connect",
                new Vector2(0, y),
                onConnectClicked
            );

            SetupTabNavigation(hostInput, portInput, slotInput);

            return canvas;
        }

        // ---------- PANEL ----------

        private static Transform CreatePanel(Transform parent)
        {
            var go = new GameObject("Panel");
            go.transform.SetParent(parent, false);

            var rt = go.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(460, 420);
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);

            var img = go.AddComponent<UIImage>();
            img.color = ArchipelagoUIStyle.SilksongBackground;

            ArchipelagoUIStyle.CreateCornerOrnamentalFrame(go.transform);

            return go.transform;
        }

        // ---------- TEXT ----------

        private static void CreateText(
            Transform parent,
            string label,
            Vector2 pos,
            int size,
            TextAnchor anchor)
        {
            var go = new GameObject(label + "_Label");
            go.transform.SetParent(parent, false);

            var txt = go.AddComponent<UIText>();
            txt.text = label;
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            txt.fontSize = size;
            txt.alignment = anchor;
            txt.color = ArchipelagoUIStyle.SilksongTextPrimary;

            var rt = go.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(380, 26);
            rt.anchoredPosition = pos;
        }

        // ---------- INPUT ----------

        private static ClickOnlyInputField CreateLabeledInput(
            Transform parent,
            string label,
            Vector2 pos)
        {
            CreateText(parent, label, pos + new Vector2(0, 22), 14, TextAnchor.MiddleLeft);

            var go = new GameObject(label + "_Input");
            go.transform.SetParent(parent, false);

            var rt = go.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(380, 30);
            rt.anchoredPosition = pos;

            var img = go.AddComponent<UIImage>();
            img.color = new Color(0.12f, 0.11f, 0.10f, 1f);

            var input = go.AddComponent<ClickOnlyInputField>();

            var textGO = new GameObject("Text");
            textGO.transform.SetParent(go.transform, false);

            var txt = textGO.AddComponent<UIText>();
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            txt.fontSize = 14;
            txt.color = ArchipelagoUIStyle.SilksongTextSecondary;
            txt.alignment = TextAnchor.MiddleLeft;

            var textRT = textGO.GetComponent<RectTransform>();
            textRT.anchorMin = Vector2.zero;
            textRT.anchorMax = Vector2.one;
            textRT.offsetMin = new Vector2(8, 0);
            textRT.offsetMax = new Vector2(-8, 0);

            input.textComponent = txt;

            var placeholderGO = new GameObject("Placeholder");
            placeholderGO.transform.SetParent(go.transform, false);

            var ph = placeholderGO.AddComponent<UIText>();
            ph.text = label;
            ph.font = txt.font;
            ph.fontSize = 14;
            ph.color = new Color(0.6f, 0.55f, 0.48f, 0.7f);
            ph.alignment = TextAnchor.MiddleLeft;

            var phRT = placeholderGO.GetComponent<RectTransform>();
            phRT.anchorMin = Vector2.zero;
            phRT.anchorMax = Vector2.one;
            phRT.offsetMin = new Vector2(8, 0);
            phRT.offsetMax = new Vector2(-8, 0);

            input.placeholder = ph;

            // Add click-only selection handler
            go.AddComponent<ClickToSelectInputField>().Setup(input);

            return input;
        }

        // ---------- BUTTON ----------

        private static void CreateButton(
            Transform parent,
            string label,
            Vector2 pos,
            Action onClick)
        {
            var btnGO = new GameObject("Button");
            btnGO.transform.SetParent(parent, false);

            var rt = btnGO.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(280, 46);
            rt.anchoredPosition = pos;

            var img = btnGO.AddComponent<UIImage>();
            img.color = ArchipelagoUIStyle.SilksongButtonBackground;

            var btn = btnGO.AddComponent<Button>();
            btn.onClick.AddListener(() => onClick());

            var colors = btn.colors;
            colors.normalColor = ArchipelagoUIStyle.SilksongButtonBackground;
            colors.highlightedColor = ArchipelagoUIStyle.SilksongButtonHover;
            colors.selectedColor = ArchipelagoUIStyle.SilksongButtonHover;
            colors.pressedColor = new Color(0.08f, 0.07f, 0.06f, 1f);
            btn.colors = colors;

            ArchipelagoUIStyle.CreateRectFrame(btnGO.transform, 3f, 2f);
            ArchipelagoUIStyle.CreateButtonCornerOrnaments(btnGO.transform);

            var textGO = new GameObject("Text");
            textGO.transform.SetParent(btnGO.transform, false);

            var txt = textGO.AddComponent<UIText>();
            txt.text = label;
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            txt.fontSize = 15;
            txt.alignment = TextAnchor.MiddleCenter;
            txt.color = ArchipelagoUIStyle.SilksongTextPrimary;

            var textRt = textGO.GetComponent<RectTransform>();
            textRt.anchorMin = Vector2.zero;
            textRt.anchorMax = Vector2.one;
            textRt.offsetMin = new Vector2(8, 0);
            textRt.offsetMax = new Vector2(-8, 0);
        }

        // ---------- TAB ORDER ----------

        public static void SetupTabNavigation(ClickOnlyInputField first, ClickOnlyInputField second, ClickOnlyInputField third)
        {
            // Add custom Tab handler component to each field
            var ClickOnlyInputFields = new List<ClickOnlyInputField> { first, second, third };

            first.gameObject.AddComponent<TabNavigationHandler>().Setup(ClickOnlyInputFields, 0);
            second.gameObject.AddComponent<TabNavigationHandler>().Setup(ClickOnlyInputFields, 1);
            third.gameObject.AddComponent<TabNavigationHandler>().Setup(ClickOnlyInputFields, 2);
        }

        // ---------- EVENT SYSTEM ----------

        private static void EnsureEventSystem()
        {
            if (UnityEngine.Object.FindFirstObjectByType<EventSystem>() != null)
                return;

            new GameObject("EventSystem",
                typeof(EventSystem),
                typeof(StandaloneInputModule));
        }

        private static void EnsureSelectionGuard()
        {
            if (UnityEngine.Object.FindFirstObjectByType<SelectionGuard>() != null)
                return;

            new GameObject("SelectionGuard")
                .AddComponent<SelectionGuard>();
        }
    }

}
