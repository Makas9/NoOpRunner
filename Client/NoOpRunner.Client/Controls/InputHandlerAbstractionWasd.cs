using NoOpRunner.Core;
using NoOpRunner.Core.Controls;
using System.Windows.Input;

namespace NoOpRunner.Client.Controls
{
    public class InputHandlerAbstractionWasd : IInputHandlerAbstraction
    {
        private InputHandlerImplementor implementor;

        public InputHandlerAbstractionWasd(InputHandlerImplementor implementor)
        {
            this.implementor = implementor;
        }

        public void HandleKeyEvent(KeyEventArgs e, WindowPixel[,] gameScreen)
        {
            switch (e.Key)
            {
                case Key.W:
                    implementor.HandleUp(gameScreen);
                    return;
                case Key.D:
                    implementor.HandleRight(gameScreen);
                    return;
                case Key.A:
                    implementor.HandleLeft(gameScreen);
                    return;
                case Key.S:
                    implementor.HandleDown(gameScreen);
                    return;
                case Key.D1:
                    implementor.HandlePower1(gameScreen);
                    return;
                default:
                    return;
            }
        }

        public void HandleKeyUpEvent(KeyEventArgs e, WindowPixel[,] gameScreen)
        {
            switch (e.Key)
            {
                case Key.D:
                    implementor.HandleRightRelease(gameScreen);
                    return;
                case Key.A:
                    implementor.HandleLeftRelease(gameScreen);
                    return;
                default:
                    return;
            }
        }
    }
}
