using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes.GenerationStrategies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.ShapeFactories;

namespace NoOpRunner.Core
{
    /// <summary>
    /// Divide to diff Game class(one for player one, another for player two), and diff connection classes(Proxy pattern in future)
    /// connection classes implement ISubject and from Game class notify, that's two patterns
    /// for now this is stupid but will be fixed with proxy pattern(maybe???)
    /// </summary>
    public class NoOpRunner : ISubject
    {
        public bool IsGameStarted { get; set; } = false;
        public event EventHandler OnLoopFired;

        private IList<IObserver> Observers { get; set; }

        public event EventHandler<MessageDto> OnMessageReceived;

        public PlatformsContainer PlatformsContainer { get; private set; }

        public Player Player { get; private set; }

        public PowerUpsContainer PowerUpsContainer { get; private set; }

        public bool IsHost { get; private set; }

        private readonly IConnectionManager connectionManager;

        public NoOpRunner(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
            Observers = new List<IObserver>();
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
            MessageDto messageDto = new MessageDto()
            {
                MessageType = message.MessageType
            };
            switch (message.MessageType)
            {
                case MessageType.PlatformsStatus:
                    messageDto.Payload = PlatformsContainer;

                    break;
                case MessageType.PlayerStatus:
                    messageDto.Payload = Player;

                    break;
                case MessageType.PowerUpsStatus:
                    messageDto.Payload = PowerUpsContainer;
                    
                    break;
                case MessageType.InitialConnection:
                case MessageType.InitialGame:
                    messageDto.Payload = new GameStateDto()
                        {
                            Platforms = PlatformsContainer,
                            Player = Player,
                            PowerUps = PowerUpsContainer
                        };
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            await connectionManager.SendMessageToClient(messageDto);
        }

        private async void ClientHandleMessage(MessageDto message)
        {
            if (!IsGameStarted)
            {
                return;
            }

            switch (message.MessageType)
            {
                case MessageType.PlatformsUpdate:
                case MessageType.PowerUpsUpdate:
                case MessageType.PlayerUpdate:

                    if (!await CheckGameStatus())
                        return;

                    Notify(message);

                    break;
                case MessageType.InitialGame:
                    var gameState = message.Payload as GameStateDto;

                    Player = gameState.Player;
                    PlatformsContainer = gameState.Platforms;
                    PowerUpsContainer = gameState.PowerUps;

                    AddObserver(gameState.Player);
                    AddObserver(gameState.Platforms);
                    AddObserver(gameState.PowerUps);

                    break;
                case MessageType.PlatformsStatus:
                    RemoveObserver(PlatformsContainer);
                    
                    PlatformsContainer = message.Payload as PlatformsContainer;

                    AddObserver(PlatformsContainer);

                    break;
                case MessageType.PlayerStatus:
                    RemoveObserver(Player);
                    
                    Player = message.Payload as Player;

                    AddObserver(Player);

                    break;
                case MessageType.PowerUpsStatus:
                    RemoveObserver(PowerUpsContainer);
                    
                    PowerUpsContainer = message.Payload as PowerUpsContainer;

                    AddObserver(PowerUpsContainer);

                    break;
                case MessageType.InitialConnection:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            OnMessageReceived?.Invoke(this, message);
        }

        private async Task<bool> CheckGameStatus()
        {
            if (PowerUpsContainer == null && PlatformsContainer == null && Player == null)
            {
                //Sometimes messages lost, so to ensure host send GameStatus
                await connectionManager.SendMessageToHost(new MessageDto()
                {
                    MessageType = MessageType.InitialGame
                });
            }else if (PowerUpsContainer == null)
            {
                await connectionManager.SendMessageToHost(new MessageDto()
                {
                    MessageType = MessageType.PowerUpsStatus
                });
            }
            else if (PlatformsContainer == null)
            {
                await connectionManager.SendMessageToHost(new MessageDto()
                {
                    MessageType = MessageType.PlatformsStatus
                });
            }
            else if (Player == null)
            {
                await connectionManager.SendMessageToHost(new MessageDto()
                {
                    MessageType = MessageType.PlayerStatus
                });
            }
            else
            {
                return true;
            }

            return false;
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
            var map = (WindowPixel[,]) PlatformsContainer.GetShapes().Clone();

            //this need to be separated, player and map move at diff speed
            //right now map dont move at all
            Player.OnLoopFired(map);
            PlatformsContainer.OnLoopFired(map);

            //Less data to send than sending whole player instance
            await connectionManager.SendMessageToClient(new MessageDto()
            {
                MessageType = MessageType.PlayerUpdate,
                Payload = new PlayerStateDto()
                {
                    State = Player.State,
                    CenterPosX = Player.CenterPosX,
                    CenterPosY = Player.CenterPosY,
                    IsLookingLeft = Player.IsLookingLeft
                }
            });
        }

        public void FireClientLoop(PlatformsContainer platforms, Player player)
        {
            Player = player;
            PlatformsContainer = platforms;
        }

        public void HandleKeyPress(KeyPress keyPress)
        {
            Player.HandleKeyPress(keyPress, (WindowPixel[,]) PlatformsContainer.GetShapes().Clone());
        }

        private void InitializeGameState()
        {
            //Common aspect ration
            PlatformsContainer =
                new PlatformsContainer(GameSettings.HorizontalCellCount, GameSettings.VerticalCellCount);

            PowerUpsContainer = new PowerUpsContainer(GameSettings.HorizontalCellCount, GameSettings.VerticalCellCount);

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
            BaseShape firstPlatform = impassableFactory.CreateStaticShape(Shape.Platform,
                new CombinedGenerationStrategy(), 0, 0, GameSettings.HorizontalCellCount,
                GameSettings.VerticalCellCount / 3);
            PlatformsContainer.AddShape(firstPlatform);

            AbstractFactory passableFactory = FactoryProducer.GetFactory(passable: true);
            BaseShape secondPlatform = passableFactory.CreateStaticShape(Shape.Platform,
                new PlatformerGenerationStrategy(), 0, GameSettings.VerticalCellCount / 3 + 1,
                GameSettings.HorizontalCellCount, GameSettings.VerticalCellCount * 2 / 3);
            PlatformsContainer.AddShape(secondPlatform);

            // Generate the player above the first platform
            Player = new Player(secondPlatform.CenterPosX, secondPlatform.CenterPosY + 1);

            BaseShape thirdPlatform = passableFactory.CreateStaticShape(Shape.Platform,
                new RandomlySegmentedGenerationStrategy(), 0, GameSettings.VerticalCellCount * 2 / 3 + 1,
                GameSettings.HorizontalCellCount, GameSettings.VerticalCellCount - 3);
            PlatformsContainer.AddShape(thirdPlatform);

            var coordinates = firstPlatform.GetCoords();
            int[] xCoords = coordinates.Item1;
            int[] yCoords = coordinates.Item2;
            int randomLocation = RandLocation(xCoords, yCoords);
            PowerUp testPowerUp =
                new PowerUp(xCoords[randomLocation], yCoords[randomLocation] + 2, PowerUps.Double_Jump);
            PowerUpsContainer.AddShape(testPowerUp);
        }

        public void HandleKeyRelease(KeyPress key)
        {
            Player.HandleKeyRelease(key, (WindowPixel[,]) PlatformsContainer.GetShapes().Clone());
        }

        public void Notify(MessageDto message)
        {
            foreach (var observer in Observers)
            {
                observer.Update(message);
            }
        }

        public void AddObserver(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            //Removes if contains
            if (Observers.Contains(observer))
            {
                Observers.Remove(observer);
            }
        }
    }
}