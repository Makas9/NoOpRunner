using System;
using System.Collections.Generic;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.PlayerStates;

namespace NoOpRunner.Core.Shapes
{
    public class Player : MovingShape
    {
        private const int MaxHealth = 3;
        private int CurrentHealth = 0;

        public Player(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            StateMachine = new PlayerOneStateMachine();
            CurrentHealth = MaxHealth;
            MapShapeX(0, 0, 1, Color.Blue);
        }

        private const int MovementIncrement = 1;
        private PlayerOneStateMachine StateMachine { get; set; } //dumb implementation of State machine pattern

        private const decimal JumpAcceleration = 0.1m;
        private const decimal JumpAccelerationPool = 0.5m;
        private const int JumpVerticalSpeed = 1;

        private bool IsJumping = false;
        private bool CanJump = true;

        private decimal VerticalAccelerationPool;

        public override void OnLoopFired(WindowPixel[,] gameScreen)
        {
            base.OnLoopFired(gameScreen);

            if (HorizontalSpeed == 0 && CanJump)
            {
                StateMachine.Idle();
            }
            else if (Math.Abs(HorizontalSpeed) > 0)
            {
                StateMachine.Run();
            }

            if (IsJumping)
            {
                if (VerticalAccelerationPool >= JumpAcceleration)
                {
                    VerticalAcceleration += JumpAcceleration;
                    VerticalAccelerationPool -= JumpAcceleration;

                    if (VerticalAccelerationPool <= 0)
                    {
                        IsJumping = false;
                        VerticalSpeed = 0;
                    }
                }
            }
            else
            {
                if (!IsShapeHit(gameScreen, CenterPosX, CenterPosY - MovementIncrement))
                {
                    StateMachine.Land();
                    CenterPosY -= MovementIncrement;
                    CanJump = false;
                }
                else
                {
                    CanJump = true;
                }
            }

            HorizontalSpeed = 0;
        }

        public override List<WindowPixel> Render()
        {
            var pixels = base.Render();

            pixels.ForEach(x => x.IsShape = false);

            return pixels;
        }

        public void HandleKeyPress(KeyPress key, WindowPixel[,] gameScreen)
        {
            switch (key)
            {
                case KeyPress.Right:
                    if (!IsShapeHit(gameScreen, CenterPosX + MovementIncrement, CenterPosY))
                    {
                        HorizontalSpeed = MovementIncrement;
                        StateMachine.TurnRight();
                        StateMachine.Run();
                    }

                    return;
                case KeyPress.Left:
                    if (!IsShapeHit(gameScreen, CenterPosX - MovementIncrement, CenterPosY))
                    {
                        HorizontalSpeed = -MovementIncrement;
                        StateMachine.TurnLeft();
                        StateMachine.Run();
                    }

                    return;
                case KeyPress.Up:
                    if (!IsJumping && CanJump && !IsShapeHit(gameScreen, CenterPosX, CenterPosY + MovementIncrement))
                    {
                        StateMachine.Jump();
                        IsJumping = true;
                        CanJump = false;

                        VerticalAcceleration = JumpAcceleration;
                        VerticalAccelerationPool = JumpAccelerationPool;
                        VerticalSpeed = JumpVerticalSpeed;
                    }

                    return;
                case KeyPress.Down:
                    if (!IsShapeHit(gameScreen, CenterPosX, CenterPosY - MovementIncrement))
                    {
                        StateMachine.Land();
                        VerticalSpeed = -MovementIncrement;
                    }

                    return;
                case KeyPress.Space:
                    // Use power-up

                    return;
            }
        }

        public void DoDamage(int damage)
        {
            CurrentHealth -= damage;

            if(CurrentHealth < 1)
            {
                CurrentHealth = 0;
                // Stop the game
            }
        }

        public void DoHeal(int heal)
        {
            CurrentHealth += heal;

            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }

        public bool StateHasChanged => StateMachine.StateHasChanged;


        public Uri GetStateAnimationUri => StateMachine.GetStatusUri();


        public bool IsPlayerTurning => StateMachine.IsTurning;


        public bool IsLookingLeft => StateMachine.IsTurnedLeft;
    }
}