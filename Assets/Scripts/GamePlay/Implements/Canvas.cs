using Painter.Setting;
using UnityEngine;
using UnityEngine.UI;
using Painter.UI;
using UnityEngine.EventSystems;
using Painter.Manager;
using System.Diagnostics;
using System;

namespace Painter.GamePlay
{
    public partial class Canvas : MonoBehaviour
    {
        #region Variable

        private Texture2D _canvas;

        private Image _image;

        private IGame _game => MGame.Fetch().Current;

        private Vector2? _lastPos = null;

        private static Color[] whiteColor;

        #endregion

        #region Override

        private void Awake()
        {
            _image = GetComponent<Image>();

            _canvas = new Texture2D((int)SGame.Fetch().UIResolution.x, (int)SGame.Fetch().UIResolution.y);
            FillWhiteTexture(_canvas);
            Sprite sp = Sprite.Create(_canvas, new Rect(0f, 0f, 1920f, 1080f), new Vector2(0.5f, 0.5f));
            _image.sprite = sp;

            gameObject.ClearEvent();
            gameObject.AddBeginDragEvent(OnBeginDragEvent);
            gameObject.AddDragEvent(OnDragEvent);
            gameObject.AddEndDragEvent(OnDragEndEvent);
        }

        #endregion

        #region Event

        private void OnBeginDragEvent(PointerEventData data)
        {
            _lastPos = null;
            Draw(ClampMouseInput(data.position));
        }

        private void OnDragEvent(PointerEventData data)
        {
            Draw(ClampMouseInput(data.position));
        }

        private void OnDragEndEvent(PointerEventData data)
        {
            SaveChangedColor();
        }

        #endregion

        #region Tool

        private static void FillWhiteTexture(Texture2D texture)
        {
            if (whiteColor == null)
            {
                whiteColor = new Color[(int)SGame.Fetch().UIResolution.x * (int)SGame.Fetch().UIResolution.y];
                Array.Fill(whiteColor, Color.white);
            }

            texture.SetPixels(0, 0, texture.width, texture.height, whiteColor);
            texture.Apply();
        }

        private Vector2 ClampMouseInput(Vector2 position)
        {
            position.x = Mathf.Clamp(position.x, 0f, SGame.Fetch().UIResolution.x);
            position.y = Mathf.Clamp(position.y, 0f, SGame.Fetch().UIResolution.y);
            return position;
        }

        #endregion

        #region Draw

        // ??????????????
        private void DrawPixel(int x, int y, Color color)
        {
            var oldColor = _canvas.GetPixel(x, y);
            if (Mathf.Abs(oldColor.r - color.r) < 0.019 
                && Mathf.Abs(oldColor.g - color.g) < 0.019 
                && Mathf.Abs(oldColor.b - color.b) < 0.019)
            {
                return;
            }

            _canvas.SetPixel(x, y, color);

            AddChangedColor(x, y, oldColor);
        }

        // ??????????
        private void DrawPoint(int x, int y, Color color)
        {
            var (blocks, level) = SPen.Fetch(_game.Pen.Name).GetBlocks(_game.DrawLevel);

            for (int i = 0; i < blocks.Length; ++i)
            {
                if (!blocks[i])
                {
                    continue;
                }

                int yOffset = i / level - level / 2;
                int xOffset = i % level - level / 2;
                DrawPixel(x + xOffset, y + yOffset, color);
            }
        }

        // ??????????, ????
        private bool DrawLineHorizontal(Vector2 lastPos, Vector2 curPos, Color color)
        {
            if (curPos.y != lastPos.y)
            {
                return false;
            }

            int originX = (int)Mathf.Min(lastPos.x, curPos.x);
            int targetX = (int)Mathf.Max(lastPos.x, curPos.x);

            for (int i = originX; i <= targetX; ++i)
            {
                DrawPoint(i, (int)curPos.y, color);
            }
            return true;
        }

        // ??????????, ????
        private bool DrawLineVertical(Vector2 lastPos, Vector2 curPos, Color color)
        {
            if (curPos.x != lastPos.x)
            {
                return false;
            }

            int originY = (int)Mathf.Min(lastPos.y, curPos.y);
            int targetY = (int)Mathf.Max(lastPos.y, curPos.y);

            for (int i = originY; i <= targetY; ++i)
            {
                DrawPoint((int)curPos.x, i, color);
            }
            return true;
        }

        // ??????????, step x
        private void DrawLineStepX(float originX, float targetX, float originY, float k, Color color)
        {
            float y = originY;
            for (int i = (int)originX; i <= (int)targetX; ++i)
            {
                DrawPoint(i, (int)y, color);

                y += k;
            }
        }

        // ??????????, step y
        private void DrawStepY(float originY, float targetY, float originX, float k, Color color)
        {
            float x = originX;
            for (int i = (int)originY; i <= (int)targetY; ++i)
            {
                DrawPoint((int)x, i, color);

                x += k;
            }
        }

        // ??????????
        private void DrawLine(Vector2 lastPos, Vector2 curPos, Color color)
        {
            // ????
            if (DrawLineHorizontal(lastPos, curPos, color))
            {
                return;
            }

            // ????
            if (DrawLineVertical(lastPos, curPos, color))
            {
                return;
            }

            float k = (curPos.y - lastPos.y) / (curPos.x - lastPos.x);

            if (Mathf.Abs(k) < 1.0f)
            {
                if (curPos.x > lastPos.x)
                {
                    DrawLineStepX(lastPos.x, curPos.x, lastPos.y, k, color);
                }
                else
                {
                    DrawLineStepX(curPos.x, lastPos.x, curPos.y, k, color);
                }
            }
            else
            {
                if (curPos.y > lastPos.y)
                {
                    DrawStepY(lastPos.y, curPos.y, lastPos.x, 1.0f / k, color);
                }
                else
                {
                    DrawStepY(curPos.y, lastPos.y, curPos.x, 1.0f / k, color);
                }
            }
        }

        private void Draw(Vector2 pos)
        {
            if (_lastPos == null || _lastPos == pos)
            {
                DrawPoint((int)pos.x, (int)pos.y, _game.Pen.DrawColor);
                _canvas.Apply();
            }
            else
            {
                DrawLine(_lastPos.Value, pos, _game.Pen.DrawColor);
                _canvas.Apply();
            }

            _lastPos = pos;
        }

        #endregion

    }
}
