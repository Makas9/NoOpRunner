using System;

namespace NoOpRunner.Core.PlayerStates
{
    class LandingState : PlayerState
    {
        public override Uri GetAnimationUri()
        {
            Logging.Instance.Write("STATE: LANDING");

            return ResourcesUriHandler.GetLandingAnimationUri();
        }
    }
}
