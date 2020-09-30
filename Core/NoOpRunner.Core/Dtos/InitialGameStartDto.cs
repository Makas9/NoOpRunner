using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Core.Dtos
{
    public class GameStateUpdateDto
    {
        public Player Player { get; set; }

        public GamePlatforms Platforms { get; set; }
    }
}
