using System;

namespace NoOpRunner.Core.PlayerStates
{
    class LandingState : PlayerState
    {
        public override Uri GetAnimationUri()
        {
            Logging.Instance.Write("STATE: LANDING", LoggingLevel.State);

            return ResourcesUriHandler.GetLandingAnimationUri();
        }
    }
}
