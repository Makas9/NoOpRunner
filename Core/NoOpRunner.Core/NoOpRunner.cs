using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.ShapeFactories;
using System;
using System.Threading.Tasks;

namespace NoOpRunner.Core
{
    public class NoOpRunner : ClientSubject
    {
        public bool IsGameStarted { get; set; } = false;
        public event EventHandler OnLoopFired;

        public event EventHandler<MessageDto> OnMessageReceived;

        public GamePlatforms GamePlatforms { get; set; }

        public Player Player { get; set; }

        public bool IsHost { get; private set; }

        private readonly IConnectionManager connectionManager;

        public NoOpRunner(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        private int RandLocation(int[] platformXCoords, int[] platformYCoords)
        {
            int found = -1, x = -1;
            while (found == -1)
            {
                x = RandomNumber.GetInstance().Next(2, platformXCoords.Length - 2);
                if (platformYCoords[x - 2] == platformYCoords[x] &&
                    platformYCoords[x - 1] == platformYCoords[x] &&
                    platformYCoords[x + 1] == platformYCoords[x] &&
                    platformYCoords[x + 2] == platformYCoords[x])
                {
                    found = x; // Spawn power up between flat platform
                }
            }

            return x;
        }

        public async Task SendMessage()
        {
            if (IsHost)
            {
                await connectionManager.SendMessageToClient(new MessageDto {Payload = "Testing message to client"});
            }
            else
            {
                await connectionManager.SendMessageToHost(new MessageDto {Payload = "Testing message to host"});
            }
        }

        public async Task ConnectToHub()
        {
            await connectionManager.Connect("http://localhost:8080", ClientHandleMessage);
        }

        private async void HostHandleMessage(MessageDto message)
        {
            MessageDto messageDto = null;
            switch (message.MessageType)
            {
                case MessageType.InitialConnection:
                    break;
                case MessageType.PlatformsStatus:
                    messageDto = new MessageDto()
                    {
                        MessageType = message.MessageType,
                        Payload = GamePlatforms
                    };
                    break;
                case MessageType.PlayerStatus:
                    messageDto = new MessageDto()
                    {
                        MessageType = message.MessageType,
                        Payload = Player
                    };
                    break;
                case MessageType.InitialGame:
                    messageDto = new MessageDto()
                    {
                        MessageType = message.MessageType,
                        Payload = new GameStateUpdateDto()
                        {
                            Platforms = GamePlatforms,
                            Player = Player
                        }
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            await connectionManager.SendMessageToClient(messageDto);
        }

        private void ClientHandleMessage(MessageDto message)
        {
            if (!IsGameStarted)
            {
                return;
            }

            switch (message.MessageType)
            {
                case MessageType.PlayerStateUpdate:
                case MessageType.PlayerPositionUpdate:
                    HandlePlayerUpdate(message);

                    break;
                case MessageType.InitialGame:
                case MessageType.PlatformsStatus:
                case MessageType.PlayerStatus:
                    Notify(this, message.MessageType, message.Payload);
                    
                    break;
                case MessageType.InitialConnection:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            OnMessageReceived?.Invoke(this, message);
        }

        private async void HandlePlayerUpdate(MessageDto message)
        {
            switch (GamePlatforms)
            {
                case null when Player == null:
                    await connectionManager.SendMessageToHost(new MessageDto()
                    {
                        MessageType = MessageType.InitialGame
                    });
                    break;
                case null:
                    await connectionManager.SendMessageToHost(new MessageDto()
                    {
                        MessageType = MessageType.PlatformsStatus
                    });
                    break;
                default:
                {
                    if (Player == null)
                    {
                        await connectionManager.SendMessageToHost(new MessageDto()
                        {
                            MessageType = MessageType.PlayerStatus
                        });
                    }
                    else
                    {
                        Notify(this, message.MessageType, message.Payload);
                    }

                    break;
                }
            }
        }

        public void StartHosting()
        {
            InitializeGameState();
            connectionManager.Start("http://localhost:8080", HostHandleMessage);
            IsHost = true;

            Logging.Instance.Write("Started hosting..");
        }

        public async Task FireHostLoop()
        {
            var map = (WindowPixel[,]) GamePlatforms.GetCurrentMap().Clone();

            //this need to be separated, player and map move at diff speed
            //right now map dont move at all
            Player.OnLoopFired(map);
            GamePlatforms.OnLoopFired(map);

            //Less data to send than sending whole player instance
            await connectionManager.SendMessageToClient(new MessageDto
            {
                MessageType = MessageType.PlayerStateUpdate,
                Payload = new PlayerStateDto()
                {
                    State = Player.State,
                    IsLookingLeft = Player.IsLookingLeft
                }
            });

            await connectionManager.SendMessageToClient(new MessageDto()
            {
                MessageType = MessageType.PlayerPositionUpdate,
                Payload = (Player.CenterPosX, Player.CenterPosY)
            });
        }

        public void FireClientLoop(GamePlatforms platforms, Player player)
        {
            Player = player;
            GamePlatforms = platforms;
        }

        public void HandleKeyPress(KeyPress keyPress)
        {
            Player.HandleKeyPress(keyPress, (WindowPixel[,]) GamePlatforms.GetCurrentMap().Clone());
        }

        private void InitializeGameState()
        {
            //Common aspect ration
            GamePlatforms = new GamePlatforms(GameSettings.AspectRatioWidth * GameSettings.CellsSizeMultiplier,
                GameSettings.AspectRatioHeight * GameSettings.CellsSizeMultiplier);
            Player = new Player(1, 2);

            /* SHAPE FACTORY DESIGN PATTERN */
            /*ShapeFactory shapeFactory = new ShapeFactory();
            BaseShape shape1 = shapeFactory.GetShape(Shape.HealthCrystal, 1, 15);
            BaseShape shape2 = shapeFactory.GetShape(Shape.DamageCrystal, 5, 15);
            BaseShape shape3 = shapeFactory.GetShape(Shape.Saw, 8, 15);
            GamePlatforms.AddShape(shape1);
            GamePlatforms.AddShape(shape2);
            GamePlatforms.AddShape(shape3);*/

            /* ABSTRACT SHAPE FACTORY DESIGN PATTERN */
            /*AbstractFactory abstractShapeFactory = FactoryProducer.GetFactory(passable: true);
            BaseShape aShape1 = abstractShapeFactory.CreateStaticShape(Shape.Platform, 0, 0, 0, 10);
            BaseShape aShape2 = abstractShapeFactory.CreateEntityShape(Shape.HealthCrystal, 10, 15);
            BaseShape aShape3 = abstractShapeFactory.CreateEntityShape(Shape.DamageCrystal, 15, 15);
            GamePlatforms.AddShape(aShape1);
            GamePlatforms.AddShape(aShape2);
            GamePlatforms.AddShape(aShape3);*/

            AbstractFactory impassableFactory = FactoryProducer.GetFactory(passable: false);
            BaseShape firstPlatform = impassableFactory.CreateStaticShape(Shape.Platform, 0, 0, 0, 10);
            GamePlatforms.AddShape(firstPlatform); // Main platform

            AbstractFactory passableFactory = FactoryProducer.GetFactory(passable: true);
            BaseShape secondPlatform = passableFactory.CreateStaticShape(Shape.Platform, 0, 10, 10, 20);
            GamePlatforms.AddShape(secondPlatform); // Second platform

            var coordinates = firstPlatform.GetCoords();
            int[] xCoords = coordinates.Item1;
            int[] yCoords = coordinates.Item2;
            int randomLocation = RandLocation(xCoords, yCoords);
            PowerUp testPowerUp = new PowerUp(xCoords[randomLocation], yCoords[randomLocation], PowerUps.Double_Jump);
            GamePlatforms.AddShape(testPowerUp.SpawnPowerUp());
        }

        public void HandleKeyRelease(KeyPress key)
        {
            Player.HandleKeyRelease(key, (WindowPixel[,]) GamePlatforms.GetCurrentMap().Clone());
        }
    }
}