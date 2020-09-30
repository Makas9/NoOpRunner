using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes.EntityShapes
{
    class Saw : EntityShape
    {
        public Saw(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeY(1, 1, 2, Color.Blue);
        }
    }
}
