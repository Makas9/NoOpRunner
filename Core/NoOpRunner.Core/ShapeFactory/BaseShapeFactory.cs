using NoOpRunner.Core.Entities;

namespace NoOpRunner.Core.ShapeFactory
{
    public abstract class BaseShapeFactory
    {
        public abstract BaseShape CreateTrap();

        public abstract BaseShape CreatePlatform();
    }
}