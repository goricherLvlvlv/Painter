using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Painter.GamePlay
{
    public class Eraser : Pen
    {
        public override PenType Type => PenType.Eraser;

        public override Color DrawColor => Color.white;
    }
}