using Newtonsoft.Json;
using NoOpRunner.Core.Enums;

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
    }
}
