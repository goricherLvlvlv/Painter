using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Painter.UI
{
    public abstract class UIArg
    {
    }

    public class NullArg : UIArg
    {
    }

    public class PalettePanelArg : UIArg
    {
        public Color color;
    }
}