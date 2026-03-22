using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Silkipelago.Archipelago.UI
{
    public class ItemNotificationUI
    {
        private Canvas _canvas;
        private GameObject _headerGO;
        private Font _cachedFont;
        private readonly Queue<NotificationItem> _notifications = new();
        private readonly Queue<string> _pendingNotifications = new();

        // Timing
        private const float DISPLAY_DURATION = 7f;
        private const float FADE_START_TIME = 0.8f;

        // Layout
        private const int MAX_NOTIFICATIONS = 10;
        private const float NOTIFICATION_HEIGHT = 75f;
        private const float NOTIFICATION_WIDTH = 300f;
        private const float TOP_PADDING = 20f;
        private const float RIGHT_PADDING = 20f;
        private const float NOTIFICATION_SPACING = 5f;
        private const float TEXT_HORIZONTAL_PADDING = 10f;
        private const float HEADER_SPACING = 10f;

        // Styling
        private const int FONT_SIZE = 16;
        private const int OUTLINE_DISTANCE = 1;

        // Alpha
        private const float ALPHA_VISIBLE = 1f;
        private const float ALPHA_HIDDEN = 0f;

        // Anchor positions
        private static readonly Vector2 ANCHOR_TOP_RIGHT = new(1, 1);
        private static readonly Vector2 ANCHOR_MIN = Vector2.zero;
        private static readonly Vector2 ANCHOR_MAX = Vector2.one;
        private static readonly Vector2 PIVOT_TOP_RIGHT = new(1, 1);

        // Text offsets
        private static readonly Vector2 TEXT_OFFSET_MIN = new(TEXT_HORIZONTAL_PADDING, 0);
        private static readonly Vector2 TEXT_OFFSET_MAX = new(-TEXT_HORIZONTAL_PADDING, 0);

        // Outline properties
        private static readonly Vector2 OUTLINE_EFFECT_DISTANCE = new(OUTLINE_DISTANCE, OUTLINE_DISTANCE);
        private static readonly Color OUTLINE_COLOR = Color.black;

        // Text properties
        private static readonly Color TEXT_COLOR = Color.white;
        private const string HEADER_TEXT = "<-- received items -->";

        public ItemNotificationUI()
        {
            _cachedFont = GetGameFont();
            CreateCanvas();
        }

        private Font GetGameFont()
        {
            // Try to load Perpetua font from the game
            var perpetuaVariants = new[] { "Perpetua", "perpetua", "Perpetua SemiBold" };

            foreach (var fontName in perpetuaVariants)
            {
                var font = Resources.Load<Font>($"Fonts/{fontName}");
                if (font != null) return font;
            }

            // Fallback to Arial
            return Resources.GetBuiltinResource<Font>("Arial.ttf");
        }

        private void CreateCanvas()
        {
            var canvasObject = new GameObject("ItemNotificationCanvas");
            _canvas = canvasObject.AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var graphicRaycaster = canvasObject.AddComponent<GraphicRaycaster>();
            canvasObject.layer = LayerMask.NameToLayer("UI");

            Object.DontDestroyOnLoad(canvasObject);

            CreateHeader();
        }

        private void CreateHeader()
        {
            _headerGO = new GameObject("Header");
            _headerGO.transform.SetParent(_canvas.transform, false);
            _headerGO.SetActive(false);

            var rectTransform = _headerGO.AddComponent<RectTransform>();
            rectTransform.anchorMin = ANCHOR_TOP_RIGHT;
            rectTransform.anchorMax = ANCHOR_TOP_RIGHT;
            rectTransform.pivot = PIVOT_TOP_RIGHT;
            rectTransform.anchoredPosition = new Vector2(-RIGHT_PADDING, -TOP_PADDING);
            rectTransform.sizeDelta = new Vector2(NOTIFICATION_WIDTH, NOTIFICATION_HEIGHT);

            var headerText = _headerGO.AddComponent<Text>();
            headerText.text = HEADER_TEXT;
            headerText.font = _cachedFont;
            headerText.fontSize = FONT_SIZE;
            headerText.fontStyle = FontStyle.Bold;
            headerText.alignment = TextAnchor.MiddleLeft;
            headerText.color = TEXT_COLOR;

            var outline = _headerGO.AddComponent<Outline>();
            outline.effectColor = OUTLINE_COLOR;
            outline.effectDistance = OUTLINE_EFFECT_DISTANCE;

            var layoutElement = _headerGO.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = NOTIFICATION_HEIGHT;
            layoutElement.preferredWidth = NOTIFICATION_WIDTH;
        }

        public void ShowItemNotification(string itemName)
        {
            if (_notifications.Count >= MAX_NOTIFICATIONS)
            {
                _pendingNotifications.Enqueue(itemName);
                return;
            }

            DisplayNotification(itemName);
        }

        private void DisplayNotification(string itemName)
        {
            var notificationGO = new GameObject($"Notification_{_notifications.Count}");
            notificationGO.transform.SetParent(_canvas.transform, false);

            var rectTransform = notificationGO.AddComponent<RectTransform>();
            rectTransform.anchorMin = ANCHOR_TOP_RIGHT;
            rectTransform.anchorMax = ANCHOR_TOP_RIGHT;
            rectTransform.pivot = PIVOT_TOP_RIGHT;

            var yOffset = -(TOP_PADDING + NOTIFICATION_HEIGHT + HEADER_SPACING + (_notifications.Count * (NOTIFICATION_HEIGHT + NOTIFICATION_SPACING)));
            rectTransform.anchoredPosition = new Vector2(-RIGHT_PADDING, yOffset);
            rectTransform.sizeDelta = new Vector2(NOTIFICATION_WIDTH, NOTIFICATION_HEIGHT);

            var canvasGroup = notificationGO.AddComponent<CanvasGroup>();
            canvasGroup.alpha = ALPHA_VISIBLE;

            CreateNotificationText(notificationGO, itemName);

            var notification = new NotificationItem
            {
                gameObject = notificationGO,
                startTime = Time.time,
                canvasGroup = canvasGroup
            };

            _notifications.Enqueue(notification);
        }

        private void CreateNotificationText(GameObject notificationGO, string itemName)
        {
            var textGO = new GameObject("Text");
            textGO.transform.SetParent(notificationGO.transform, false);

            var textRect = textGO.AddComponent<RectTransform>();
            textRect.anchorMin = ANCHOR_MIN;
            textRect.anchorMax = ANCHOR_MAX;
            textRect.offsetMin = TEXT_OFFSET_MIN;
            textRect.offsetMax = TEXT_OFFSET_MAX;

            var text = textGO.AddComponent<Text>();
            text.text = itemName;
            text.font = _cachedFont;
            text.fontSize = FONT_SIZE;
            text.fontStyle = FontStyle.Bold;
            text.alignment = TextAnchor.MiddleLeft;
            text.color = TEXT_COLOR;

            var outline = textGO.AddComponent<Outline>();
            outline.effectColor = OUTLINE_COLOR;
            outline.effectDistance = OUTLINE_EFFECT_DISTANCE;

            var layoutElement = textGO.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = NOTIFICATION_HEIGHT;
            layoutElement.preferredWidth = NOTIFICATION_WIDTH;
        }

        public void Update()
        {
            var currentTime = Time.time;
            var toRemove = new List<NotificationItem>();

            foreach (var notification in _notifications)
            {
                var elapsedTime = currentTime - notification.startTime;
                UpdateNotificationAlpha(notification, elapsedTime);

                if (elapsedTime >= DISPLAY_DURATION)
                {
                    toRemove.Add(notification);
                    Object.Destroy(notification.gameObject);
                }
            }

            foreach (var notification in toRemove)
            {
                _notifications.Dequeue();
            }

            DisplayPendingNotifications();
            RepositionNotifications();
        }

        private void UpdateNotificationAlpha(NotificationItem notification, float elapsedTime)
        {
            if (elapsedTime >= DISPLAY_DURATION * FADE_START_TIME)
            {
                var fadeProgress = (elapsedTime - DISPLAY_DURATION * FADE_START_TIME) /
                                  (DISPLAY_DURATION * (1 - FADE_START_TIME));
                notification.canvasGroup.alpha = Mathf.Lerp(ALPHA_VISIBLE, ALPHA_HIDDEN, fadeProgress);
            }
            else
            {
                notification.canvasGroup.alpha = ALPHA_VISIBLE;
            }
        }

        private void DisplayPendingNotifications()
        {
            while (_notifications.Count < MAX_NOTIFICATIONS && _pendingNotifications.Count > 0)
            {
                var nextItem = _pendingNotifications.Dequeue();
                DisplayNotification(nextItem);
            }
        }

        private void RepositionNotifications()
        {
            UpdateHeaderVisibility();

            var index = 0;
            foreach (var notification in _notifications)
            {
                var rectTransform = notification.gameObject.GetComponent<RectTransform>();
                var yOffset = -(TOP_PADDING + NOTIFICATION_HEIGHT + HEADER_SPACING + (index * (NOTIFICATION_HEIGHT + NOTIFICATION_SPACING)));
                rectTransform.anchoredPosition = new Vector2(-RIGHT_PADDING, yOffset);
                index++;
            }
        }

        private void UpdateHeaderVisibility()
        {
            _headerGO.SetActive(_notifications.Count > 0);
        }

        private class NotificationItem
        {
            public GameObject gameObject { get; set; }
            public float startTime { get; set; }
            public CanvasGroup canvasGroup { get; set; }
        }
    }
}
