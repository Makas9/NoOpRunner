using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;

namespace NoOpRunner.Core
{
    public class PlayerStateObserver : IObserver
    {
        public void Update(NoOpRunner sender, object args)
        {
            var state = args as Dtos.PlayerStateDto;
            
            sender.Player.State = state.State;
            
            sender.Player.IsLookingLeft = state.IsLookingLeft;
        }
    }
}