using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painter.GamePlay
{
    public interface IGame
    {
        public IRoot Root { get; }

        public ICanvas Canvas { get; }

        public IPen Pen { get; }

        public void ChooseEraser();

        public void ChoosePen(string path);

    }
}
