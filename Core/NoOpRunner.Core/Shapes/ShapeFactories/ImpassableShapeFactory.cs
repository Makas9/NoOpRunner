using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes.EntityShapes;
using System;

namespace NoOpRunner.Core.Shapes.ShapeFactories
{
    class ImpassableShapeFactory : AbstractFactory
    {
        public override EntityShape CreateEntityShape(Shape shape, int x, int y)
        {
            switch (shape)
            {
                case Shape.Saw: return new Saw(x, y);
                case Shape.Rocket: return new Rocket(x, y);
                default: throw new Exception("Shape is not found");
            }
        }
        public override StaticShape CreateStaticShape(Shape shape, int x, int y, int bottomPosY = 0, int topPosY = 0)
        {
            switch (shape)
            {
                case Shape.Platform: return new ImpassablePlatform(x, y, bottomPosY, topPosY);
                default: throw new Exception("Shape is not found");
            }
        }

    }
}
