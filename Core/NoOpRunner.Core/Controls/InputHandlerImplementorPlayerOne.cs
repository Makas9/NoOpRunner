using NoOpRunner.Core.Shapes;
using System;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Controls
{
    public class InputHandlerImplementorPlayerOne : InputHandlerImplementor
    {
        private Player player;

        public InputHandlerImplementorPlayerOne(Player player)
        {
            Logging.Instance.Write("[InputHandlerImplementorPlayerOne]: initialized", LoggingLevel.Pattern);
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
            player.Jump(gameScreen);
            player.CanPassPlatforms = true;
        }

        public override void HandleDown(WindowPixel[,] gameScreen)
        {
            player.DropDown(gameScreen);
            player.CanPassPlatforms = true;
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
        }
    }
}
