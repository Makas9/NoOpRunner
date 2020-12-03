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

        public int X { get; set; }

        public int Y { get; set; }

        public bool IsShape { get; set; }
    }
}
