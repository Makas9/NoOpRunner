using System;
using System.Windows;
using System.Windows.Controls;
using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Core.PlayerStates
{
    public class IdleAnimationState : IPlayerAnimationState
    {
        private Player _player;
        private UIElement _idleAnimation;

        public IdleAnimationState(Player player)
        {
            _player = player;
            _idleAnimation = new MediaElement(){Source = SpritesUriHandler.GetIdleAnimationUri()};
        }

        public UIElement Jump()
        {
            _player.SetAnimationState(_player.JumpState);
            return _player.GetAnimationState().Jump();
        }

        public UIElement Run()
        {
            _player.SetAnimationState(_player.RunState);
            return _player.GetAnimationState().Run();
        }

        public UIElement Idle()
        {
            _player.SetAnimationState(this);
            return _idleAnimation;
        }

        public UIElement Land()
        {
            throw new Exception("Can't land after idle");
        }
    }
}