using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.PlayerStates;
using NoOpRunner.Core.Rendering;
using NoOpRunner.Core.Shapes.GenerationStrategies;

namespace NoOpRunner.Core.Shapes
{
    public class Player : MovingShape, IObserver, IVisualElement
    {
        private const int MaxHealth = 3;
        private int currentHealth = 0;

        private const int HitBoxWidth = 1;
        private const int HitBoxHeight = 2;

        public Player(int x, int y) : base(new FillGenerationStrategy(), x, y, x + HitBoxWidth, y + HitBoxHeight)
        {
            StateMachine = new PlayerOneStateMachine();
            currentHealth = MaxHealth;
            
            TakenPowerUps = new List<PowerUps>();
        }

        private PowerUps? usedPowerUp;
        public PowerUps? UsedPowerUp
        {
            get
            {
                var temp = usedPowerUp;

                usedPowerUp = null;

                return temp;
            }
            private set => usedPowerUp = value;
        }

        private const int MovementIncrement = 1;
        private PlayerOneStateMachine StateMachine { get; set; } //dumb implementation of State machine pattern

        private const decimal JumpAcceleration = 0.1m;
        private const decimal JumpAccelerationPool = 0.5m;
        private const int JumpVerticalSpeed = 1;

        private bool IsJumping = false;
        private bool CanJump = true;
        
        private IList<PowerUps> TakenPowerUps { get; set; }

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
                    StateMachine.Land();
                    CenterPosY -= MovementIncrement;
                    CanJump = false;
                }
                else
                {
                    CanJump = true;

                    if (HorizontalSpeed == 0)
                    {
                        StateMachine.Idle();
                    }
                }
            }

            if (Math.Abs(HorizontalSpeed) > 0 && CanJump)
            {
                StateMachine.Run();
            }
        }

        /// <summary>
        /// Unused, will need for collision, rename who needs it
        /// </summary>
        /// <returns></returns>
        public override List<WindowPixel> Render()
        {
            var pixels = base.Render();

            pixels.ForEach(x => x.IsShape = false);

            return pixels;
        }

        private WindowPixel GetAnimationPixel(out int hitBoxY, out int hitBoxX)
        {
            var animationShapeBlock = ShapeBlocks[0];

            var absX = CenterPosX + animationShapeBlock.OffsetX;
            var absY = CenterPosY + animationShapeBlock.OffsetY;

            hitBoxX = HitBoxWidth;
            hitBoxY = HitBoxHeight;

            return new WindowPixel(absX, absY, isShape: true);
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
                    }

                    return;
                case KeyPress.Left:
                    if (!IsShapeHit(gameScreen, CenterPosX - HorizontalSpeed, CenterPosY))
                    {
                        HorizontalSpeed = Math.Max(HorizontalSpeed - 1, -HorizontalSpeedLimit);
                        if (HorizontalSpeed < 0)
                            StateMachine.TurnLeft();
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
                    }else if (TakenPowerUps.Contains(PowerUps.Double_Jump) && !IsShapeHit(gameScreen, CenterPosX, CenterPosY + MovementIncrement))
                    {
                        StateMachine.Jump();
                        IsJumping = true;
                        CanJump = false;

                        VerticalAcceleration = JumpAcceleration;
                        VerticalAccelerationPool = JumpAccelerationPool;
                        VerticalSpeed = JumpVerticalSpeed;

                        UsedPowerUp = PowerUps.Double_Jump;

                        TakenPowerUps.Remove(PowerUps.Double_Jump);
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
        
        public bool IsLookingLeft
        {
            get => StateMachine.IsLookingLeft;
            private set => StateMachine.IsLookingLeft = value;
        }

        public PlayerOneState State
        {
            get => StateMachine.State;
            private set => StateMachine.State = value;
        }

        public void Update(MessageDto message)
        {
            if (message.MessageType != MessageType.PlayerUpdate) 
                return;
            
            Console.WriteLine("Observer: Player, say Hello World");
            
            var messageDto = message.Payload as PlayerStateDto;

            State = messageDto.State;
            IsLookingLeft = messageDto.IsLookingLeft;

            CenterPosX = messageDto.CenterPosX;
            CenterPosY = messageDto.CenterPosY;
            
        }

        public void TakePowerUp(PowerUps powerUp)
        {
            if (!TakenPowerUps.Contains(powerUp))
            {
                TakenPowerUps.Add(powerUp);
            }
        }
        public void Display(Canvas canvas)
        {
            var animationPixel = GetAnimationPixel(out int hitBoxY, out int hitBoxX);
            
            var gifWidth = canvas.ActualWidth / GameSettings.HorizontalCellCount;
            var gifHeight = canvas.ActualHeight / GameSettings.VerticalCellCount;
            
            var playerAnimation = canvas.Children.OfType<GifImage>().FirstOrDefault(x=> x.VisualType == VisualElementType.Player);

            if (playerAnimation == null)
            {
                playerAnimation = new GifImage()
                {
                    Width = gifWidth,
                    Height = gifHeight,
                    VisualType = VisualElementType.Player,
                    GifSource = StateMachine.GetStatusUri(),
                    Stretch = Stretch.Fill
                };
                
                Canvas.SetLeft(playerAnimation, gifWidth * animationPixel.X * hitBoxX);
                Canvas.SetBottom(playerAnimation, gifHeight * animationPixel.Y * hitBoxY);

                canvas.Children.Add(playerAnimation);
                
                playerAnimation.StartAnimation();
            }
            else
            {
                if (StateMachine.StateHasChanged)
                {
                    playerAnimation.SetValue(GifImage.GifSourceProperty,
                        StateMachine.GetStatusUri());
                }
            }

            foreach (UIElement canvasChild in canvas.Children)
            {
                //Move
                RenderingHelper.AnimateUiElementMove(canvasChild, gifWidth * animationPixel.X,
                    gifHeight * animationPixel.Y);
                
                //Resize
                canvasChild.SetValue(FrameworkElement.WidthProperty, gifWidth * hitBoxX); // sketch for all hit box
                canvasChild.SetValue(FrameworkElement.HeightProperty, gifHeight * hitBoxY); // sketch for all hit box
                
                //Mirror
                if (StateMachine.IsTurning)
                {
                    canvasChild.SetValue(UIElement.RenderTransformProperty,
                        IsLookingLeft
                            ? new ScaleTransform() {ScaleX = -1, CenterX = gifWidth / 2}
                            : new ScaleTransform() {ScaleX = 1, CenterX = gifWidth / 2});
                }
            }
        }
    }
}
