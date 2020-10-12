using NoOpRunner.Core.Shapes.GenerationStrategies;

namespace NoOpRunner.Core.Shapes
{
    public abstract class EntityShape : BaseShape
    {
        /// <summary>
        /// Creates a single-cell EntityShape at the given coords
        /// </summary>
        public EntityShape(int x, int y) : base(new FillGenerationStrategy(), x, y, x + 1, y + 1) { }

        public EntityShape(GenerationStrategy strategy, int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY)
            : base(strategy, lowerBoundX, lowerBoundY, upperBoundX, upperBoundY) { }
    }
}
