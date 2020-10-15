using NoOpRunner.Core.Builders;
using NoOpRunner.Core.Configurators;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes.EntityShapes;
using NoOpRunner.Core.Shapes.GenerationStrategies;
using System;

namespace NoOpRunner.Core.Shapes.ShapeFactories
{
    public class PassableShapeFactory : AbstractFactory
    {
        private readonly ShapeBuilder<PassablePlatformConfigurator> passablePlatformBuilder;

        public PassableShapeFactory()
        {
            passablePlatformBuilder = new ShapeBuilder<PassablePlatformConfigurator>();
        }

        public override EntityShape CreateEntityShape(Shape shape, int x, int y)
        {
            Logging.Instance.Write("Abstract Factory -> PassableShapeFactory used (" + shape + ")");

            switch (shape)
            {
                case Shape.HealthCrystal: return new HealthCrystal(x, y);
                case Shape.DamageCrystal: return new DamageCrystal(x, y);
                default: throw new ArgumentException("Shape is not found");
            }
        }
        public override BaseShape CreateStaticShape(Shape shape, GenerationStrategy strategy, int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY)
        {
            Logging.Instance.Write("Abstract Factory -> PassableShapeFactory used (" + shape + ")");

            switch (shape)
            {
                case Shape.Platform:
                    return passablePlatformBuilder
                           .Configure()
                           .ConfigureBounds(lowerBoundX, lowerBoundY, upperBoundX, upperBoundY)
                           .ConfigureGenerationStrategy(strategy)
                           .Build();

                default: throw new ArgumentException("Shape is not found");
            }
        }

    }
}
