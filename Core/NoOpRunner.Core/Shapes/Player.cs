using System.Collections.Generic;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes
{
    public class Player : MovingShape
    {
        public Player(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeX(0, 1, 1, Color.Blue); // Top
            MapShapeX(0, 0, 1, Color.Blue); // Bottom
        }

        private const int MovementIncrement = 1;

        private const decimal JumpAcceleration = 0.1m;
        private const decimal JumpAccelerationPool = 0.5m;
        private const int JumpVerticalSpeed = 1;

        private bool IsJumping = false;
        private bool CanJump = true;

        private decimal VerticalAccelerationPool;

        public override void OnLoopFired(WindowPixel[,] gameScreen)
        {
            base.OnLoopFired(gameScreen);

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
                    }

                    return;
                case KeyPress.Left:
                    if (!IsShapeHit(gameScreen, CenterPosX - MovementIncrement, CenterPosY))
                    {
                        HorizontalSpeed = -MovementIncrement;
                    }

                    return;
                case KeyPress.Up:
                    if (!IsJumping && CanJump && !IsShapeHit(gameScreen, CenterPosX, CenterPosY + MovementIncrement))
                    {
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
                        VerticalSpeed = -MovementIncrement;
                    }

                    return;
                case KeyPress.Space:
                    // Use power-up

                    return;
            }
        }
    }
}
