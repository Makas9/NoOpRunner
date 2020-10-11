﻿using NoOpRunner.Core.Enums;
using System;
using NoOpRunner.Core.Entities.EntityShapes;

namespace NoOpRunner.Core.Entities.ShapeFactories
{
    class PassableShapeFactory : AbstractFactory
    {
        public override EntityShape CreateEntityShape(Shape shape, int x, int y)
        {
            switch (shape)
            {
                case Shape.HealthCrystal: return new HealthCrystal(x, y);
                case Shape.DamageCrystal: return new DamageCrystal(x, y);
                default: throw new ArgumentException("Shape is not found");
            }
        }
        public override StaticShape CreateStaticShape(Shape shape, int x, int y, int bottomPosY = 0, int topPosY = 0)
        {
            switch (shape)
            {
                case Shape.Platform: return new PassablePlatform(x, y, bottomPosY, topPosY);
                default: throw new ArgumentException("Shape is not found");
            }
        }

    }
}
