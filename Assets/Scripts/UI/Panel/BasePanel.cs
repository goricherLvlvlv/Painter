using Painter.GamePlay;
using Painter.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Painter.UI
{
    public abstract class BasePanel : MonoBehaviour
    {
        protected static IGame _game => MGame.Fetch().Current;

        protected static IRoot _root => _game?.Root;

        public static void Open<PanelType, ArgType>(ArgType arg) where PanelType : BasePanel where ArgType : UIArg
        {
            var go = _root?.LoadPrefabAtUILayer(typeof(PanelType).Name + ".prefab");
            if (go != null)
            {
                var panel = go.GetComponent<PanelType>();
                panel.Init<ArgType>(arg);
            }
        }

        protected void Close()
        {
            Destroy(gameObject);
        }

        protected virtual void Init<ArgType>(ArgType arg) where ArgType : UIArg
        {

        }

    }
}