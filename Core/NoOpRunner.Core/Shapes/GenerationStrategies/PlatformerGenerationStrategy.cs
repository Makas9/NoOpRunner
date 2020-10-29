using System;
using System.Collections.Generic;

namespace NoOpRunner.Core.Shapes.GenerationStrategies
{
    class PlatformerGenerationStrategy : GenerationStrategy
    {
        public override List<ShapeBlock> GenerateShapeBlocks(int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY, int? startY = null)
        {
            var blocks = new List<ShapeBlock>();
            var random = RandomNumber.GetInstance();
            var curY = startY ?? lowerBoundY;

            for (var curX = lowerBoundX; curX < upperBoundX; curX += random.Next(2, 4))
            {
                var length = Math.Min(random.Next(2, 5), upperBoundX - curX);
                MapShapeX(blocks, curX, curY, length);
                curX += length;
                curY = Math.Max(lowerBoundY, Math.Min(curY + random.Next(-3, 4), upperBoundY));
            }

            return blocks;
        }
    }
}
