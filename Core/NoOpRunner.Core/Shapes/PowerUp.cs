using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes.EntityShapes;
using System;

namespace NoOpRunner.Core.Shapes
{
    public class PowerUp : EntityShape
    {
        public readonly PowerUps PowerUpType;

        public PowerUp(int x, int y, PowerUps powerup) : base(x, y)
        {
            this.PowerUpType = powerup;
        }

        public override bool CanOverlap(BaseShape other)
        {
            if (other is Player ||
                other is Rocket)
                return true;

            return false;
        }

        public override void OnCollision(BaseShape other)
        {
            throw new NotImplementedException();
        }
    }
}
