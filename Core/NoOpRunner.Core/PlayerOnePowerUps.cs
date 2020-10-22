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
        private IList<PowerUps?> exhaustedPowerUps;

        public PowerUps? ExhaustedPowerUp
        {
            get
            {
                var temp = exhaustedPowerUps.FirstOrDefault();

                if (temp != null)
                {
                    exhaustedPowerUps.Remove(temp);
                }

                return temp;
            }
            private set
            {
                if (!exhaustedPowerUps.Contains(value))
                {
                    exhaustedPowerUps.Add(value);
                }
            }
        }

        private IDictionary<PowerUps, int> activePowerUps;

        public IList<PowerUps> ActivePowerUps => activePowerUps.Keys.ToList();

        private IList<PowerUps> availablePowerUps;

        public PlayerOnePowerUps()
        {
            this.exhaustedPowerUps = new List<PowerUps?>();
            this.activePowerUps = new Dictionary<PowerUps, int>();
            this.availablePowerUps = new List<PowerUps>();
        }

        public void OnLoopFired()
        {
            foreach (var powerUp in activePowerUps.Keys.ToList())
            {
                activePowerUps[powerUp]--;
            }

            foreach (var powerUp in activePowerUps.Where((x) => x.Value <= 0).Select(x => x.Key).ToList())
            {
                activePowerUps.Remove(powerUp);
                ExhaustedPowerUp = powerUp;
            }
        }

        public void TakePowerUp(PowerUps powerUp)
        {
            if (!availablePowerUps.Contains(powerUp))
            {
                availablePowerUps.Add(powerUp);
            }
        }

        public bool UsePowerUp(PowerUps powerUp)
        {
            if (!availablePowerUps.Contains(powerUp))

                return false;

            availablePowerUps.Remove(powerUp);

            if (powerUp == PowerUps.Double_Jump)
            {
                ExhaustedPowerUp = powerUp;
            }
            else
            {
                //two sec
                activePowerUps[powerUp] = 1000 * 5 / GameSettings.TimeBetweenFrames.Milliseconds;
            }

            return true;
        }
    }
}