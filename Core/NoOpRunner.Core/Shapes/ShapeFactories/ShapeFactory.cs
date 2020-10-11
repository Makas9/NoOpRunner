using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Enums;
using System;
using NoOpRunner.Core.Entities.EntityShapes;

namespace NoOpRunner.Core.Entities.ShapeFactories
{
    class ShapeFactory : Factory
    {
        public override BaseShape GetShape(Shape shape, int x, int y)
        {
            switch (shape)
            {
                case Shape.HealthCrystal: return new HealthCrystal(x, y);
                case Shape.DamageCrystal: return new DamageCrystal(x, y);
                case Shape.Saw: return new Saw(x, y);
                case Shape.Rocket: return new Rocket(x, y);
                default: throw new ArgumentException("Shape is not found");
            }
        }
    }
}
