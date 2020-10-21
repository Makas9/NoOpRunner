using NoOpRunner.Core;
using NoOpRunner.Core.Controls;
using System.Windows.Input;

namespace NoOpRunner.Client.Controls
{
    class InputHandlerAbstractionArrows : IInputHandlerAbstraction
    {
        private InputHandlerImplementor implementor;

        public InputHandlerAbstractionArrows(InputHandlerImplementor implementor)
        {
            this.implementor = implementor;
        }

        public void HandleKeyDownEvent(KeyEventArgs e, WindowPixel[,] gameScreen)
        {
            switch (e.Key)
            {
                case Key.Up:
                    implementor.HandleUp(gameScreen);
                    return;
                case Key.Right:
                    implementor.HandleRight(gameScreen);
                    return;
                case Key.Left:
                    implementor.HandleLeft(gameScreen);
                    return;
                case Key.Down:
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
                case Key.Right:
                    implementor.HandleRightRelease(gameScreen);
                    return;
                case Key.Left:
                    implementor.HandleLeftRelease(gameScreen);
                    return;
                default:
                    return;
            }
        }
    }
}
