﻿using NoOpRunner.Core.Shapes.GenerationStrategies;

namespace NoOpRunner.Core.Shapes.StaticShapes
{
    public class PassablePlatform : StaticShape
    {
        protected PassablePlatform() { } // Needed for JSON deserialization
        public PassablePlatform(GenerationStrategy strategy, int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY)
            : base(strategy, lowerBoundX, lowerBoundY, upperBoundX, upperBoundY) { }


        public override bool CanOverlap(BaseShape other)
        {
            // TODO: Can overlap on conditions (i.e. rockets always, player depending on keys pressed?)
            return true;
        }

        public override void OnCollision(BaseShape other)
        {
            // Do nothing
        }
    }
}
