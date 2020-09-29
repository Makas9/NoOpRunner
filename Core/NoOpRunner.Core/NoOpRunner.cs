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

        public GamePlatforms GamePlatforms { get; set; }

        public Player Player { get; set; }

        private bool IsHost { get; set; }

        public bool IsClientConnected { get; private set; }

        private readonly IConnectionManager connectionManager;

        public NoOpRunner(IConnectionManager connectionManager)
        {
            GamePlatforms = new GamePlatforms(32, 32);
            Player = new Player(5, 7);

            GamePlatforms.AddShape(new Platform(0, 0, 0, 10)); // Main platform
            GamePlatforms.AddShape(new Platform(0, 10, 10, 20)); // Second platform


            //GameWindow.AddShape(new Square(5, 5));
            //GameWindow.AddShape(new Square(9, 5));
            //GameWindow.AddShape(new Square(13, 5));
            

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
            if (message.MessageType == MessageType.InitialConnection)
            {
                IsClientConnected = true;
            }

            OnMessageReceived?.Invoke(this, message);
        }

        public void StartHosting()
        {
            connectionManager.Start("http://localhost:8080", HandleMessage);
            IsHost = true;
        }

        public void FireLoop()
        {
            var map = (WindowPixel[,]) GamePlatforms.GetCurrentMap().Clone();
            //this need to be separated, player and map move at diff speed
            //right now map dont move at all
            Player.OnLoopFired(map);
            GamePlatforms.OnLoopFired(map);
        }

        public void HandleKeyPress(KeyPress keyPress)
        {
            Player.HandleKeyPress(keyPress, (WindowPixel[,])GamePlatforms.GetCurrentMap().Clone());
        }
    }
}