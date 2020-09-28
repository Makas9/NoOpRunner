using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;
using System;

namespace NoOpRunner.Core.Shapes
{
    public class PowerUp : BaseShape
    {
        public PowerUp(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            Color powerup = GetRandomPowerUp();

            MapShapeX(0, 2, 1, powerup);
        }

        private Color GetRandomPowerUp()
        {
            Array powerups = Enum.GetValues(typeof(PowerUps));
            int index = RandomNumber.GetInstance().Next(0, powerups.Length - 1);

            switch (powerups.GetValue(index))
            {
                case PowerUps.Speed_Boost: return Color.Red;
                case PowerUps.Invisibility: return Color.Blue;
                case PowerUps.Invulnerability: return Color.Green;
                case PowerUps.Double_Jump: return Color.Yellow;
                default: return Color.Black;
            }
        }
    }
}
