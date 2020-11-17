using System;

namespace NoOpRunner.Core.PlayerStates
{
    class RunningState : PlayerState
    {
        public override Uri GetAnimationUri()
        {
            String direction = this.player.IsLookingLeft ? "LEFT" : "RIGHT";
            Logging.Instance.Write("STATE: RUNNING TO " + direction);

            return ResourcesUriHandler.GetRunningAnimationUri();
        }
    }
}
