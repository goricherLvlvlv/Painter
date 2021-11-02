using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Painter.Manager;

namespace Painter.GamePlay
{
    public class Root : MonoBehaviour, IRoot
    {
        private MResource _resource => MResource.Fetch();

        private const string GameResourcePath = "Prefabs/Game/";
        private const string UIResourcePath = "Prefabs/UI/";

        [SerializeField]
        private GameObject _gameLayer;

        [SerializeField]
        private GameObject _uiLayer;

        public GameObject LoadPrefabAtGameLayer(string name)
        {
            return LoadPrefab($"{GameResourcePath}{name}", _gameLayer);
        }

        public GameObject LoadPrefabAtUILayer(string name)
        {
            return LoadPrefab($"{UIResourcePath}{name}", _gameLayer);
        }

        private GameObject LoadPrefab(string path, GameObject parent)
        {
            var go = _resource.LoadAssets<GameObject>(path);
            go = Instantiate(go, parent.transform);
            go.transform.localPosition = Vector2.zero;
            go.transform.localScale = Vector3.one;
            go.transform.localRotation = Quaternion.identity;

            return go;
        }
    }
}