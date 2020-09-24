using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes
{
    class RealisticShapeFactory : AbstractFactory
    {
        public override BaseShape GetShape(Shape shape, int x, int y)
        {
            switch (shape)
            {
                case Shape.Stairs: return new Stairs(x, y);
            }

            return null;
        }
    }
}
