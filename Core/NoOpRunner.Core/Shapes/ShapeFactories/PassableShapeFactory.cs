using System;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes.EntityShapes;
using NoOpRunner.Core.Shapes.GenerationStrategies;
using NoOpRunner.Core.Shapes.StaticShapes;

namespace NoOpRunner.Core.Shapes.ShapeFactories
{
    class PassableShapeFactory : AbstractFactory
    {
        public override EntityShape CreateEntityShape(Shape shape, int x, int y)
        {
            switch (shape)
            {
                case Shape.HealthCrystal: return new HealthCrystal(x, y);
                case Shape.DamageCrystal: return new DamageCrystal(x, y);
                default: throw new ArgumentException("Shape is not found");
            }
        }
        public override StaticShape CreateStaticShape(Shape shape, GenerationStrategy strategy, int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY)
        {
            switch (shape)
            {
                case Shape.Platform: return new PassablePlatform(strategy, lowerBoundX, lowerBoundY, upperBoundX, upperBoundY);
                default: throw new ArgumentException("Shape is not found");
            }
        }

    }
}
