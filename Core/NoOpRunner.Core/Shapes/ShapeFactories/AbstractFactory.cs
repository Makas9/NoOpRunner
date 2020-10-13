using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes.ShapeFactories
{
    public abstract class AbstractFactory
    {
        public abstract EntityShape CreateEntityShape(Shape shape, int x, int y);
        public abstract BaseShape CreateStaticShape(Shape shape, GenerationStrategy strategy, int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY);
    }
}
