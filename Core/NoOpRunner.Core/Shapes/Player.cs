using System;
using System.Collections.Generic;
using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.PlayerStates;

namespace NoOpRunner.Core.Shapes
{
    public class Player : MovingShape
    {
        private const int MaxHealth = 3;
        private int currentHealth = 0;
        
        private const int HitBoxWidth = 1;
        private const int HitBoxHeight = 2;

        public Player(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            StateMachine = new PlayerOneStateMachine();

            currentHealth = MaxHealth;
            MapShapeY(0, 0, HitBoxHeight, Color.Blue);
            // Need for width??

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
        }

        public override List<WindowPixel> Render()
        {
            var pixels = base.Render();

            pixels.ForEach(x => x.IsShape = false);

            return pixels;
        }

        public WindowPixel GetAnimationPixel(out int hitBoxY, out int hitBoxX)
        {
            var animationShapeBlock = ShapeBlocks[0];
            
            var absX = CenterPosX + animationShapeBlock.OffsetX;
            var absY = CenterPosY + animationShapeBlock.OffsetY;

            hitBoxX = HitBoxWidth;
            hitBoxY = HitBoxHeight;
            
            return new WindowPixel(absX, absY, animationShapeBlock.Color, true); 
        }
        public void HandleKeyPress(KeyPress key, WindowPixel[,] gameScreen)
        {
            switch (key)
            {
                case KeyPress.Right:
                    if (!IsShapeHit(gameScreen, CenterPosX + HorizontalSpeed, CenterPosY))
                    {
                        HorizontalSpeed = Math.Min(HorizontalSpeed + 1, HorizontalSpeedLimit);
                        if (HorizontalSpeed > 0)
                            StateMachine.TurnRight();
                        StateMachine.Run();
                    }

                    return;
                case KeyPress.Left:
                    if (!IsShapeHit(gameScreen, CenterPosX - HorizontalSpeed, CenterPosY))
                    {
                        HorizontalSpeed = Math.Max(HorizontalSpeed - 1, -HorizontalSpeedLimit);
                        if (HorizontalSpeed < 0)
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

        public void ModifyHealth(int healthPoints)
        {
            currentHealth += healthPoints;

            if (currentHealth > MaxHealth)
            {
                currentHealth = MaxHealth;
            }
            else if (currentHealth < 1)
            {
                currentHealth = 0;
                // Stop the game
            }
        }

        public override bool CanOverlap(BaseShape other)
        {
            throw new NotImplementedException();
        }

        public override void OnCollision(BaseShape other)
        {
            throw new NotImplementedException();
        }

        public void HandleKeyRelease(KeyPress key, WindowPixel[,] gameScreen)
        {
            switch (key)
            {
                case KeyPress.Right:
                case KeyPress.Left:
                    HorizontalSpeed = 0;
                    return;
            }
        }

        public bool StateHasChanged => StateMachine.StateHasChanged;


        public Uri GetStateAnimationUri => StateMachine.GetStatusUri();


        public bool IsPlayerTurning => StateMachine.IsTurning;


        public bool IsLookingLeft => StateMachine.IsTurnedLeft;
    }
}
