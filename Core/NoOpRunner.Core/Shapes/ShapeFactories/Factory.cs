using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Entities.ShapeFactories
{
    public abstract class Factory
    {
        public abstract BaseShape GetShape(Shape shape, int x, int y);
    }
}
