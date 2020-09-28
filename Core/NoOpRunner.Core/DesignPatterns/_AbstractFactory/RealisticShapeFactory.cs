using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes.RealisticShapes;
using System;

namespace NoOpRunner.Core.DesignPatterns._AbstractFactory
{
    class RealisticShapeFactory : AbstractFactory
    {
        public override BaseShape GetShape(Shape shape, int x, int y)
        {
            switch (shape)
            {
                case Shape.Stairs: return new Stairs(x, y);
                case Shape.Stone: return new Stone(x, y);
                case Shape.Fence: return new Fence(x, y);
                default: throw new Exception("Shape is not found");
            }
        }
    }
}
