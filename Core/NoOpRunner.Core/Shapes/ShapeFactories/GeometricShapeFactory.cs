using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes.RegularShapes;
using System;

namespace NoOpRunner.Core.Shapes.ShapeFactories
{
    class GeometricShapeFactory : AbstractFactory
    {
        public override BaseShape GetShape(Shape shape, int x, int y)
        {
            switch (shape)
            {
                case Shape.Square: return new Square(x, y);
                case Shape.Circle: return new Circle(x, y);
                case Shape.Rectangle: return new Rectangle(x, y);
                default: throw new Exception("Shape is not found");
            }
        }

    }
}
