using NoOpRunner.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoOpRunner.Core.PlayerStates
{
    class JumpingState : PlayerState
    {
        public override Uri doAnimation()
        {
            return ResourcesUriHandler.GetJumpingAnimationUri();
        }

        public override void doAction()
        {
            sendMessage();
        }

        public override PlayerOneState getEnum()
        {
            return PlayerOneState.Jumping;
        }

        public void sendMessage()
        {
            Logging.Instance.Write("STATE: JUMPING");
        }
    }
}
