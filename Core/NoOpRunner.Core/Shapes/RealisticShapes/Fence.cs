using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes.RealisticShapes
{
    public class Fence : BaseShape
    {
        public Fence(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeY(1, 1, 2, Color.Red);
        }
    }
}
