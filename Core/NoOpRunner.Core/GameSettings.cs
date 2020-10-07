﻿namespace NoOpRunner.Core
{
    public  static class GameSettings
    {
        public const int AspectRatioWidth = 16;
        public const int AspectRatioHeight = 9;

        public const int CellsSizeMultiplier = 3;

        public static int HorizontalCellCount => AspectRatioWidth * CellsSizeMultiplier;
        public static int VerticalCellCount => AspectRatioHeight * CellsSizeMultiplier;
    }
}