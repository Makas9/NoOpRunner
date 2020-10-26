using System;

namespace NoOpRunner.Core
{
    public  static class GameSettings
    {
        private const int AspectRatioWidth = 16;
        private const int AspectRatioHeight = 9;

        private const int CellsSizeMultiplier = 4;

        public static readonly TimeSpan MoveAnimationDuration = TimeSpan.FromMilliseconds(1000 / 20);
        
        public const int TimeBetweenFramesMs = 70;

        public static int TimeBetweenMapStep => 1000 / 70;

        public static int HorizontalCellCount => AspectRatioWidth * CellsSizeMultiplier;
        public static int VerticalCellCount => AspectRatioHeight * CellsSizeMultiplier;
    }
}