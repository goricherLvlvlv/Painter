using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Painter.UI
{
    public static class UIExtension
    {
        #region Event

        private static T Fetch<T>(this GameObject gameObject, bool autoAdd = false) where T : Component
        {
            var result = gameObject.TryGetComponent<T>(out var component);
            if (!result && autoAdd)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }

        public static void AddClickEvent(this GameObject gameObject, Action action)
        {
            gameObject.Fetch<ClickEvent>(true).AddClickEvent(action);
        }

        public static void AddBeginDragEvent(this GameObject gameObject, Action<PointerEventData> action)
        { 
            gameObject.Fetch<DragEvent>(true).AddBeginDragEvent(action);
        }

        public static void AddDragEvent(this GameObject gameObject, Action<PointerEventData> action)
        {
            gameObject.Fetch<DragEvent>(true).AddDragEvent(action);
        }

        public static void AddEndDragEvent(this GameObject gameObject, Action<PointerEventData> action)
        {
            gameObject.Fetch<DragEvent>(true).AddEndDragEvent(action);
        }

        public static void ClearEvent(this GameObject gameObject)
        {
            gameObject.Fetch<ClickEvent>(false)?.Clear();
            gameObject.Fetch<DragEvent>(false)?.Clear();
        }

        #endregion
    }
}