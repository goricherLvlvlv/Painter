using Painter.GamePlay;
using Painter.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Painter.UI
{
    public abstract class BasePanel : MonoBehaviour
    {
        protected IGame _game => MGame.Fetch().Current;

        protected virtual void Awake()
        {
            InitComponent();
        }

        protected virtual void InitComponent()
        {

        }

        protected void CloseSelf()
        {
            Destroy(gameObject);
        }
    }
}