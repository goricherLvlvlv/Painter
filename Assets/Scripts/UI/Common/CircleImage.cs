using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Painter.UI
{
    public class CircleImage : Image
    {
        private const int LineCount = 360;

        public Color ValueColor = Color.white;

        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            toFill.Clear();

            float radius = GetRadius();
            Vector2 offset = GetPivotOffset(radius);

            toFill.AddVert(offset, ValueColor * color, new Vector2(0f, 0f));
            for (int i = 0; i < LineCount; ++i)
            {
                toFill.AddVert(GetPosition(i, radius, offset), GetColor32(i) * color, new Vector2(1f, 1f));

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

        public Color32 GetColor32(int i)
        {
            int r, g, b;
            int lineCount_6 = LineCount / 6;


            if (i >= 0 && i <= lineCount_6)
            {
                r = 255;
                g = 0;
                b = (int)((i - 0) / 60f * 255);
            }
            else if (i >= lineCount_6 && i <= lineCount_6 * 2)
            {
                r = (int)((lineCount_6 * 2 - i) / 60f * 255);
                g = 0;
                b = 255;
            }
            else if (i >= lineCount_6 * 2 && i <= lineCount_6 * 3)
            {
                r = 0;
                g = (int)((i - lineCount_6 * 2) / 60f * 255);
                b = 255;
            }
            else if (i >= lineCount_6 * 3 && i <= lineCount_6 * 4)
            {
                r = 0;
                g = 255;
                b = (int)((lineCount_6 * 4 - i) / 60f * 255);
            }
            else if (i >= lineCount_6 * 4 && i <= lineCount_6 * 5)
            {
                r = (int)((i - lineCount_6 * 4) / 60f * 255);
                g = 255;
                b = 0;
            }
            else
            {
                r = 255;
                g = (int)((lineCount_6 * 6 - i) / 60f * 255);
                b = 0;
            }

            Color32 color = new Color32((byte)r, (byte)g, (byte)b, 255);

            return color;
        }
    }
}