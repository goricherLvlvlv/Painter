using Painter.Setting;
using UnityEngine;
using UnityEngine.UI;
using Painter.UI;
using UnityEngine.EventSystems;
using Painter.Manager;

namespace Painter.GamePlay
{
    public partial class Canvas : MonoBehaviour
    {
        #region Variable

        private Texture2D _canvas;

        private Image _image;

        private Game _game => MGame.Fetch().Current;

        private Vector2? _lastPos = null;

        #endregion

        #region Override

        private void Awake()
        {
            _image = GetComponent<Image>();

            _canvas = new Texture2D((int)SGame.Fetch().UIResolution.x, (int)SGame.Fetch().UIResolution.y);
            for (int row = 0; row < _canvas.width; ++row)
            {
                for (int col = 0; col < _canvas.height; ++col)
                {
                    _canvas.SetPixel(row, col, Color.white);
                }
            }
            _canvas.Apply();

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

        private Vector2 ClampMouseInput(Vector2 position)
        {
            position.x = Mathf.Clamp(position.x, 0f, SGame.Fetch().UIResolution.x);
            position.y = Mathf.Clamp(position.y, 0f, SGame.Fetch().UIResolution.y);
            return position;
        }

        #endregion

        #region Draw

        // 绘制单个像素点
        private void DrawPixel(int x, int y, Color color)
        {
            var oldColor = _canvas.GetPixel(x, y);
            if (oldColor == color)
            {
                return;
            }

            _canvas.SetPixel(x, y, color);

            AddChangedColor(x, y, oldColor);
        }

        // 绘制一个点
        private void DrawPoint(int x, int y, Color color)
        {
            DrawPixel(x - 1, y - 1, color);
            DrawPixel(x, y - 1, color);
            DrawPixel(x + 1, y - 1, color);
            DrawPixel(x - 1, y, color);
            DrawPixel(x, y, color);
            DrawPixel(x + 1, y, color);
            DrawPixel(x - 1, y + 1, color);
            DrawPixel(x, y + 1, color);
            DrawPixel(x + 1, y + 1, color);
        }

        // 绘制一条线, 横线
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

        // 绘制一条线, 竖线
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

        // 绘制一条线, step x
        private void DrawLineStepX(float originX, float targetX, float originY, float k, Color color)
        {
            float y = originY;
            for (int i = (int)originX; i <= (int)targetX; ++i)
            {
                DrawPoint(i, (int)y, color);

                y += k;
            }
        }

        // 绘制一条线, step y
        private void DrawStepY(float originY, float targetY, float originX, float k, Color color)
        {
            float x = originX;
            for (int i = (int)originY; i <= (int)targetY; ++i)
            {
                DrawPoint((int)x, i, color);

                x += k;
            }
        }

        // 绘制一条线
        private void DrawLine(Vector2 lastPos, Vector2 curPos, Color color)
        {
            // 横线
            if (DrawLineHorizontal(lastPos, curPos, color))
            {
                return;
            }

            // 竖线
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
                DrawPoint((int)pos.x, (int)pos.y, _game.Pen.Color);
                _canvas.Apply();
            }
            else
            {
                DrawLine(_lastPos.Value, pos, _game.Pen.Color);
                _canvas.Apply();
            }

            _lastPos = pos;
        }

        #endregion

    }
}
