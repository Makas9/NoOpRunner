namespace NoOpRunner.Core
{
    public class WindowPixel
    {
        public WindowPixel(int x, int y, bool isShape)
        {
            X = x;
            Y = y;
            IsShape = isShape;
        }

        public readonly int X;

        public readonly int Y;

        public bool IsShape;
    }
}
