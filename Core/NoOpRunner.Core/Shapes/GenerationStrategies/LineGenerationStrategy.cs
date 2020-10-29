using System.Collections.Generic;

namespace NoOpRunner.Core.Shapes.GenerationStrategies
{
    class LineGenerationStrategy : GenerationStrategy
    {
        public override List<ShapeBlock> GenerateShapeBlocks(int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY, int? startY = null)
        {
            Logging.Instance.Write("[LineGenerationStrategy]: Shape blocks generated", LoggingLevel.Pattern);
            var blocks = new List<ShapeBlock>();
            MapShapeX(blocks, lowerBoundX, startY ?? lowerBoundY, upperBoundX - lowerBoundX);
            return blocks;
        }
    }
}
