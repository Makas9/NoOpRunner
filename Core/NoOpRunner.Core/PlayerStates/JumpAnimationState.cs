using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Core.PlayerStates
{
    public class JumpAnimationState : IPlayerAnimationState
    {
        private readonly Player _player;
        private UIElement animation;

        public JumpAnimationState(Player player)
        {
            _player = player;
        }

        public void SetUiElement(UIElement element)
        {
            animation = element;
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