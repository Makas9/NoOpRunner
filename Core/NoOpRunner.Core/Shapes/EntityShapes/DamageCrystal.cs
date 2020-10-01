using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;
using System;

namespace NoOpRunner.Core.Shapes.EntityShapes
{
    class DamageCrystal : EntityShape
    {
        public DamageCrystal(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeY(1, 1, 2, Color.Red);
        }

        public override bool CanOverlap(BaseShape other)
        {
            throw new NotImplementedException();
        }

        public override void OnCollision(BaseShape other)
        {
            if (other is Player p)
            {
                p.ModifyHealth(heal: false, 1);
            }
        }
    }
}
