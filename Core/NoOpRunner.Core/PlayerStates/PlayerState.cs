using System;

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
