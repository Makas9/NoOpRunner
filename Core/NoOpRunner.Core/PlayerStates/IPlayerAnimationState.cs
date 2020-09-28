using System.Windows;

namespace NoOpRunner.Core.PlayerStates
{
    public interface IPlayerAnimationState
    {
        
        UIElement Jump();

        UIElement Run();

        UIElement Idle();

        UIElement Land();
    }
}