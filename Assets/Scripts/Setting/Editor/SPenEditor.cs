using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Painter.Setting;
using Painter.Tool;

namespace Painter.Editor.Setting
{
    [CustomEditor(typeof(SPen))]
    public class SPenEditor : UnityEditor.Editor
    {

        private SerializedProperty _3x3blocks;
        private SerializedProperty _5x5blocks;
        private SerializedProperty _7x7blocks;
        private SerializedProperty _9x9blocks;
        private SerializedProperty _11x11blocks;

        private GUIStyle styleEnable;
        private GUIStyle styleDisable;

        private void OnEnable()
        {
            styleEnable = new GUIStyle();
            styleEnable.onNormal.background = Texture2D.whiteTexture;
            styleEnable.onActive.background = Texture2D.whiteTexture;
            styleEnable.onFocused.background = Texture2D.whiteTexture;
            styleEnable.normal.background = Texture2D.whiteTexture;
            styleEnable.active.background = Texture2D.whiteTexture;
            styleEnable.focused.background = Texture2D.whiteTexture;

            styleDisable = new GUIStyle();
            styleDisable.onNormal.background = Texture2D.grayTexture;
            styleDisable.onActive.background = Texture2D.grayTexture;
            styleDisable.onFocused.background = Texture2D.grayTexture;
            styleDisable.normal.background = Texture2D.grayTexture;
            styleDisable.active.background = Texture2D.grayTexture;
            styleDisable.focused.background = Texture2D.grayTexture;

            _3x3blocks = serializedObject.FindProperty("_3x3blocks");
            _5x5blocks = serializedObject.FindProperty("_5x5blocks");
            _7x7blocks = serializedObject.FindProperty("_7x7blocks");
            _9x9blocks = serializedObject.FindProperty("_9x9blocks");
            _11x11blocks = serializedObject.FindProperty("_11x11blocks");
        }

        public override void OnInspectorGUI()
        {
            DrawBlocks(_3x3blocks);
            EditorGUILayout.Space(10);
            DrawBlocks(_5x5blocks);
            EditorGUILayout.Space(10);
            DrawBlocks(_7x7blocks);
            EditorGUILayout.Space(10);
            DrawBlocks(_9x9blocks);
            EditorGUILayout.Space(10);
            DrawBlocks(_11x11blocks);

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawBlocks(SerializedProperty blocks)
        {
            var length = (int)Mathf.Sqrt(blocks.arraySize);
            bool flag = false;

            bool? allSet = null;

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(blocks.displayName);
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("全选", GUILayout.Width(70f)))
            {
                allSet = true;
            }
            else if (GUILayout.Button("全不选", GUILayout.Width(80f)))
            {
                allSet = false;
            }
            EditorGUILayout.EndHorizontal();

            for (int i = 0; i < blocks.arraySize; ++i)
            {
                if (i % length == 0)
                {
                    EditorGUILayout.BeginHorizontal();
                    flag = true;
                }

                var block = blocks.GetArrayElementAtIndex(i);
                if (allSet != null)
                {
                    block.boolValue = allSet.Value;
                }
                block.boolValue = DrawBlock(block.boolValue);

                if ((i + 1) % length == 0)
                {
                    EditorGUILayout.EndHorizontal();
                    flag = false;
                }
            }

            if (flag)
            {
                EditorGUILayout.EndHorizontal();
            }

        }

        private bool DrawBlock(bool val, float length = 15f)
        {
            val = EditorGUILayout.Toggle(val, val ? styleEnable : styleDisable, GUILayout.Width(length), GUILayout.Height(length));
            return val;
        }
    }
}