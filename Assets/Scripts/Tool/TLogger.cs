using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Painter.Tool
{
    public static class TLogger
    {
        public static void Log(object message, object sender = null)
        {
            Debug.Log(message);
        }

        public static void Warn(object message, object sender = null)
        {
            Debug.LogWarning(message);
        }

        public static void Error(object message, object sender = null)
        {
            Debug.LogError(message);
        }
    }
}