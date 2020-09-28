using System;
using System.Collections.Generic;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.PlayerStates;

namespace NoOpRunner.Core.Shapes
{
    public class Player : MovingShape
    {
        public Player(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            SpritesUriHandler.LoadSprites();
            StateMachine = new PlayerOneStateMachine();
            // MapShapeX(0, 1, 1, Color.Blue); // Top
            MapShapeX(0, 0, 1, Color.Blue); // Bottom
        }

        private const int MovementIncrement = 1;
        public PlayerOneStateMachine StateMachine { get; }//shit

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
                StateMachine.Idl();
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

        private bool IsShapeHit(WindowPixel[,] gameScreen, int x, int y)
        {
            return x < 0 || y < 0 || x > gameScreen.GetUpperBound(0) ||
                   y > gameScreen.GetUpperBound(1) || (gameScreen[x, y] != default && gameScreen[x, y].IsShape);
        }
    }
}
