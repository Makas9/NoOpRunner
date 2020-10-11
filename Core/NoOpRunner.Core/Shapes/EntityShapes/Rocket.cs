using NoOpRunner.Core.Enums;
using System;

namespace NoOpRunner.Core.Entities.EntityShapes
{
    class Rocket : EntityShape
    {
        public Rocket(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeY(1, 1, 2, Color.Blue);
        }

        public override bool CanOverlap(BaseShape other)
        {
            return false;
        }

        public override void OnCollision(BaseShape other)
        {
            if (other is Player p)
            {
                //p.ModifyHealth(heal: false, 1);
                throw new NotImplementedException();
            }
        }
    }
}
