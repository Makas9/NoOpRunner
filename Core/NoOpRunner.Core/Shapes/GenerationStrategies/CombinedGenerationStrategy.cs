using System;
using System.Collections.Generic;
using System.Linq;

namespace NoOpRunner.Core.Shapes.GenerationStrategies
{
    class CombinedGenerationStrategy : GenerationStrategy
    {
        public override List<ShapeBlock> GenerateShapeBlocks(int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY)
        {
            var blocks = new List<ShapeBlock>();

            // This is a bit stupid, but it won't be the strategy pattern if I make the methods static
            var lineStrat = new LineGenerationStrategy();
            var newX = Math.Min(lowerBoundX + 4, upperBoundX);
            blocks.AddRange(lineStrat.GenerateShapeBlocks(lowerBoundX, lowerBoundY, newX, upperBoundY));

            var stairStrat = new StairGenerationStrategy();
            blocks.AddRange(stairStrat.GenerateShapeBlocks(newX, lowerBoundY, upperBoundX, Math.Min(lowerBoundY + (upperBoundY - lowerBoundY)/2, upperBoundY)));

            blocks.AddRange(lineStrat.GenerateShapeBlocks(blocks.Last().OffsetX + 1, blocks.Last().OffsetY, upperBoundX, upperBoundY));

            return blocks;
        }
    }
}
