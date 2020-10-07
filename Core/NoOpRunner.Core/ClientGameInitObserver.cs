using System;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Core
{
    public class ClientGameInitObserver : IObserver
    {
        public void Update(object sender, object args)
        {
            var gameState = args as Dtos.GameStateUpdateDto;
            
            ((NoOpRunner) sender).FireClientLoop(gameState.Platforms, gameState.Player);
        }
    }
}