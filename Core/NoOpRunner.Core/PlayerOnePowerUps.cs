using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core
{
    /// <summary>
    /// Dumb, fix, too big PR
    /// </summary>
    public class PlayerOnePowerUps
    {
        [JsonProperty]
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

        [JsonProperty]
        private List<ActivePowerUp> activePowerUps;

        public IList<PowerUps> ActivePowerUps => activePowerUps.Select(x=> x.PowerUp).ToList();

        [JsonProperty]
        private IList<PowerUps> availablePowerUps;

        public PlayerOnePowerUps()
        {
            this.exhaustedPowerUps = new List<PowerUps?>();
            this.activePowerUps = new List<ActivePowerUp>();
            this.availablePowerUps = new List<PowerUps>();
        }

        public void OnLoopFired()
        {
            activePowerUps.ForEach(x=> x.OnLoopFired());

            foreach (var powerUp in activePowerUps.Where((x) => x.FramesLeft <= 0).ToList())
            {
                activePowerUps.Remove(powerUp);
                ExhaustedPowerUp = powerUp.PowerUp;
            }
        }

        public void TakePowerUp(PowerUps powerUp)
        {
            availablePowerUps.Add(powerUp);
        }

        public bool UsePowerUp(PowerUps powerUp)
        {
            if (!availablePowerUps.Contains(powerUp))

                return false;

            availablePowerUps.Remove(powerUp);

            activePowerUps.Add(powerUp == PowerUps.Double_Jump
                ? new ActivePowerUp(powerUp, 1)
                : new ActivePowerUp(powerUp, 1000 * 5 / GameSettings.TimeBetweenFramesMs));

            return true;
        }

        public bool IsAvailable(PowerUps powerUps)
        {
            return availablePowerUps.Contains(powerUps);
        }
    }
}