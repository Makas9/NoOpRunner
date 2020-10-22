using NoOpRunner.Core;
using System.Windows.Input;

namespace NoOpRunner.Client.Controls
{
    public interface IInputHandlerAbstraction
    {
        void HandleKeyDownEvent(KeyEventArgs e, WindowPixel[,] gameScreen);
        void HandleKeyUpEvent(KeyEventArgs e, WindowPixel[,] gameScreen);
    }
}
