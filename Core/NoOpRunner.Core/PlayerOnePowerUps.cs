using System.Collections.Generic;
using System.Linq;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core
{
    /// <summary>
    /// Dumb, fix, too big PR
    /// </summary>
    public class PlayerOnePowerUps
    {
        private IList<PowerUps?> usedPowerUp;
        
        public PowerUps? UsedPowerUp
        {
            get
            {
                var temp = usedPowerUp.FirstOrDefault();

                if (temp!= null)
                {
                    usedPowerUp.Remove(temp);    
                }

                return temp;
            }
            private set
            {
                if (!usedPowerUp.Contains(value))
                {
                    usedPowerUp.Add(value);                    
                }
            } 
        }

        private IDictionary<PowerUps, int> usingPowerUps;

        public IList<PowerUps> UsingPowerUps => usingPowerUps.Keys.ToList();

        private IList<PowerUps> takenPowerUps;

        public PlayerOnePowerUps()
        {
            this.usedPowerUp = new List<PowerUps?>();
            this.usingPowerUps = new Dictionary<PowerUps, int>();
            this.takenPowerUps = new List<PowerUps>();
        }

        public void OnLoopFired()
        {
            foreach (var powerUp in usingPowerUps.Keys.ToList())
            {
                usingPowerUps[powerUp]--;
            }

            foreach (var powerUp in usingPowerUps.Where((x) => x.Value <= 0).Select(x => x.Key).ToList())
            {

                usingPowerUps.Remove(powerUp);
                UsedPowerUp = powerUp;
            }
        }

        public void TakePowerUp(PowerUps powerUp)
        {
            if (!takenPowerUps.Contains(powerUp))
            {
                takenPowerUps.Add(powerUp);
            }
        }

        public bool UsePowerUp(PowerUps powerUp)
        {
            if (!takenPowerUps.Contains(powerUp)) 
                
                return false;
            
            takenPowerUps.Remove(powerUp);
                
            if (powerUp ==PowerUps.Double_Jump)
            {
                UsedPowerUp = powerUp;    
            }
            else
            {
                //two sec
                usingPowerUps[powerUp] = 1000*5/GameSettings.Fps.Milliseconds;
            }

            return true;

        }
    }
}