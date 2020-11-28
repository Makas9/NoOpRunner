using System;
using System.Windows.Controls;
using NoOpRunner.Client.Rendering;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;

namespace NoOpRunner.Client.PlayerAnimationDecorators
{
    public abstract class PlayerDecorator : IVisualElement
    {
        protected IVisualElement Player { get; set; }

        protected PlayerDecorator(IVisualElement player)
        {
            Player = player;
        }

        public virtual void Display(Canvas canvas)
        {
            Player.Display(canvas);
        }

        public abstract IVisualElement RemoveLayer(VisualElementType visualElementType);

        protected void UpdatePlayerWhileRemoving(VisualElementType visualElementType)
        {
            if (Player.GetType() != typeof(PlayerRenderer))
            {
                Player = ((PlayerDecorator) Player).RemoveLayer(visualElementType);
            }
        }
        
    }
}