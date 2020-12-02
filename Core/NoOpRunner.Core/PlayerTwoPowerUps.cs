using System.Collections.Generic;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core
{
    public class PlayerTwoPowerUps
    {
        private IList<PowerUps> AvailablePowerUps { get; set; }
        
        private PowerUps? ActivePowerUp { get; set; }

        public PlayerTwoPowerUps()
        {
            AvailablePowerUps = new List<PowerUps>();
        }

        public void TakePowerUp(PowerUps powerUps)
        {
            AvailablePowerUps.Add(powerUps);
        }

        public bool SetPowerUp(PowerUps powerUps)
        {
            if (ActivePowerUp == powerUps)
            
                return true;
            
            if (ActivePowerUp != null )
            
                AvailablePowerUps.Add((PowerUps)ActivePowerUp);

            if (!AvailablePowerUps.Contains(powerUps)) 
                
                return false;
            
            ActivePowerUp = powerUps;

            AvailablePowerUps.Remove(powerUps);

            return true;

        }
        
        public PowerUps? UsePowerUp()
        {
            return PowerUps.Saw; 
                
            if (ActivePowerUp == null) 
                
                return null;

            var temp = ActivePowerUp;
            
            ActivePowerUp = null;
            
            return temp;
        }
    }
}