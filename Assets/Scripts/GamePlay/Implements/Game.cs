using System.Collections;
using System.Collections.Generic;
using Painter.Manager;
using Painter.Setting;
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

        #endregion

        #region Override

        public void Awake()
        {
            _canvas = Root.LoadPrefabAtGameLayer("Canvas.prefab").GetComponent<Canvas>();

            Root.LoadPrefabAtUILayer("GamePanel.prefab");
        }

        #endregion

        #region Interface

        public void ChooseEraser()
        {
            ChoosePen("Eraser.prefab");
        }

        public void ChoosePen(string path)
        {
            if (_pen != null)
            {
                Destroy(_pen.gameObject);
                _pen = null;
            }

            _pen = Root.LoadPrefabAtGameLayer(path).GetComponent<Pen>();
            _pen.Path = path;
        }
        #endregion
    }
}