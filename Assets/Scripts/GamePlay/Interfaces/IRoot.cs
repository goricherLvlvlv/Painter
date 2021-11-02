using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Painter.GamePlay
{
    public interface IRoot
    {
        public GameObject LoadPrefabAtGameLayer(string path);

        public GameObject LoadPrefabAtUILayer(string path);
    }
}