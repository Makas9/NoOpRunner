using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core
{
    public class WindowPixel
    {
        public WindowPixel(int x, int y, Color color, bool isShape)
        {
            X = x;
            Y = y;
            Color = color;
            IsShape = isShape;
        }

        public readonly int X;

        public readonly int Y;

        public Color Color { get; set; }

        public bool IsShape;
    }
}
