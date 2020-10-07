﻿using Newtonsoft.Json;
using NoOpRunner.Core.Shapes;
using System.Collections.Generic;
using System.Linq;

namespace NoOpRunner.Core.Entities
{
    public abstract class BaseShape
    {
        public int CenterPosX { get; set; }
        public int CenterPosY { get; set; }

        [JsonProperty]
        protected List<ShapeBlock> ShapeBlocks = new List<ShapeBlock>();

        /// <summary>
        /// Generate a shape somewhere in the given region using the provided strategy.
        /// CenterPos will be set to the position of the first generated ShapeBlock
        /// </summary>
        public BaseShape(GenerationStrategy strategy, int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY)
        {
            var blocks = strategy.GenerateShapeBlocks(lowerBoundX, lowerBoundY, upperBoundX, upperBoundY);
            CenterPosX = blocks[0].OffsetX;
            CenterPosY = blocks[0].OffsetY;
            ShapeBlocks = GenerationStrategy.MakeRelative(blocks, CenterPosX, CenterPosY);
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

        public virtual void OnLoopFired(WindowPixel[,] gameScreen) { }

        public virtual List<WindowPixel> Render()
        {
            var windowPixels = new List<WindowPixel>();

            //Could use Flyweight pattern or Prototype pattern in the future
            ShapeBlocks.ForEach(x =>
            {
                var absX = CenterPosX + x.OffsetX;
                var absY = CenterPosY + x.OffsetY;

                windowPixels.Add(new WindowPixel(absX, absY, isShape: true));
            });

            return windowPixels;
        }

        public bool IsHit(int x, int y)
        {
            return ShapeBlocks.Any(s => s.OffsetX + CenterPosX == x && s.OffsetY + CenterPosY == y);
        }

        public virtual void OnClick()
        {
            // Do nothing by default
        }

        public abstract bool CanOverlap(BaseShape other);
        public abstract void OnCollision(BaseShape other);
    }
}
