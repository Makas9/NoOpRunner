using System;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Core.PlayerStates
{
    public class PlayerOneStateMachine
    {
        private PlayerState CurrentState;

        private PlayerState LastState { get; set; }

        public PlayerOneState State
        {
            get => CurrentState.getEnum();
            set
            {
                setState(value);
            }
        }

        public void setState(PlayerOneState state)
        {
            this.LastState = this.CurrentState;
            switch (state)
            {
                case PlayerOneState.Idle:
                    this.CurrentState = new IdleState();
                    break;
                case PlayerOneState.Jumping:
                    this.CurrentState = new JumpingState();
                    break;
                case PlayerOneState.Landing:
                    this.CurrentState = new LandingState();
                    break;
                case PlayerOneState.Running:
                    this.CurrentState = new RunningState();
                    break;
            }
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
            return this.CurrentState.doAnimation();
        }

        public void DoAction()
        {
            this.CurrentState.doAction();
        }

        public bool IsTurning => isLastDirectionLeft != IsLookingLeft;

        public void Jump()
        {
            setState(PlayerOneState.Jumping);
        }

        public void Land()
        {
            setState(PlayerOneState.Landing);
        }

        public void Run()
        {
            setState(PlayerOneState.Running);
        }

        public void Idle()
        {
            setState(PlayerOneState.Idle);
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