using NoOpRunner.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoOpRunner.Core.PlayerStates
{
    public abstract class PlayerState
    {
        protected PlayerOneStateMachine player;

        public void SetPlayer(PlayerOneStateMachine player)
        {
            this.player = player;
        }

        public abstract Uri doAnimation();
        public abstract void doAction();

        public abstract PlayerOneState getEnum();
    }
}
