using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes.RealisticShapes
{
    public class Stone : RealisticShape
    {
        public Stone(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeX(1, 1, 1, Color.Red);
        }
    }
}
