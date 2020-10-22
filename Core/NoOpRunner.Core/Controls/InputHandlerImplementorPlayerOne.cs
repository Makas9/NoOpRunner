using NoOpRunner.Core.Shapes;
using System;

namespace NoOpRunner.Core.Controls
{
    public class InputHandlerImplementorPlayerOne : InputHandlerImplementor
    {
        private Player player;

        public InputHandlerImplementorPlayerOne(Player player)
        {
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
        }

        public override void HandleDown(WindowPixel[,] gameScreen)
        {
            player.DropDown(gameScreen);
        }

        public override void HandlePower1(WindowPixel[,] gameScreen)
        {
            // Will be implemented soon with Lukas' PR
            throw new NotImplementedException("powerup activation not yet implemented");
        }

        public override void HandleLeftRelease(WindowPixel[,] gameScreen)
        {
            player.Stop();
        }

        public override void HandleRightRelease(WindowPixel[,] gameScreen)
        {
            player.Stop();
        }
    }
}
