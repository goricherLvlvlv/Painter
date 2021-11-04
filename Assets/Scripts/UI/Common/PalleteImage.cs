using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Painter.UI
{
    public class PalleteImage : Image
    {
        private const int LineCount = 360;

        public Color ValueColor = Color.white;

        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            toFill.Clear();

            float radius = GetRadius();
            Vector2 offset = GetPivotOffset(radius);

            toFill.AddVert(offset, Color.white * color * ValueColor, new Vector2(0f, 0f));
            for (int i = 0; i < LineCount; ++i)
            {
                toFill.AddVert(GetPosition(i, radius, offset), GetColor32(i) * color * ValueColor, new Vector2(1f, 1f));

                if (i >= 1)
                {
                    toFill.AddTriangle(0, i, (i == LineCount - 1) ? 1 : (i + 1));
                }
            }

        }

        public void SetValueColor(float value)
        { 
            var color = Color.white * value;
            color.a = 1f;
            ValueColor = color;

            UpdateGeometry();
        }

        public float GetRadius()
        {
            RectTransform rect = transform as RectTransform;
            return Mathf.Min(rect.sizeDelta.x, rect.sizeDelta.y) / 2f;
        }

        public Vector2 GetPivotOffset(float radius)
        {
            RectTransform rect = transform as RectTransform;
            return new Vector2((0.5f - rect.pivot.x) * radius * 2f, (0.5f - rect.pivot.y) * radius * 2f);
        }

        public Vector3 GetPosition(int i, float radius, Vector2 offset)
        {
            var x = radius * Mathf.Sin(i / 180f * Mathf.PI) + offset.x;
            var y = radius * Mathf.Cos(i / 180f * Mathf.PI) + offset.y;

            return new Vector3(x, y);
        }

        public int GetHue(Color32 color)
        {
            var max = Mathf.Max(color.r, color.g, color.b);
            var min = Mathf.Min(color.r, color.g, color.b);
            var delta = max - min;

            int lineCount_6 = LineCount / 6;
            float angle;
            if (color.r == max)
            {
                angle = (float)(color.g - color.b) / delta * lineCount_6;
            }
            else if (color.g == max)
            {
                angle = ((float)(color.b - color.r) / delta + 2f) * lineCount_6;
            }
            else
            {
                angle = ((float)(color.r - color.g) / delta + 4f) * lineCount_6;
            }

            return angle < 0f ? (int)(angle + 360) : (int)angle;
        }

        public Color32 GetColor32(int hue)
        {
            int r, g, b;
            int lineCount_6 = LineCount / 6;
            float lineCount_6f = lineCount_6;

            if (hue >= 0 && hue <= lineCount_6)
            {
                r = 255;
                g = (int)((hue - 0) / lineCount_6f * 255);
                b = 0;
            }
            else if (hue >= lineCount_6 && hue <= lineCount_6 * 2)
            {
                r = (int)((lineCount_6 * 2 - hue) / lineCount_6f * 255);
                g = 255;
                b = 0;
            }
            else if (hue >= lineCount_6 * 2 && hue <= lineCount_6 * 3)
            {
                r = 0;
                g = 255;
                b = (int)((hue - lineCount_6 * 2) / lineCount_6f * 255);
            }
            else if (hue >= lineCount_6 * 3 && hue <= lineCount_6 * 4)
            {
                r = 0;
                g = (int)((lineCount_6 * 4 - hue) / lineCount_6f * 255);
                b = 255;
            }
            else if (hue >= lineCount_6 * 4 && hue <= lineCount_6 * 5)
            {
                r = (int)((hue - lineCount_6 * 4) / lineCount_6f * 255);
                g = 0;
                b = 255;
            }
            else
            {
                r = 255;
                g = 0;
                b = (int)((lineCount_6 * 6 - hue) / lineCount_6f * 255);
            }

            Color32 color = new Color32((byte)r, (byte)g, (byte)b, 255);

            return color;
        }
    }
}