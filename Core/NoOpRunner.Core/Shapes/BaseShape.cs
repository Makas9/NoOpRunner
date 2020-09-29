using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoOpRunner.Core.Entities
{
    public abstract class BaseShape
    {
        public int CenterPosX { get; set; }
        public int CenterPosY { get; set; }

        protected List<ShapeBlock> ShapeBlocks = new List<ShapeBlock>();

        public BaseShape(int centerPosX, int centerPosY)
        {
            CenterPosX = centerPosX;
            CenterPosY = centerPosY;

            ShapeBlocks = new List<ShapeBlock>();
        }

        public (int[], int[]) GetCoords()
        {
            List<int> xCoords = new List<int>();
            List<int> yCoords = new List<int>();

            ShapeBlocks.ForEach(x =>
            {
                var absX = CenterPosX + x.OffsetX;
                var absY = CenterPosY + x.OffsetY;

                xCoords.Add(absX);
                yCoords.Add(absY);
            });

            return (xCoords.ToArray(), yCoords.ToArray());
        }

        protected BaseShape MapShapeX(int offsetX, int offsetY, int length, Color color)
        {
            for (int i = 0; i < length; i++)
            {
                AddShapeBlock(offsetX + i, offsetY, color);
            }

            return this;
        }

        protected BaseShape MapShapeY(int offsetX, int offsetY, int length, Color color)
        {
            for (int i = 0; i < length; i++)
            {
                AddShapeBlock(offsetX, offsetY + i, color);
            }

            return this;
        }

        public virtual void OnLoopFired(WindowPixel[,] gameScreen) { }

        public virtual List<WindowPixel> Render()
        {
            var windowPixels = new List<WindowPixel>();

            ShapeBlocks.ForEach(x =>
            {
                var absX = CenterPosX + x.OffsetX;
                var absY = CenterPosY + x.OffsetY;

                windowPixels.Add(new WindowPixel(absX, absY, x.Color, true));
            });

            return windowPixels;
        }

        public bool IsHit(int x, int y)
        {
            return ShapeBlocks.Any(s => s.OffsetX + CenterPosX == x && s.OffsetY + CenterPosY == y);
        }

        private void AddShapeBlock(int offsetX, int offsetY, Color color)
        {
            ShapeBlocks.Add(new ShapeBlock
            {
                OffsetX = offsetX,
                OffsetY = offsetY,
                Color = color
            });
        }

        public virtual void OnClick()
        {
            ShapeBlocks.ForEach(x =>
            {
                x.Color = Color.Green;
            });
        }
    }
}
