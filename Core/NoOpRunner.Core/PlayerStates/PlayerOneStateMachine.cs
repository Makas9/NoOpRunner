using System;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.PlayerStates
{
    public class PlayerOneStateMachine
    {
        private PlayerOneState _currentState = PlayerOneState.Idle;
        private PlayerOneState _lastState { get; set; }

        private bool _isLastDirectionLeft;

        public bool StateHasChanged => _currentState != _lastState;

        public bool IsTurnedLeft { get; private set; }

        public Uri GetStatusUri()
        {
            switch (_currentState)
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

        public bool IsTurning => _isLastDirectionLeft != IsTurnedLeft;

        public void Jump()
        {
            _lastState = _currentState;
            _currentState = PlayerOneState.Jumping;
        }

        public void Land()
        {
            _lastState = _currentState;
            _currentState = PlayerOneState.Landing;
        }

        public void Run()
        {
            _lastState = _currentState;
            _currentState = PlayerOneState.Running;
        }

        public void Idle()
        {
            _lastState = _currentState;
            _currentState = PlayerOneState.Idle;
        }

        public void TurnRight()
        {
            _isLastDirectionLeft = IsTurnedLeft;
            IsTurnedLeft = false;
        }

        public void TurnLeft()
        {
            _isLastDirectionLeft = IsTurnedLeft;
            IsTurnedLeft = true;
        }
    }
}