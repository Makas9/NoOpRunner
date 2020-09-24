using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes
{
    class ShapeFactory : AbstractFactory
    {
        public override BaseShape GetShape(Shape shape, int x, int y)
        {
            switch (shape)
            {
                case Shape.Square: return new Square(x, y);
                case Shape.Circle: return new Circle(x, y);
                case Shape.Rectangle: return new Rectangle(x, y);
            }

            return null;
        }

    }
}
