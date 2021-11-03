using Painter.Tool;
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
                _instance = Fetch(null);
            }

            return _instance;
        }

        public static T Fetch(string name)
        {
            if (name == typeof(T).Name)
            {
                TLogger.Error($"It is invalid that create name:[{name}] is same as T's name.");
                return null;
            }

            if (string.IsNullOrEmpty(name))
            {
                name = typeof(T).Name;
            }

            string path = Path.Combine("Assets/Settings", name) + ".asset";
            T result = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(T)) as T;
            if (result == null)
            {
                result = ScriptableObject.CreateInstance<T>();
                UnityEditor.AssetDatabase.CreateAsset(result, path);
                UnityEditor.AssetDatabase.SaveAssets();
            }

            return result;
        }
    }
}