using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NoOpRunner.Core.Tests")]
namespace NoOpRunner.Core.PlayerStates
{
    public abstract class PlayerState
    {
        protected PlayerOneStateMachine player;

        public void SetPlayer(PlayerOneStateMachine player)
        {
            this.player = player;
        }

        public abstract Uri GetAnimationUri();
    }
}
