using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Core
{
    public class GameState
    {
        public PlatformsContainer Platforms { get; set; }

        public Player Player { get; set; }

        public PowerUpsContainer PowerUpsContainer { get; set; }
    }
}
