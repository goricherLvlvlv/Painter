using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Painter.Manager
{
    public class ManagerBase<T>
    {
        private static T _instance;

        public static T Fetch()
        {
            if (_instance == null)
            {
                _instance = Activator.CreateInstance<T>();
            }
            return _instance;
        }
    }
}