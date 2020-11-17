using System;

namespace NoOpRunner.Core.PlayerStates
{
    class JumpingState : PlayerState
    {
        public override Uri GetAnimationUri()
        {
            Logging.Instance.Write("STATE: JUMPING");

            return ResourcesUriHandler.GetJumpingAnimationUri();
        }
    }
}
