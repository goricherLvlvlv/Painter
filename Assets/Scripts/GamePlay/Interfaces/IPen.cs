using UnityEngine;

namespace Painter.GamePlay
{
    public interface IPen
    {
        public Color Color { get; }

        public string Path { get; set; }
    }
}
