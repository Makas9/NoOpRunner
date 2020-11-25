using System;

namespace NoOpRunner.Core.PlayerStates
{
    public class RunningState : PlayerState
    {
        public override Uri GetAnimationUri()
        {
            String direction = this.player.IsLookingLeft ? "LEFT" : "RIGHT";
            Logging.Instance.Write("STATE: RUNNING TO " + direction, LoggingLevel.State);

            return ResourcesUriHandler.GetRunningAnimationUri();
        }
    }
}
