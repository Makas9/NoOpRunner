using System;

namespace NoOpRunner.Core.PlayerStates
{
    public abstract class PlayerState
    {
        protected PlayerOneStateMachine player;

        public int HealthPoints { get; set; } = 3;

        public void SetPlayer(PlayerOneStateMachine player)
        {
            this.player = player;
        }

        public abstract Uri GetAnimationUri();
    }
}
