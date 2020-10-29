using System;
using Newtonsoft.Json;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes.EntityShapes;

namespace NoOpRunner.Core.Shapes
{
    public class PowerUp : EntityShape
    {
        [JsonProperty]
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
