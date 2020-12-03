using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Controls
{
    public class InputHandlerImplementorPlayerTwo : InputHandlerImplementor
    {
        private PlayerTwo playerTwo;
        
        public InputHandlerImplementorPlayerTwo(PlayerTwo playerTwo)
        {            
            this.playerTwo = playerTwo;

            Logging.Instance.Write("[InputHandlerImplementorPlayerTwo]: initialized", LoggingLevel.Bridge);
        }

        public override void HandlePower1(WindowPixel[,] gameScreen)
        {
            playerTwo.SetPowerUp(PowerUps.Saw);
            
            Logging.Instance.Write("Input Handler Bridge: P2 would trigger a powerup here", LoggingLevel.Bridge);
        }
    }
}
