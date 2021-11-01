using Painter.Setting;
using UnityEngine;
using UnityEngine.UI;
using Painter.GamePlay.UI;
using UnityEngine.EventSystems;
using Painter.Manager;

namespace Painter.GamePlay
{
    public class Canvas : MonoBehaviour
    {
        private Texture2D _canvas;

        private Image _image;

        private Game _game => MGame.Fetch().Current;

        private Vector2? _lastPos = null;

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
            gameObject.AddBeginDragEvent((data)=>
            {
                _lastPos = null;
                Draw(data);
            });
            gameObject.AddDragEvent(Draw);
        }

        private void Draw(int x, int y, Color color)
        {
            _canvas.SetPixel(x - 1, y - 1, color);
            _canvas.SetPixel(x, y - 1, color);
            _canvas.SetPixel(x + 1, y - 1, color);
            _canvas.SetPixel(x - 1, y, color);
            _canvas.SetPixel(x, y, color);
            _canvas.SetPixel(x + 1, y, color);
            _canvas.SetPixel(x - 1, y + 1, color);
            _canvas.SetPixel(x, y + 1, color);
            _canvas.SetPixel(x + 1, y + 1, color);
        }

        private void DrawLine(Vector2 lastPos, Vector2 curPos, Color color)
        {
            // ∫·œﬂ
            bool horizontal = curPos.y == lastPos.y;

            if (horizontal)
            {
                int originX = (int)Mathf.Min(lastPos.x, curPos.x);
                int targetX = (int)Mathf.Max(lastPos.x, curPos.x);

                for (int i = originX; i <= targetX; ++i)
                {
                    Draw(i, (int)curPos.y, color);
                }

                return;
            }

            //  ˙œﬂ
            bool vertical = curPos.x == lastPos.x;

            if (vertical)
            {
                int originY = (int)Mathf.Min(lastPos.y, curPos.y);
                int targetY = (int)Mathf.Max(lastPos.y, curPos.y);

                for (int i = originY; i <= targetY; ++i)
                {
                    Draw((int)curPos.x, i, color);
                }

                return;
            }

            void StepX(float originX, float targetX, float originY, float k)
            {
                float y = originY;
                for (int i = (int)originX; i <= (int)targetX; ++i)
                {
                    Draw(i, (int)y, color);

                    y += k;
                }
            }

            void StepY(float originY, float targetY, float originX, float k)
            {
                float x = originX;
                for (int i = (int)originY; i <= (int)targetY; ++i)
                {
                    Draw((int)x, i, color);

                    x += k;
                }
            }

            float k = (curPos.y - lastPos.y) / (curPos.x - lastPos.x);

            if (Mathf.Abs(k) < 1.0f)
            {
                if (curPos.x > lastPos.x)
                {
                    StepX(lastPos.x, curPos.x, lastPos.y, k);
                }
                else
                {
                    StepX(curPos.x, lastPos.x, curPos.y, k);
                }
            }
            else
            {
                if (curPos.y > lastPos.y)
                {
                    StepY(lastPos.y, curPos.y, lastPos.x, 1.0f / k);
                }
                else
                { 
                    StepY(curPos.y, lastPos.y, curPos.x, 1.0f / k);
                }
            }
        }

        private Vector2 ClampMouseInput(Vector2 position)
        {
            position.x = Mathf.Clamp(position.x, 0f, SGame.Fetch().UIResolution.x);
            position.y = Mathf.Clamp(position.y, 0f, SGame.Fetch().UIResolution.y);
            return position;
        }

        private void Draw(PointerEventData data)
        {
            var pos = ClampMouseInput(data.position);

            if (_lastPos == null || _lastPos == pos)
            {
                Draw((int)pos.x, (int)pos.y, _game.Pen.Color);
                _canvas.Apply();
            }
            else
            {
                DrawLine(_lastPos.Value, pos, _game.Pen.Color);
                _canvas.Apply();
            }

            _lastPos = pos;
        }
    }
}
