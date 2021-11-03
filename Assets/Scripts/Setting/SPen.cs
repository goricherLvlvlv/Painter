using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Painter.Setting
{
    public class SPen : SettingBase<SPen>
    {
        [SerializeField]
        private bool[] _3x3blocks = new bool[9];

        [SerializeField]
        private bool[] _5x5blocks = new bool[25];

        [SerializeField]
        private bool[] _7x7blocks = new bool[49];

        [SerializeField]
        private bool[] _9x9blocks = new bool[81];

        [SerializeField]
        private bool[] _11x11blocks = new bool[121];

        public bool[] Blocks3 => _3x3blocks;

        public bool[] Blocks5 => _5x5blocks;

        public bool[] Blocks7 => _7x7blocks;

        public bool[] Blocks9 => _9x9blocks;

        public bool[] Blocks11 => _11x11blocks;
    }
}