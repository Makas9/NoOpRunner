using System.Collections.Generic;

namespace NoOpRunner.Core.Shapes.GenerationStrategies
{
    class StairGenerationStrategy : GenerationStrategy
    {
        public override List<ShapeBlock> GenerateShapeBlocks(int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY, int? startY)
        {
            Logging.Instance.Write("Stair generation strategy used.");

            var blocks = new List<ShapeBlock>();
            for (int curX = lowerBoundX, curY = startY ?? lowerBoundY; curX < upperBoundX && curY < upperBoundY; ++curX, ++curY)
            {
                if (curX != lowerBoundX)
                    AddShapeBlock(blocks, curX, curY - 1);
                AddShapeBlock(blocks, curX, curY);
            }
            return blocks;
        }
    }
}
