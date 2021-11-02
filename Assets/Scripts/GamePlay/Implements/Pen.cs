using Painter.Manager;
using Painter.Setting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Painter.GamePlay
{
    public class Pen : MonoBehaviour, IPen
    {
        #region Variable

        private IGame _game => MGame.Fetch().Current;

        public virtual Color Color => Color.black;

        public string Path { get; set; }

        #endregion

        #region Override

        private void Update()
        {
            FollowMouse(ClampMouseInput(Input.mousePosition));
        }

        #endregion

        #region Position

        private void FollowMouse(Vector2 mouseInput)
        {
            transform.localPosition = RecorrectMouseInput(mouseInput);
        }

        private Vector2 ClampMouseInput(Vector2 mouseInput)
        {
            mouseInput.x = Mathf.Clamp(mouseInput.x, 0f, SGame.Fetch().UIResolution.x);
            mouseInput.y = Mathf.Clamp(mouseInput.y, 0f, SGame.Fetch().UIResolution.y);
            return mouseInput;
        }

        private Vector2 RecorrectMouseInput(Vector2 mouseInput)
        {
            mouseInput.x -= SGame.Fetch().UIResolution.x / 2;
            mouseInput.y -= SGame.Fetch().UIResolution.y / 2;
            return mouseInput;
        }

        #endregion
    }
}