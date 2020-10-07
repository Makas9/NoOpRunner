using System;
using System.Collections.Generic;

namespace NoOpRunner.Core.Shapes.GenerationStrategies
{
    class RandomlySegmentedGenerationStrategy : GenerationStrategy
    {
        public override List<ShapeBlock> GenerateShapeBlocks(int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY)
        {
            Logging.Instance.Write("Randomly segmented generation strategy used.");

            var blocks = new List<ShapeBlock>();
            var random = RandomNumber.GetInstance();

            var curY = random.Next(lowerBoundY, upperBoundY);
            for (int curX = lowerBoundX; curX < upperBoundX;)
            {
                var vertical = random.NextDouble() >= 0.5;
                if (vertical)
                {
                    var blockLength = random.Next(2, 3);
                    var downwards = random.NextDouble() >= 0.5;
                    int newY;
                    if (downwards)
                    {
                        newY = Math.Max(curY - blockLength, lowerBoundY);
                        MapShapeY(blocks, curX, newY, curY - newY + 1);
                    }
                    else
                    {
                        newY = Math.Min(curY + blockLength, upperBoundY - 1);
                        MapShapeY(blocks, curX, curY, newY - curY + 1);
                    }
                    curY = newY;
                    curX += 1;
                }
                else
                {
                    var blockLength = random.Next(2, 4);
                    var newX = Math.Min(curX + blockLength, upperBoundX - 1);
                    MapShapeX(blocks, curX, curY, newX - curX + 1);
                    curX = newX + 1;
                }
            }

            return blocks;
        }
    }
}
