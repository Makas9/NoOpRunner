using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Controls
{
    public class InputHandlerImplementorPlayerOne : InputHandlerImplementor
    {
        private Player player;

        public InputHandlerImplementorPlayerOne(Player player)
        {
            Logging.Instance.Write("[InputHandlerImplementorPlayerOne]: initialized", LoggingLevel.Bridge);
            this.player = player;
        }

        public override void HandleLeft(WindowPixel[,] gameScreen)
        {
            player.MoveLeft(gameScreen);
        }

        public override void HandleRight(WindowPixel[,] gameScreen)
        {
            player.MoveRight(gameScreen);
        }

        public override void HandleUp(WindowPixel[,] gameScreen)
        {
            player.CanPassPlatforms = true;
            player.Jump(gameScreen);
        }

        public override void HandleDown(WindowPixel[,] gameScreen)
        {
            player.CanPassPlatforms = true;
            player.IsDroppingDown = true;
            player.DropDown(gameScreen);
        }

        public override void HandlePower1(WindowPixel[,] gameScreen)
        {
            player.UsePowerUp(PowerUps.Speed_Boost);
        }

        public override void HandlePower2(WindowPixel[,] gameScreen)
        {
            player.UsePowerUp(PowerUps.Invulnerability);
        }

        public override void HandleLeftRelease(WindowPixel[,] gameScreen)
        {
            player.Stop();
        }

        public override void HandleRightRelease(WindowPixel[,] gameScreen)
        {
            player.Stop();
        }

        public override void HandleUpRelease(WindowPixel[,] gameScreen)
        {
            player.CanPassPlatforms = false;
        }

        public override void HandleDownRelease(WindowPixel[,] gameScreen)
        {
            player.CanPassPlatforms = false;
            player.IsDroppingDown = false;
        }
    }
}
