using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes.RealisticShapes
{
    class Fence : RealisticShape
    {
        public Fence(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeY(1, 1, 2, Color.Red);
        }
    }
}
