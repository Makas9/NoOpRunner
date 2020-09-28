using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.DesignPatterns._AbstractFactory
{
    public abstract class AbstractFactory
    {
        public abstract BaseShape GetShape(Shape shape, int x, int y);
    }
}
