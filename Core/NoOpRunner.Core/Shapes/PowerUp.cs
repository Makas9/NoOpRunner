using Newtonsoft.Json;
using NoOpRunner.Core.Enums;
using System.Linq;

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

        public override void ShiftBlocks()
        {
            var isVisible = ShapeBlocks.Any();

            base.ShiftBlocks();

            if (!ShapeBlocks.Any() && PowerUpType == PowerUps.Health_Crystal && isVisible)
            {
                Map?.Notify("HealPlayerTwo");
            }
        }
    }
}
