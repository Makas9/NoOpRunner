using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes.EntityShapes;
using System;

namespace NoOpRunner.Core.Shapes.ShapeFactories
{
    class ImpassableShapeFactory : AbstractFactory
    {
        public override EntityShape CreateEntityShape(Shape shape, int x, int y)
        {
            switch (shape)
            {
                case Shape.Saw: return new Saw(x, y);
                case Shape.Rocket: return new Rocket(x, y);
                default: throw new ArgumentException("Shape is not found");
            }
        }

        public override StaticShape CreateStaticShape(Shape shape, GenerationStrategy strategy, int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY)
        {
            switch (shape)
            {
                case Shape.Platform: return new ImpassablePlatform(strategy, lowerBoundX, lowerBoundY, upperBoundX, upperBoundY);
                default: throw new ArgumentException("Shape is not found");
            }
        }

    }
}
