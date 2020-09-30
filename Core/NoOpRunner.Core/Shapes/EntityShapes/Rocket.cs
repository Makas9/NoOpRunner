using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes.EntityShapes
{
    class Rocket : EntityShape
    {
        public Rocket(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeY(1, 1, 2, Color.Blue);
        }
    }
}
