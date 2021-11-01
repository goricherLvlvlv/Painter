using Painter.Setting;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Painter.Manager
{
    public class MResource : ManagerBase<MResource>
    {
        public T LoadAssets<T>(string path) where T : Object
        {
#if UNITY_EDITOR
            return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(Path.Combine(SResource.Fetch().Path, path)) as T;
#else
            return null;
#endif
        }
    }
}