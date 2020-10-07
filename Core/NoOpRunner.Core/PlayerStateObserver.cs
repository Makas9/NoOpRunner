using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;

namespace NoOpRunner.Core
{
    public class PlayerStateObserver : IObserver
    {
        public void Update(object sender, object args)
        {
            var state = args as Dtos.PlayerStateDto;
            
            ((NoOpRunner) sender).Player.State = state.State;
            
            ((NoOpRunner) sender).Player.IsLookingLeft = state.IsLookingLeft;
        }
    }
}