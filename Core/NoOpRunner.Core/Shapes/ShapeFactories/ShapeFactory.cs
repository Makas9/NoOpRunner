﻿using System;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes.EntityShapes;

namespace NoOpRunner.Core.Shapes.ShapeFactories
{
    public class ShapeFactory : Factory
    {
        public override BaseShape GetShape(Shape shape, int x, int y)
        {
            Logging.Instance.Write("Factory used (" + shape + ")");

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
