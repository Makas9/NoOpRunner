using System;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.PlayerStates
{
    public class PlayerOneStateMachine
    {
        private PlayerOneState _currentState = PlayerOneState.IdleState;
        private PlayerOneState _lastState;

        private bool _leftDirection;
        private bool _lastDirection;

        public bool CompareStates => _currentState == _lastState;
        
        public bool IsTurnedLeft => _leftDirection;

        public Uri GetStatusUri()
        {
            switch (_currentState)
            {
                case PlayerOneState.IdleState:
                    return SpritesUriHandler.GetIdleAnimationUri();
                case PlayerOneState.JumpingState:
                    return SpritesUriHandler.GetJumpingAnimationUri();
                case PlayerOneState.LandingState:
                    return SpritesUriHandler.GetLandingAnimationUri();
                case PlayerOneState.RunningState:
                    return SpritesUriHandler.GetRunningAnimationUri();
                default:
                    return null;
            }
        }


        public bool IsAnimatedState =>
            _currentState == PlayerOneState.IdleState || _currentState == PlayerOneState.RunningState;

        public bool IsTurning => _lastDirection != _leftDirection;

        public void Jump()
        {
            _lastState = _currentState;
            _currentState = PlayerOneState.JumpingState;
        }

        public void Land()
        {
            _lastState = _currentState;
            _currentState = PlayerOneState.LandingState;
        }

        public void Run()
        {
            _lastState = _currentState;
            _currentState = PlayerOneState.RunningState;
        }

        public void Idl()
        {
            _lastState = _currentState;
            _currentState = PlayerOneState.IdleState;
        }

        public void TurnRight()
        {
            _lastDirection = _leftDirection;
            _leftDirection = false;
        }

        public void TurnLeft()
        {
            _lastDirection = _leftDirection;
            _leftDirection = true;
        }

        public PlayerOneState GetState()
        {
            return _currentState;
        }

        
        
    }
}