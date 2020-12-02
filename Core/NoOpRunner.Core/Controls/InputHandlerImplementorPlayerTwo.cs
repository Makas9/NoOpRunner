using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Controls
{
    public class InputHandlerImplementorPlayerTwo : InputHandlerImplementor
    {
        private PlayerTwo playerTwo;
        
        public InputHandlerImplementorPlayerTwo(PlayerTwo playerTwo)
        {
            Logging.Instance.Write("[InputHandlerImplementorPlayerTwo]: initialized", LoggingLevel.Pattern);
            
            this.playerTwo = playerTwo;

        }

        public override void HandlePower1(WindowPixel[,] gameScreen)
        {
            Logging.Instance.Write("Input Handler Bridge: P2 would trigger a powerup here", LoggingLevel.Pattern);

            playerTwo.SetPowerUp(PowerUps.Saw);
        }
    }
}
