using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Painter.GamePlay
{
    public class ColorPalette : MonoBehaviour
    {
        private Image image;
        public GameObject quad;

        private void Awake()
        {
            image = GetComponent<Image>();
            StartCoroutine(Colors());

            int radio = 255;
            Mesh mesh = new Mesh();
            List<Vector3> vertices = new List<Vector3>() { Vector3.zero };
            List<int> triangles = new List<int>();

            for (int i = 0; i < 360; ++i)
            {
                var x = radio * Mathf.Sin(i / 180f * Mathf.PI);
                var y = radio * Mathf.Cos(i / 180f * Mathf.PI);

                vertices.Add(new Vector3(x / 10f, y / 10f));

                if (i >= 1)
                {
                    triangles.Add(0);
                    triangles.Add(i);
                    if (i == 360 - 1)
                    {
                        triangles.Add(1);
                    }
                    else
                    {
                        triangles.Add(i + 1);
                    }
                }
            }

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            quad.GetComponent<MeshFilter>().mesh = mesh;
        }

        private IEnumerator Colors()
        {
            int radio = 255;
            for (int i = 0; i < 360; ++i)
            {
                var x = radio * Mathf.Sin(i / 180f * Mathf.PI);
                var y = radio * Mathf.Cos(i / 180f * Mathf.PI);

                int r = 0;
                int g = 0;
                int b = 0;

                if (i >= 0 && i <= 60)
                {
                    r = 255;
                    g = 0;
                    b = (int)((i - 0) / 60f * 255);
                }
                else if (i >= 60 && i <= 120)
                {
                    r = (int)((120 - i) / 60f * 255);
                    g = 0;
                    b = 255;
                }
                else if (i >= 120 && i <= 180)
                {
                    r = 0;
                    g = (int)((i - 120) / 60f * 255);
                    b = 255;
                }
                else if (i >= 180 && i <= 240)
                {
                    r = 0;
                    g = 255;
                    b = (int)((240 - i) / 60f * 255);
                }
                else if (i >= 240 && i <= 300)
                {
                    r = (int)((i - 240) / 60f * 255);
                    g = 255;
                    b = 0;
                }
                else
                {
                    r = 255;
                    g = (int)((360 - i) / 60f * 255);
                    b = 0;
                }

                Color32 color = new Color32((byte)r, (byte)g, (byte)b, 255);
                image.color = color;

                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
            }
        }
    }
}