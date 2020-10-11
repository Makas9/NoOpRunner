using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Entities.ShapeFactories
{
    public abstract class AbstractFactory
    {
        public abstract EntityShape CreateEntityShape(Shape shape, int x, int y);
        public abstract StaticShape CreateStaticShape(Shape shape, int x, int y, int bottomPosY = 0, int topPosY = 0);
    }
}
