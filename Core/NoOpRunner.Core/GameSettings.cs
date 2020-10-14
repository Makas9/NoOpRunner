namespace NoOpRunner.Core
{
    public  static class GameSettings
    {
        private const int AspectRatioWidth = 16;
        private const int AspectRatioHeight = 9;

        private const int CellsSizeMultiplier = 4;

        public static int HorizontalCellCount => AspectRatioWidth * CellsSizeMultiplier;
        public static int VerticalCellCount => AspectRatioHeight * CellsSizeMultiplier;
    }
}