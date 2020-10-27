using System.Linq;
using NoOpRunner.Core.Shapes.GenerationStrategies;

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
        public void ShiftAndUpdate()
        {
            ShapeBlocks.ForEach(x=> x.OffsetX--);
            ShapeBlocks.RemoveAll(x => x.OffsetX < 0);

            var lastShape = ShapeBlocks.Last();
            if (lastShape.OffsetX < GameSettings.HorizontalCellCount)
            {
                // Generate blocks for the next screen
                var blocks = Strategy.GenerateShapeBlocks(lastShape.OffsetX + 1, lowerBoundY, GameSettings.HorizontalCellCount * 2, upperBoundY, lastShape.OffsetY + CenterPosY);
                ShapeBlocks.AddRange(GenerationStrategy.MakeRelative(blocks, CenterPosX, CenterPosY));
            }
        }
    }
}
