using UnityEngine;

namespace Painter.GamePlay
{
    public enum PenType
    {
        Pen,
        Eraser,
    }

    public interface IPen
    {
        public PenType Type { get; }

        public Color Color { get; set; }

        public Color DrawColor { get; }

        public string Name { get; set; }
    }
}
