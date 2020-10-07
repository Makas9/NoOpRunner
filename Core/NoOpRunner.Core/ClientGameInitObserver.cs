using NoOpRunner.Core.Interfaces;

namespace NoOpRunner.Core
{
    public class ClientGameInitObserver : IObserver
    {
        public void Update(NoOpRunner sender, object args)
        {
            var gameState = args as Dtos.GameStateUpdateDto;

            sender.FireClientLoop(gameState.Platforms, gameState.Player);
        }
    }
}