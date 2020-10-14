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

        private bool _isLookingLeft;

        public bool IsLookingLeft
        {
            get => _isLookingLeft;
            set
            {
                isLastDirectionLeft = _isLookingLeft;
                _isLookingLeft = value;
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

        public bool IsTurning => isLastDirectionLeft != IsLookingLeft;

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
            IsLookingLeft = false;
        }

        public void TurnLeft()
        {
            IsLookingLeft = true;
        }
    }
}