using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;
using System;

namespace NoOpRunner.Core.Shapes.EntityShapes
{
    class HealthCrystal : EntityShape
    {
        public HealthCrystal(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeY(1, 1, 2, Color.Green);
        }

        public override bool CanOverlap(BaseShape other)
        {
            throw new NotImplementedException();
        }

        public override void OnCollision(BaseShape other)
        {
            if (other is Player p)
            {
                p.ModifyHealth(1);
            }
        }
    }
}
