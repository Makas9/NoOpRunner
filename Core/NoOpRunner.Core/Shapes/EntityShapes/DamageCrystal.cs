using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes.EntityShapes
{
    class DamageCrystal : EntityShape
    {
        public DamageCrystal(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeY(1, 1, 2, Color.Red);
        }

        public override void OnCollision(BaseShape other)
        {
            if (other.GetType() == typeof(Player))
            {
                other.DoDamage(1);
            }
        }
    }
}
