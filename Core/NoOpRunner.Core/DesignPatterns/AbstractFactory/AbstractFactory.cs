using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes
{
    public abstract class AbstractFactory
    {
        public abstract BaseShape GetShape(Shape shape, int x, int y);
    }
}
