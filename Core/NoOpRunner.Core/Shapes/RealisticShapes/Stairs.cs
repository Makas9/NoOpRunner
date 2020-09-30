using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes.RealisticShapes
{
    public class Stairs : RealisticShape
    {
        public Stairs(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeX(1, 1, 1, Color.Red);
            MapShapeX(0, 0, 2, Color.Red);
            MapShapeX(-1, -1, 3, Color.Red);
        }
    }
}
