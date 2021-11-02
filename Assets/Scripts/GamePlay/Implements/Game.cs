using System.Collections;
using System.Collections.Generic;
using Painter.Manager;
using Painter.Setting;
using UnityEngine;

namespace Painter.GamePlay
{
    public class Game : MonoBehaviour
    {
        private MResource _resource => MResource.Fetch();

        public Root Root;

        private Canvas _canvas = null;
        private Pen _pen = null;

        public Canvas Canvas => _canvas;
        public Pen Pen => _pen;

        public void Awake()
        {
            _canvas = Root.LoadPrefabAtGameLayer("Canvas.prefab").GetComponent<Canvas>();
            _pen = Root.LoadPrefabAtGameLayer("Pen.prefab").GetComponent<Pen>();

            Root.LoadPrefabAtUILayer("GamePanel.prefab");
        }
    }
}