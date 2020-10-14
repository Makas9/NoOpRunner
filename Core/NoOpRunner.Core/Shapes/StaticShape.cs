using System.Collections.Generic;
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
        public void PushAndRemove()
        {
            ShapeBlocks.ForEach(x=> x.OffsetX--);
            ShapeBlocks.RemoveAll(x => x.OffsetX < 0);
        }

        public void AppendPlatform(IList<ShapeBlock> shapeBlocks)
        {
            ShapeBlocks.AddRange(shapeBlocks);
        }
    }
}
