using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core
{
    public class PlayerTwo
    {
        private PlayerTwoPowerUps PlayerTwoPowerUps { get; set; }

        private int maxHealth = 3;
        
        public int CurrentHealth { get; private set; }

        public PlayerTwo()
        {
            PlayerTwoPowerUps = new PlayerTwoPowerUps();

            CurrentHealth = maxHealth;
        }

        public bool Hit()
        {
            CurrentHealth--;
            
            return CurrentHealth > 0;
        }

        public void Heal()
        {
            CurrentHealth++;
            
            if (CurrentHealth >= maxHealth)
            
                CurrentHealth = maxHealth;
            
        }

        /// <summary>
        /// for now just saw
        /// I'm lazy and I'm know it
        /// </summary>
        /// <param name="powerUps"></param>
        /// <returns></returns>
        public bool SetPowerUp(PowerUps powerUps)
        {
            return PlayerTwoPowerUps.SetPowerUp(PowerUps.Saw);//TODO: other types
        }
        /// <summary>
        /// P1 power up convert to P2
        /// P2 see P1 sprites for now and I'm to lazy for this
        /// so do you
        /// </summary>
        /// <param name="powerUps"></param>
        public void TakePowerUp(PowerUps powerUps)
        {
            PlayerTwoPowerUps.TakePowerUp(PowerUps.Saw);//For testing purpose 

            #region For final implementation LMAO
            // switch (powerUps)
            // {
            //     case PowerUps.Speed_Boost:
            //         PlayerTwoPowerUps.TakePowerUp(PowerUps.Proximity_Mine);
            //         break;
            //     case PowerUps.Invisibility:
            //         PlayerTwoPowerUps.TakePowerUp(PowerUps.Rocket);
            //         break;
            //     case PowerUps.Invulnerability:
            //         PlayerTwoPowerUps.TakePowerUp(PowerUps.Saw);
            //         break;
            //     case PowerUps.Double_Jump:
            //         PlayerTwoPowerUps.TakePowerUp(PowerUps.Knockback_Bomb);
            //         break;
            //     default:
            //         throw new ArgumentOutOfRangeException(nameof(powerUps), powerUps, null);
            // }
            #endregion
            
        }

        public PowerUps? UseActivePowerUp => PlayerTwoPowerUps.UsePowerUp();
        
    }
}