using System.Windows;
using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Core.PlayerStates
{
    public class RunAnimationState : IPlayerAnimationState
    {
        private readonly Player _player;

        public RunAnimationState(Player player)
        {
            _player = player;
        }

        public UIElement Jump()
        {
            throw new System.NotImplementedException();
        }

        public UIElement Run()
        {
            throw new System.NotImplementedException();
        }

        public UIElement Idle()
        {
            throw new System.NotImplementedException();
        }

        public UIElement Land()
        {
            throw new System.NotImplementedException();
        }
    }
}