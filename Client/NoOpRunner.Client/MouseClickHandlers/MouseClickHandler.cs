using System.Windows.Controls;
using NoOpRunner.Core;

namespace NoOpRunner.Client.MouseClickHandlers
{
    public abstract class MouseClickHandler
    {
        protected Core.NoOpRunner Game { get; set; }

        protected MouseClickHandler(Core.NoOpRunner game)
        {
            Game = game;
        }

        public void Handle(int positionX, int positionY)
        {
            HandleMouseClick(positionX, positionY);

            NextChainItem?.Handle( positionX, positionY);
        }
        protected MouseClickHandler NextChainItem { get; set; }

        protected abstract void HandleMouseClick(int positionX, int positionY);

        public void SetNextChainItem(MouseClickHandler mouseClickHandler)
        {
            NextChainItem = mouseClickHandler;
        }
    }
}