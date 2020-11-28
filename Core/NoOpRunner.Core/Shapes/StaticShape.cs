using NoOpRunner.Core.Shapes.GenerationStrategies;
using System.Collections.Generic;
using System.Linq;

namespace NoOpRunner.Core.Shapes
{
    public abstract class StaticShape : BaseShape
    {
        protected StaticShape() { } // Needed for JSON deserialization
        public StaticShape(GenerationStrategy strategy, int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY)
            : base(strategy, lowerBoundX, lowerBoundY, upperBoundX, upperBoundY) { }

        /// <summary>
        /// For platforms sliding
        /// platforms CenterPosX is all ways 0
        /// </summary>
        public override void ShiftBlocks()
        {
            ShapeBlocks.ForEach(x => x.OffsetX--);
            ShapeBlocks.RemoveAll(x => x.OffsetX < 0);
        }

        /// <summary>
        /// Get the first out-of-bounds ShapeBlocks, which should be sent to client. Called by host.
        /// </summary>
        public override List<List<ShapeBlock>> GetNextBlocks()
        {
            var nextBlock = ShapeBlocks.FirstOrDefault(x => x.OffsetX >= GameSettings.HorizontalCellCount);
            if (nextBlock is null)
            {
                // Generate blocks for the next screen
                var lastBlock = ShapeBlocks.Last();
                var blocks = Strategy.GenerateShapeBlocks(lastBlock.OffsetX + 1, lowerBoundY, GameSettings.HorizontalCellCount * 2, upperBoundY, lastBlock.OffsetY + CenterPosY);
                nextBlock = blocks.First();
                ShapeBlocks.AddRange(GenerationStrategy.MakeRelative(blocks, CenterPosX, CenterPosY));
            }
            return new List<List<ShapeBlock>> { ShapeBlocks.Where(x => x.OffsetX == nextBlock.OffsetX).ToList() };
        }

        /// <summary>
        /// Add a shape block to the end of the static shape. Called by cliend to append generated shape blocks sent by host
        /// </summary>
        /// <param name="shapeBlock"></param>
        public void AddShapeBlocks(List<ShapeBlock> shapeBlocks)
        {
            ShapeBlocks.AddRange(shapeBlocks);
        }
    }
}
