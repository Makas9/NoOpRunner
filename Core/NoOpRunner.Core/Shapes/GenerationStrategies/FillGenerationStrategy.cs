﻿using System.Collections.Generic;

namespace NoOpRunner.Core.Shapes.GenerationStrategies
{
    class FillGenerationStrategy : GenerationStrategy
    {
        public override List<ShapeBlock> GenerateShapeBlocks(int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY)
        {
            var blocks = new List<ShapeBlock>();
            for (var curX = lowerBoundX; curX < upperBoundX; ++curX)
            {
                for (var curY = lowerBoundY; curY < upperBoundY; ++curY)
                {
                    AddShapeBlock(blocks, curX, curY);
                }
            }
            return blocks;
        }
    }
}
