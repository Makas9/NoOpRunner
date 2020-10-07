using System;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.PlayerStates
{
    public class PlayerOneStateMachine
    {
        private PlayerOneState currentState = PlayerOneState.Idle;
        
        public PlayerOneState State
        {
            get => currentState;
            set
            {
                LastState = currentState;
                currentState = value;
            }
        }
        
        private PlayerOneState LastState { get; set; }

        private bool isLastDirectionLeft;

        public bool StateHasChanged => currentState != LastState;

        public bool IsTurnedLeft { get; private set; }

        public bool IsLookingLeft
        {
            set
            {
                isLastDirectionLeft = IsTurnedLeft;
                IsTurnedLeft = value;
            }
        }

        public Uri GetStatusUri()
        {
            switch (currentState)
            {
                case PlayerOneState.Idle:
                    return SpritesUriHandler.GetIdleAnimationUri();
                case PlayerOneState.Jumping:
                    return SpritesUriHandler.GetJumpingAnimationUri();
                case PlayerOneState.Landing:
                    return SpritesUriHandler.GetLandingAnimationUri();
                case PlayerOneState.Running:
                    return SpritesUriHandler.GetRunningAnimationUri();
                default:
                    return null;
            }
        }

        public bool IsTurning => isLastDirectionLeft != IsTurnedLeft;

        public void Jump()
        {
            LastState = currentState;
            currentState = PlayerOneState.Jumping;
        }

        public void Land()
        {
            LastState = currentState;
            currentState = PlayerOneState.Landing;
        }

        public void Run()
        {
            LastState = currentState;
            currentState = PlayerOneState.Running;
        }

        public void Idle()
        {
            LastState = currentState;
            currentState = PlayerOneState.Idle;
        }

        public void TurnRight()
        {
            isLastDirectionLeft = IsTurnedLeft;
            IsTurnedLeft = false;
        }

        public void TurnLeft()
        {
            isLastDirectionLeft = IsTurnedLeft;
            IsTurnedLeft = true;
        }
    }
}