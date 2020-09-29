using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes.ShapeFactories
{
    public abstract class Factory
    {
        public abstract BaseShape GetShape(Shape shape, int x, int y);
    }
}
