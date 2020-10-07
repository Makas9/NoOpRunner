using NoOpRunner.Core.Entities;

namespace NoOpRunner.Core.Shapes
{
    public abstract class StaticShape : BaseShape
    {
        public int LastPosX = 0,
                   LastPosY = 0;

        public StaticShape(GenerationStrategy strategy, int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY) 
            : base(strategy, lowerBoundX, lowerBoundY, upperBoundX, upperBoundY) { }
    }
}
