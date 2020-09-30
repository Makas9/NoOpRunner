﻿using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes
{
    public class PowerUp : BaseShape
    {
        protected PowerUps PowerUpType;

        public PowerUp(int centerPosX, int centerPosY, PowerUps powerup) : base(centerPosX, centerPosY)
        {
            this.PowerUpType = powerup;
        }

        public BaseShape SpawnPowerUp()
        {
            Color spawn = GetPowerUpColor(PowerUpType);

            return MapShapeX(0, 2, 1, spawn);
        }

        private Color GetPowerUpColor(PowerUps powerup)
        {
            switch (powerup)
            {
                case PowerUps.Speed_Boost: return Color.Red;
                case PowerUps.Invisibility: return Color.Blue;
                case PowerUps.Invulnerability: return Color.Green;
                case PowerUps.Double_Jump: return Color.Yellow;
                default: return Color.Black;
            }
        }

        public override bool CanOverlap(BaseShape other)
        {
            return false; // Not Implemented Yet
        }

        public override void OnCollision(BaseShape other)
        {
            // Not Implemented Yet
        }
    }
}
