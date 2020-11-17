using System;

namespace NoOpRunner.Core.PlayerStates
{
    public class PlayerOneStateMachine
    {
        private PlayerState CurrentState = new IdleState();

        private PlayerState LastState { get; set; }

        public PlayerState State
        {
            get => CurrentState;
            set
            {
                setState(value);
            }
        }

        public void setState(PlayerState state)
        {
            this.LastState = this.CurrentState;
            this.CurrentState = state;
            this.CurrentState.SetPlayer(this);
        }

        private bool isLastDirectionLeft;

        public bool StateHasChanged => CurrentState != LastState;

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
            return this.CurrentState.GetAnimationUri();
        }

        public bool IsTurning => isLastDirectionLeft != IsLookingLeft;

        public void Jump()
        {
            setState(new JumpingState());
        }

        public void Land()
        {
            setState(new LandingState());
        }

        public void Run()
        {
            setState(new RunningState());
        }

        public void Idle()
        {
            setState(new IdleState());
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