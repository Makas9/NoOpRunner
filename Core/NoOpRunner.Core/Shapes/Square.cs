using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes
{
    public class Square : BaseShape
    {
        public Square(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeX(-1, -1, 3, Color.Red);
            MapShapeX(-1, 0, 3, Color.Red);
            MapShapeX(-1, 1, 3, Color.Red);
        }
    }
}
