using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes.GenerationStrategies;

namespace NoOpRunner.Core.Shapes.ShapeFactories
{
    public abstract class AbstractFactory
    {
        public abstract EntityShape CreateEntityShape(Shape shape, int x, int y);
        public abstract StaticShape CreateStaticShape(Shape shape, GenerationStrategy strategy, int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY);
    }
}
