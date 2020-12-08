using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core
{
    public class ActivePowerUp
    {
        public PowerUps PowerUp { get; set; }
        
        public int FramesLeft { get; private set; }

        public ActivePowerUp(PowerUps powerUp, int framesLeft)
        {
            PowerUp = powerUp;
            FramesLeft = framesLeft;
        }

        public void OnLoopFired()
        {
            FramesLeft--;
        }
    }
}