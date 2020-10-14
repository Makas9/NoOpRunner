using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Core.Dtos
{
    public class GameStateDto
    {
        public Player Player { get; set; }

        public PlatformsContainer Platforms { get; set; }
        
        public PowerUpsContainer PowerUps { get; set; }
    }
}
