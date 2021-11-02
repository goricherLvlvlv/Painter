using UnityEngine.EventSystems;
using System;

namespace Painter.UI
{
    public class ClickEvent : BaseEvent, IPointerClickHandler
    {
        private event Action _onClickEvent;

        public void OnPointerClick(PointerEventData eventData)
        {
            _onClickEvent?.Invoke();
        }

        public void AddClickEvent(Action action)
        {
            _onClickEvent += action;
        }

        public override void Clear()
        {
            base.Clear();

            _onClickEvent = null;
        }
    }
}