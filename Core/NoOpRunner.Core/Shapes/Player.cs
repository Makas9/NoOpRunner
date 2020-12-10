using Newtonsoft.Json;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Exceptions;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.PlayerStates;
using NoOpRunner.Core.Shapes.GenerationStrategies;
using NoOpRunner.Core.Shapes.StaticShapes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoOpRunner.Core.Shapes
{
    public class Player : MovingShape, IObserver, IMapPart
    {
        private const int MaxHealth = 3;
        private const int HitBoxWidth = 1;
        private const int HitBoxHeight = 2;

        public int HealthPoints { get; private set; } = 0;

        public Player(int x, int y) : base(new FillGenerationStrategy(), x, y, x + HitBoxWidth, y + HitBoxHeight)
        {
            StateMachine = new PlayerOneStateMachine();
            PlayerOnePowerUps = new PlayerOnePowerUps();

            HealthPoints = MaxHealth;
        }

        private const int MovementIncrement = 1;
        private PlayerOneStateMachine StateMachine { get; set; } //dumb implementation of State machine pattern

        public Uri StateUri => StateMachine.GetStateUri();

        [JsonProperty]
        public PlayerOnePowerUps PlayerOnePowerUps { get; private set; }

        private const decimal JumpAcceleration = 0.1m;
        private const decimal JumpAccelerationPool = 0.5m;
        private const int JumpVerticalSpeed = 1;

        private bool isJumping = false;
        private bool canJump = true;
        public bool CanPassPlatforms { get; set; } = false;
        public bool IsDroppingDown { get; set; } = false;
        private int invulnerableCycles = 0;

        private decimal VerticalAccelerationPool;

        public override void OnLoopFired(WindowPixel[,] gameScreen)
        {
            for (int i = 0; i <= ActivePowerUps.Count(x => x == PowerUps.Speed_Boost); i++)//Stack speed boost
            {
                base.OnLoopFired(gameScreen);

                if (isJumping)
                {
                    if (VerticalAccelerationPool >= JumpAcceleration)
                    {
                        VerticalAcceleration += JumpAcceleration;
                        VerticalAccelerationPool -= JumpAcceleration;

                        if (VerticalAccelerationPool <= 0)
                        {
                            isJumping = false;
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
                        canJump = false;
                    }
                    else
                    {
                        canJump = true;

                        if (HorizontalSpeed == 0)
                        {
                            StateMachine.Idle();
                        }
                    }
                }

                if (Math.Abs(HorizontalSpeed) > 0 && canJump)
                {
                    StateMachine.Run();
                }
                            
            }
        }

        /// <summary>
        /// Unused, will need for collision, rename who needs it
        /// </summary>
        /// <returns></returns>
        // public override WindowPixelCollection Render()
        // {
        //     var pixels = base.Render();
        //
        //     pixels.GetItems().ForEach(x => x.IsShape = false);
        //
        //         yield return flyweightPixel;
        //     }
        // }

        public WindowPixel GetAnimationPixel(out int hitBoxY, out int hitBoxX)
        {
            var animationShapeBlock = ShapeBlocks[0];

            var absX = CenterPosX + animationShapeBlock.OffsetX;
            var absY = CenterPosY + animationShapeBlock.OffsetY;

            hitBoxX = HitBoxWidth;
            hitBoxY = HitBoxHeight;

            return new WindowPixel(absX, absY, isShape: true);
        }

        public void MoveRight(WindowPixel[,] gameScreen)
        {
            if (!IsShapeHit(gameScreen, CenterPosX + (int)HorizontalSpeed, CenterPosY))
            {
                HorizontalSpeed = Math.Min(HorizontalSpeed + 1, HorizontalSpeedLimit);
                if (HorizontalSpeed > 0)
                    StateMachine.TurnRight();
            }
        }

        public void MoveLeft(WindowPixel[,] gameScreen)
        {
            if (!IsShapeHit(gameScreen, CenterPosX - (int)HorizontalSpeed, CenterPosY))
            {
                HorizontalSpeed = Math.Max(HorizontalSpeed - 1, -HorizontalSpeedLimit);
                if (HorizontalSpeed < 0)
                    StateMachine.TurnLeft();
            }
        }

        public void Jump(WindowPixel[,] gameScreen)
        {
            if (!isJumping && canJump && !IsShapeHit(gameScreen, CenterPosX, CenterPosY + MovementIncrement) ||
                (!IsShapeHit(gameScreen, CenterPosX, CenterPosY + MovementIncrement) &&//double jump
                 PlayerOnePowerUps.UsePowerUp(PowerUps.Double_Jump)))
            {
                StateMachine.Jump();
                isJumping = true;
                canJump = false;

                VerticalAcceleration = JumpAcceleration;
                VerticalAccelerationPool = JumpAccelerationPool;
                VerticalSpeed = JumpVerticalSpeed;
            }
        }

        public void UsePowerUp(PowerUps powerUp)
        {
            PlayerOnePowerUps.UsePowerUp(powerUp);
        }

        public void DropDown(WindowPixel[,] gameScreen)
        {
            if (!IsShapeHit(gameScreen, CenterPosX, CenterPosY - MovementIncrement))
            {
                StateMachine.Land();
                VerticalSpeed = -MovementIncrement;
            }
        }

        public void Stop()
        {
            HorizontalSpeed = 0;
        }

        public void ModifyHealth(int healthPoints)
        {
            if (healthPoints < 0)
            {
                if (invulnerableCycles > 0)
                    return;

                invulnerableCycles = 2;
            }
                

            HealthPoints += healthPoints;

            if (HealthPoints > MaxHealth)
            {
                HealthPoints = MaxHealth;
            }
            else if (HealthPoints < 1)
            {
                HealthPoints = 0;

                throw new GameOverException(false);
            }
        }

        public override bool CanOverlap(BaseShape other)
        {
            switch(other)
            {
                case PowerUp _:
                    return true;
                case PassablePlatform _:
                    return CanPassPlatforms && (IsDroppingDown || isJumping);
                case ImpassablePlatform _:
                    return false;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Handle collision with another shape.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>True if shape can be passed through, false otherwise.</returns>
        public override bool OnCollision(BaseShape other)
        {
            if (other is PowerUp powerUp)
            {
                switch (powerUp.PowerUpType)
                {
                    case PowerUps.Saw:
                        var modifier = 2;
                        HorizontalSpeedModifier = IsLookingLeft || powerUp.IsStatic ? modifier : -modifier;
                        ModifyHealth(-1);

                        break;
                }
            }

            return CanOverlap(other);
        }

        public bool IsLookingLeft
        {
            get => StateMachine.IsLookingLeft;
            private set => StateMachine.IsLookingLeft = value;
        }

        public PlayerState State
        {
            get => StateMachine.State;
            private set => StateMachine.State = value;
        }

        public bool IsTurning => StateMachine.IsTurning;

        public bool StateHasChanged => StateMachine.StateHasChanged;

        public void Update(MessageDto message)
        {
            if (message.MessageType != MessageType.PlayerUpdate)
                return;

            Logging.Instance.Write("Observer: player got update", LoggingLevel.Pattern);
            
            var messageDto = message.Payload as PlayerStateDto;

            State = messageDto.State;
            IsLookingLeft = messageDto.IsLookingLeft;

            HealthPoints = messageDto.HealthPoints;

            CenterPosX = messageDto.CenterPosX;
            CenterPosY = messageDto.CenterPosY;

            PlayerOnePowerUps = messageDto.PlayerOnePowerUps;
        }

        public void TakePowerUp(PowerUps powerUp)
        {
            PlayerOnePowerUps.TakePowerUp(powerUp);
        }

        public PowerUps? ExhaustedPowerUp => PlayerOnePowerUps.ExhaustedPowerUp;

        public IList<PowerUps> ActivePowerUps => PlayerOnePowerUps.ActivePowerUps;

        public void LoopPowerUps()
        {
            PlayerOnePowerUps.OnLoopFired();
        }

        public void OnMapMoveLoopFired(WindowPixel[,] windowPixels)
        {
            if (invulnerableCycles > 0)
                invulnerableCycles -= 1;

            if (CenterPosX - 1 == 0)
            {
                if (!IsShapeHit(windowPixels, CenterPosX - 1, CenterPosY))
                {
                    CenterPosX++;
                }
            }
            else if (CenterPosX - 1 < 0)
                CenterPosX++;
            else
                CenterPosX--;
        }

        public override IMapPart GetAtPos(int centerPosX, int centerPosY)
        {
            return null;
        }
    }
}