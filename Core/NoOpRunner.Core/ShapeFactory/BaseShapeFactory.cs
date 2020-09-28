using NoOpRunner.Core.Entities;

namespace NoOpRunner.Core.ShapeFactory
{
    public abstract class BaseShapeFactory
    {
        //TODO: need more shapes for factory
        public abstract BaseShape CreateTrap();

        public abstract BaseShape CreatePlatform();
    }
}