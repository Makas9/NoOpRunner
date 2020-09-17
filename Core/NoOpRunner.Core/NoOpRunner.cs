using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes;
using System;

namespace NoOpRunner.Core
{
    public class NoOpRunner
    {
        public event EventHandler OnLoopFired;

        public GameWindow GameWindow { get; set; }

        public Player Player { get; set; }

        public NoOpRunner()
        {
            GameWindow = new GameWindow(32, 32);
            Player = new Player(5, 7);

            GameWindow.AddShape(new Platform(0, 0));


            //GameWindow.AddShape(new Square(5, 5));
            //GameWindow.AddShape(new Square(9, 5));
            //GameWindow.AddShape(new Square(13, 5));


            GameWindow.AddShape(Player);
        }

        public void FireLoop()
        {
            GameWindow.OnLoopFired((WindowPixel[,])GameWindow.GetCurrentWindow().Clone());
        }

        public void HandleKeyPress(KeyPress keyPress)
        {
            Player.HandleKeyPress(keyPress, (WindowPixel[,])GameWindow.GetCurrentWindow().Clone());
        }
    }
}
