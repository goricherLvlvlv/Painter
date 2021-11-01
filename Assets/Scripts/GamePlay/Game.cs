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

        public GameObject UIRoot;

        private Canvas _canvas = null;
        private Pen _pen = null;

        public Canvas Canvas => _canvas;
        public Pen Pen => _pen;

        public void Awake()
        {
            var result = _resource.LoadAssets<GameObject>("Canvas.prefab");
            _canvas = Instantiate(result, UIRoot.transform).GetComponent<Canvas>();
            _canvas.transform.localPosition = Vector2.zero;
            _canvas.transform.localScale = Vector3.one;
            _canvas.transform.localRotation = Quaternion.identity;

            result = _resource.LoadAssets<GameObject>("Pen.prefab");
            _pen = Instantiate(result, UIRoot.transform).GetComponent<Pen>();
            _pen.transform.localPosition = Vector2.zero;
            _pen.transform.localScale = Vector3.one;
            _pen.transform.localRotation = Quaternion.identity;
        }
    }
}