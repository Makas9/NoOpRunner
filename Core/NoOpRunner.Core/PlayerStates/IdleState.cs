using System;

namespace NoOpRunner.Core.PlayerStates
{
    public class IdleState : PlayerState
    {
        public override Uri GetAnimationUri()
        {
            Logging.Instance.Write("STATE: IDLE", LoggingLevel.State);

            return ResourcesUriHandler.GetIdleAnimationUri();
        }
    }
}
