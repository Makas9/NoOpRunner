using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Entities;
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

        public bool IsClientConnected { get; private set; }

        private readonly IConnectionManager connectionManager;

        public NoOpRunner(IConnectionManager connectionManager)
        {
            GameWindow = new GameWindow(32, 32);
            Player = new Player(1, 2);

            /* SHAPE FACTORY DESIGN PATTERN */
            ShapeFactory shapeFactory = new ShapeFactory();
            BaseShape shape1 = shapeFactory.GetShape(Shape.Square, 5, 5);
            BaseShape shape2 = shapeFactory.GetShape(Shape.Circle, 10, 5);
            BaseShape shape3 = shapeFactory.GetShape(Shape.Rectangle, 15, 5);
            GameWindow.AddShape(shape1);
            GameWindow.AddShape(shape2);
            GameWindow.AddShape(shape3);

            /* ABSTRACT SHAPE FACTORY DESIGN PATTERN */
            AbstractFactory abstractShapeFactory = FactoryProducer.GetFactory(true);
            BaseShape aShape1 = abstractShapeFactory.GetShape(Shape.Stairs, 20, 5);
            BaseShape aShape2 = abstractShapeFactory.GetShape(Shape.Stone, 22, 5);
            BaseShape aShape3 = abstractShapeFactory.GetShape(Shape.Fence, 24, 5);
            GameWindow.AddShape(aShape1);
            GameWindow.AddShape(aShape2);
            GameWindow.AddShape(aShape3);


            /*Platform firstPlatform = new Platform(0, 0, 0, 10);
            GameWindow.AddShape(firstPlatform); // Main platform

            Platform secondPlatform = new Platform(0, 10, 10, 20);
            GameWindow.AddShape(secondPlatform); // Second platform

            GameWindow.AddShape(new PowerUp(0, 0, firstPlatform.GetCoordsX(), firstPlatform.GetCoordsY()));
            GameWindow.AddShape(new PowerUp(0, 10, secondPlatform.GetCoordsX(), secondPlatform.GetCoordsY()));*/

            //GameWindow.AddShape(new Square(5, 5));
            //GameWindow.AddShape(new Square(9, 5));
            //GameWindow.AddShape(new Square(13, 5));

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
            GameWindow.OnLoopFired((WindowPixel[,])GameWindow.GetCurrentWindow().Clone());
        }

        public void HandleKeyPress(KeyPress keyPress)
        {
            Player.HandleKeyPress(keyPress, (WindowPixel[,])GameWindow.GetCurrentWindow().Clone());
        }
    }
}