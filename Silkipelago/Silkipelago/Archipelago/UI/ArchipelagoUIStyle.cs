using UnityEngine;
using UIImage = UnityEngine.UI.Image;

namespace Silkipelago.Archipelago.UI
{
    public class ArchipelagoUIStyle
    {
        // Silksong aesthetic colors
        public static readonly Color SilksongBackground = new Color(0.02f, 0.02f, 0.02f, 0.95f);
        public static readonly Color SilksongPanelBackground = new Color(0.06f, 0.06f, 0.07f, 0.9f);
        public static readonly Color SilksongTextPrimary = new Color(0.95f, 0.95f, 0.95f, 1f);
        public static readonly Color SilksongTextSecondary = new Color(0.80f, 0.80f, 0.82f, 1f);
        public static readonly Color SilksongInputBackground = new Color(0.03f, 0.03f, 0.04f, 1f);
        public static readonly Color SilksongButtonBackground = new Color(0.10f, 0.10f, 0.11f, 1f);
        public static readonly Color SilksongButtonHover = new Color(0.16f, 0.16f, 0.17f, 1f);
        public static readonly Color SilksongAccent = new Color(0.90f, 0.90f, 0.92f, 1f);

        public static void CreateCornerOrnamentalFrame(Transform parent)
        {
            var margin = 6f;
            var thickness = 2f;

            var longLen = 42f;   // reach toward middle
            var shortLen = 16f;  // secondary ornament
            var notch = 8f;      // inset step

            // TL
            CreateGothicCorner(parent, new Vector2(0, 1), new Vector2(margin, -margin),
                thickness, longLen, shortLen, notch, true, true);

            // TR
            CreateGothicCorner(parent, new Vector2(1, 1), new Vector2(-margin, -margin),
                thickness, longLen, shortLen, notch, false, true);

            // BL
            CreateGothicCorner(parent, new Vector2(0, 0), new Vector2(margin, margin),
                thickness, longLen, shortLen, notch, true, false);

            // BR
            CreateGothicCorner(parent, new Vector2(1, 0), new Vector2(-margin, margin),
                thickness, longLen, shortLen, notch, false, false);
        }

        private static void CreateGothicCorner(
   Transform parent,
   Vector2 anchor,
   Vector2 offset,
   float t,
   float longLen,
   float shortLen,
   float notch,
   bool right,
   bool down)
        {
            float sx = right ? 1 : -1;
            float sy = down ? -1 : 1;

            // main horizontal
            CreateBar(parent, anchor,
                offset,
                new Vector2(longLen, t),
                new Vector2(right ? 0 : 1, anchor.y));

            // main vertical
            CreateBar(parent, anchor,
                offset,
                new Vector2(t, longLen),
                new Vector2(anchor.x, down ? 1 : 0));

            // inner step horizontal
            CreateBar(parent, anchor,
                offset + new Vector2(sx * notch, sy * notch),
                new Vector2(shortLen, t),
                new Vector2(right ? 0 : 1, anchor.y));

            // inner step vertical
            CreateBar(parent, anchor,
                offset + new Vector2(sx * notch, sy * notch),
                new Vector2(t, shortLen),
                new Vector2(anchor.x, down ? 1 : 0));

            // corner node square
            CreateBar(parent, anchor,
                offset,
                new Vector2(t * 2, t * 2),
                anchor);
        }

        private static void CreateBar(
    Transform parent,
    Vector2 anchor,
    Vector2 offset,
    Vector2 size,
    Vector2 pivot)
        {
            var go = new GameObject("OrnamentBar");
            go.transform.SetParent(parent, false);

            var img = go.AddComponent<UIImage>();
            img.color = SilksongAccent;

            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = anchor;
            rt.anchorMax = anchor;
            rt.pivot = pivot;
            rt.sizeDelta = size;
            rt.anchoredPosition = offset;
        }

        public static void CreateRectFrame(Transform parent, float inset, float thickness)
        {
            CreateFrameBar(parent, true, inset, thickness);   // top
            CreateFrameBar(parent, false, inset, thickness);  // bottom
            CreateFrameBarV(parent, true, inset, thickness);  // left
            CreateFrameBarV(parent, false, inset, thickness); // right
        }


        private static void CreateFrameBar(
    Transform parent,
    bool top,
    float inset,
    float thickness)
        {
            var go = new GameObject("FrameH");
            go.transform.SetParent(parent, false);

            var img = go.AddComponent<UIImage>();
            img.color = SilksongAccent;

            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0, top ? 1 : 0);
            rt.anchorMax = new Vector2(1, top ? 1 : 0);

            if (top)
            {
                rt.offsetMin = new Vector2(inset, -inset - thickness);
                rt.offsetMax = new Vector2(-inset, -inset);
            }
            else
            {
                rt.offsetMin = new Vector2(inset, inset);
                rt.offsetMax = new Vector2(-inset, inset + thickness);
            }
        }

        private static void CreateFrameBarV(
    Transform parent,
    bool left,
    float inset,
    float thickness)
        {
            var go = new GameObject("FrameV");
            go.transform.SetParent(parent, false);

            var img = go.AddComponent<UIImage>();
            img.color = SilksongAccent;

            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(left ? 0 : 1, 0);
            rt.anchorMax = new Vector2(left ? 0 : 1, 1);

            if (left)
            {
                rt.offsetMin = new Vector2(inset, inset);
                rt.offsetMax = new Vector2(inset + thickness, -inset);
            }
            else
            {
                rt.offsetMin = new Vector2(-inset - thickness, inset);
                rt.offsetMax = new Vector2(-inset, -inset);
            }
        }

        public static void CreateButtonCornerOrnaments(Transform parent)
        {
            var margin = 3f;
            var t = 2f;
            var len = 12f;

            CreateMiniCorner(parent, new Vector2(0, 1), new Vector2(margin, -margin), t, len, true, true);
            CreateMiniCorner(parent, new Vector2(1, 1), new Vector2(-margin, -margin), t, len, false, true);
            CreateMiniCorner(parent, new Vector2(0, 0), new Vector2(margin, margin), t, len, true, false);
            CreateMiniCorner(parent, new Vector2(1, 0), new Vector2(-margin, margin), t, len, false, false);
        }

        private static void CreateMiniCorner(
    Transform parent,
    Vector2 anchor,
    Vector2 offset,
    float thickness,
    float len,
    bool right,
    bool down)
        {
            // horizontal tick
            CreateBar(parent, anchor, offset,
                new Vector2(len, thickness),
                new Vector2(right ? 0 : 1, anchor.y));

            // vertical tick
            CreateBar(parent, anchor, offset,
                new Vector2(thickness, len),
                new Vector2(anchor.x, down ? 1 : 0));
        }

    }
}
