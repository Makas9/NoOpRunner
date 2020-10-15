using NoOpRunner.Core.Builders;
using NoOpRunner.Core.Configurators;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes.EntityShapes;
using NoOpRunner.Core.Shapes.GenerationStrategies;
using System;

namespace NoOpRunner.Core.Shapes.ShapeFactories
{
    public class ImpassableShapeFactory : AbstractFactory
    {
        private readonly ShapeBuilder<ImpassablePlatformConfigurator> impassablePlatformBuilder;

        public ImpassableShapeFactory()
        {
            impassablePlatformBuilder = new ShapeBuilder<ImpassablePlatformConfigurator>();
        }

        public override EntityShape CreateEntityShape(Shape shape, int x, int y)
        {
            Logging.Instance.Write("Abstract Factory -> ImpassableShapeFactory used (" + shape + ")");

            switch (shape)
            {
                case Shape.Saw: return new Saw(x, y);
                case Shape.Rocket: return new Rocket(x, y);
                default: throw new ArgumentException("Shape is not found");
            }
        }

        public override BaseShape CreateStaticShape(Shape shape, GenerationStrategy strategy, int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY)
        {
            Logging.Instance.Write("Abstract Factory -> ImpassableShapeFactory used (" + shape + ")");

            switch (shape)
            {
                case Shape.Platform:
                    return impassablePlatformBuilder
                      .Configure()
                      .ConfigureBounds(lowerBoundX, lowerBoundY, upperBoundX, upperBoundY)
                      .ConfigureGenerationStrategy(strategy)
                      .Build();
                default: throw new ArgumentException("Shape is not found");
            }
        }

    }
}
