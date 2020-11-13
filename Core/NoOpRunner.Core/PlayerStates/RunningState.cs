using NoOpRunner.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoOpRunner.Core.PlayerStates
{
    class RunningState : PlayerState
    {
        public override Uri doAnimation()
        {
            return ResourcesUriHandler.GetRunningAnimationUri();
        }

        public override void doAction()
        {
            sendMessage();
        }

        public override PlayerOneState getEnum()
        {
            return PlayerOneState.Running;
        }

        public void sendMessage()
        {
            String direction = this.player.IsLookingLeft ? "LEFT" : "RIGHT";
            Logging.Instance.Write("STATE: RUNNING TO " + direction);
        }
    }
}
