using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes.RegularShapes
{
    public class Rectangle : GeometricShape
    {
        public Rectangle(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeX(0, 1, 1, Color.Red);
            MapShapeX(0, 0, 1, Color.Red);
            MapShapeX(0, -1, 1, Color.Red);
        }
    }
}
