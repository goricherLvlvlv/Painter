using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Painter.GamePlay
{
    public interface IGame
    {
        public IRoot Root { get; }

        public ICanvas Canvas { get; }

        public IPen Pen { get; }

        public int PenLevel { get; set; }
        public int EraserLevel { get; set; }
        public int DrawLevel { get; }

        public void ChooseEraser();

        public void ChoosePen(string path);

    }
}
