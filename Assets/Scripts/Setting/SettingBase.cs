using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Painter.Setting
{
    public class SettingBase<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance;

        public static T Fetch()
        {
            if (_instance == null)
            {
                string path = Path.Combine("Assets/Settings", typeof(T).Name) + ".asset";
                T result = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(T)) as T;
                if (result == null)
                {
                    result = ScriptableObject.CreateInstance<T>();
                    UnityEditor.AssetDatabase.CreateAsset(result, path);
                    UnityEditor.AssetDatabase.SaveAssets();
                }
                _instance = result;
            }

            return _instance;
        }
    }
}