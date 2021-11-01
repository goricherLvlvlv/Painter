using UnityEngine.EventSystems;
using System;

namespace Painter.GamePlay.UI
{
    public class DragEvent : BaseEvent, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private event Action<PointerEventData> _onBeginDragEvent;
        private event Action<PointerEventData> _onDragEvent;
        private event Action<PointerEventData> _onEndDragEvent;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _onBeginDragEvent?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _onDragEvent?.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _onEndDragEvent?.Invoke(eventData);
        }

        public void AddBeginDragEvent(Action<PointerEventData> action)
        {
            _onBeginDragEvent += action;
        }

        public void AddDragEvent(Action<PointerEventData> action)
        {
            _onDragEvent += action;
        }

        public void AddEndDragEvent(Action<PointerEventData> action)
        {
            _onEndDragEvent += action;
        }

        public override void Clear()
        {
            base.Clear();

            _onBeginDragEvent = null;
            _onDragEvent = null;
            _onEndDragEvent = null;
        }
    }
}