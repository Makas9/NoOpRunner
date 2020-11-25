using System;

namespace NoOpRunner.Core.PlayerStates
{
    public class JumpingState : PlayerState
    {
        public override Uri GetAnimationUri()
        {
            Logging.Instance.Write("STATE: JUMPING", LoggingLevel.State);

            return ResourcesUriHandler.GetJumpingAnimationUri();
        }
    }
}
