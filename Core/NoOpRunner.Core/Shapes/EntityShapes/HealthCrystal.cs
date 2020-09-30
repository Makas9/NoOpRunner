using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes.EntityShapes
{
    class HealthCrystal : EntityShape
    {
        public HealthCrystal(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeY(1, 1, 2, Color.Green);
        }

        public override void OnCollision(BaseShape other)
        {
            if (other.GetType() == typeof(Player))
            {
                other.DoHeal(1);
            }
        }
    }
}
