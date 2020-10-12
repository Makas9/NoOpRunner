using NoOpRunner.Core.Entities;

namespace NoOpRunner.Core.Shapes
{
    public abstract class StaticShape : BaseShape
    {
        protected StaticShape() { } // Needed for JSON deserialization
        public StaticShape(GenerationStrategy strategy, int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY) 
            : base(strategy, lowerBoundX, lowerBoundY, upperBoundX, upperBoundY) { }
    }
}
