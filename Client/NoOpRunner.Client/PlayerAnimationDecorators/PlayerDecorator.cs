using System.Windows.Controls;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;

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
            if (Player.GetType() != typeof(Player))
            {
                Player = ((PlayerDecorator) Player).RemoveLayer(visualElementType);
            }
        }
        
    }
}