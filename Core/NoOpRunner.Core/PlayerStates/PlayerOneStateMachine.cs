using System;

namespace NoOpRunner.Core.PlayerStates
{
    public class PlayerOneStateMachine
    {
        private PlayerState currentState = new IdleState();

        private PlayerState LastState { get; set; }

        public PlayerState State
        {
            get => currentState;
            set
            {
                SetState(value);
            }
        }

        public void SetState(PlayerState state)
        {
            this.LastState = this.currentState;
            this.currentState = state;
            this.currentState.SetPlayer(this);
        }

        private bool isLastDirectionLeft;

        public bool StateHasChanged => currentState != LastState;

        public bool _isLookingLeft;

        public bool IsLookingLeft
        {
            get => _isLookingLeft;
            set
            {
                isLastDirectionLeft = _isLookingLeft;
                _isLookingLeft = value;
            }
        }

        public Uri GetStateUri()
        {
            return this.currentState.GetAnimationUri();
        }

        public bool IsTurning => isLastDirectionLeft != IsLookingLeft;

        public void Jump()
        {
            SetState(new JumpingState());
        }

        public void Land()
        {
            SetState(new LandingState());
        }

        public void Run()
        {
            SetState(new RunningState());
        }

        public void Idle()
        {
            SetState(new IdleState());
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