using System.Collections;
using System.Collections.Generic;
using Painter.Manager;
using Painter.Setting;
using Painter.UI;
using UnityEngine;

namespace Painter.GamePlay
{
    public class Game : MonoBehaviour, IGame
    {
        #region Variable
        private MResource _resource => MResource.Fetch();

        private Canvas _canvas = null;
        private Pen _pen = null;

        [SerializeField]
        private Root _root;

        public IRoot Root => _root;
        public ICanvas Canvas => _canvas;
        public IPen Pen => _pen;

        public int PenLevel { get; set; } = 1;
        public int EraserLevel { get; set; } = 1;
        public int DrawLevel => Pen.Type == PenType.Pen ? PenLevel : EraserLevel;

        #endregion

        #region Override

        public void Awake()
        {
            _canvas = Root.LoadPrefabAtGameLayer("Canvas.prefab").GetComponent<Canvas>();

            BasePanel.Open<GamePanel, NullArg>(null);
        }

        #endregion

        #region Interface

        public void ChooseEraser()
        {
            ChoosePen("Eraser");
        }

        public void ChoosePen(string name)
        {
            Color oldColor = Color.black;
            if (_pen != null)
            {
                oldColor = _pen.Color;
                Destroy(_pen.gameObject);
                _pen = null;
            }

            _pen = Root.LoadPrefabAtGameLayer(name + ".prefab").GetComponent<Pen>();
            _pen.Color = oldColor;
            _pen.Name = name;
        }
        #endregion
    }
}