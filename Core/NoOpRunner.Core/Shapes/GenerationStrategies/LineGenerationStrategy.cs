using System.Collections.Generic;

namespace NoOpRunner.Core.Shapes.GenerationStrategies
{
    class LineGenerationStrategy : GenerationStrategy
    {
        public override List<ShapeBlock> GenerateShapeBlocks(int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY)
        {
            var blocks = new List<ShapeBlock>();
            MapShapeX(blocks, lowerBoundX, lowerBoundY, upperBoundX - lowerBoundX);
            return blocks;
        }
    }
}
