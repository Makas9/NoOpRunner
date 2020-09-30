using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes.EntityShapes
{
    class DamageCrystal : EntityShape
    {
        public DamageCrystal(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeY(1, 1, 2, Color.Red);
        }
    }
}
