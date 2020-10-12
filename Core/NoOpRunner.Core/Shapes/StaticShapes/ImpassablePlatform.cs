using NoOpRunner.Core.Entities;

namespace NoOpRunner.Core.Shapes
{
    public class ImpassablePlatform : StaticShape
    {
        protected ImpassablePlatform() { } // Needed for JSON deserialization
        public ImpassablePlatform(GenerationStrategy strategy, int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY) 
            : base(strategy, lowerBoundX, lowerBoundY, upperBoundX, upperBoundY) { }

        public override bool CanOverlap(BaseShape other) => false;

        public override void OnCollision(BaseShape other)
        {
            // Do nothing
        }
    }
}
