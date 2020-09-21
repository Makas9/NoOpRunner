using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;
using System;
using System.Threading.Tasks;

namespace NoOpRunner.Core
{
    public class NoOpRunner
    {
        public event EventHandler OnLoopFired;

        public event EventHandler<MessageDto> OnMessageReceived;

        public GameWindow GameWindow { get; set; }

        public Player Player { get; set; }

        private bool IsHost { get; set; }

        private readonly IConnectionManager connectionManager;

        public NoOpRunner(IConnectionManager connectionManager)
        {
            GameWindow = new GameWindow(32, 32);
            Player = new Player(5, 7);

            GameWindow.AddShape(new Square(5, 5));
            GameWindow.AddShape(new Square(9, 5));
            GameWindow.AddShape(new Square(13, 5));


            GameWindow.AddShape(Player);

            this.connectionManager = connectionManager;
        }

        public async Task SendMessage()
        {
            if (IsHost)
            {
                await connectionManager.SendMessageToClient(new MessageDto { Payload = "Testing message to client" });
            }
            else
            {
                await connectionManager.SendMessageToHost(new MessageDto { Payload = "Testing message to host" });
            }
        }

        public async Task ConnectToHub()
        {
            await connectionManager.Connect("http://localhost:8080", HandleMessage);
        }

        private void HandleMessage(MessageDto message)
        {
            OnMessageReceived?.Invoke(this, message);
        }

        public void StartHosting()
        {
            connectionManager.Start("http://localhost:8080");
            IsHost = true;
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