using NoOpRunner.Core.Builders;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.GenerationStrategies;
using NoOpRunner.Core.Shapes.ShapeFactories;
using NoOpRunner.Core.Shapes.StaticShapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public PlatformsContainer PlatformsContainer 
        {
            get => GameState?.Platforms; 
            set => GameState.Platforms = value; 
        }

        public Player Player
        {
            get => GameState?.Player;
            set => GameState.Player = value;
        }
        public PowerUpsContainer PowerUpsContainer
        {
            get => GameState.PowerUpsContainer;
            set => GameState.PowerUpsContainer = value;
        }

        public GameState GameState { get; private set; }

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
            if (!IsGameStarted && message.MessageType != MessageType.InitialConnection)
            {
                return;
            }

            switch (message.MessageType)
            {
                case MessageType.PlatformsUpdate:
                case MessageType.PowerUpsUpdate:
                case MessageType.PlayerUpdate:

                    if (PowerUpsContainer == null && PlatformsContainer == null && Player == null)
                    {
                        //Sometimes messages lost, so to ensure host send GameStatus
                        await connectionManager.SendMessageToHost(new MessageDto()
                        {
                            MessageType = MessageType.InitialGame
                        });
                    }
                    else
                    {
                        Notify(message);
                    }

                    break;
                case MessageType.InitialConnection:
                case MessageType.InitialGame:
                    var gameState = message.Payload as GameStateDto;

                    GameState = new GameState()
                    {
                        Platforms = gameState.Platforms,
                        Player = gameState.Player,
                        PowerUpsContainer = gameState.PowerUps
                    };

                    AddObserver(gameState.Player);
                    AddObserver(gameState.Platforms);
                    AddObserver(gameState.PowerUps);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            OnMessageReceived?.Invoke(this, message);
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
            var gameeStateBuilder = new GameStateBuilder();
            var initialGameState = gameeStateBuilder.Configure()
                .InitializeMap(GameSettings.HorizontalCellCount, GameSettings.VerticalCellCount)
                .InitializePowerUps(GameSettings.HorizontalCellCount, GameSettings.VerticalCellCount)
                .AddImpassableShape(f => f.CreateStaticShape(Shape.Platform, new CombinedGenerationStrategy(), 0, 0, GameSettings.HorizontalCellCount, GameSettings.VerticalCellCount / 3))
                .AddPassableShape(f => f.CreateStaticShape(Shape.Platform, new PlatformerGenerationStrategy(), 0, GameSettings.VerticalCellCount / 3 + 1, GameSettings.HorizontalCellCount, GameSettings.VerticalCellCount * 2 / 3))
                .AddPassableShape(f => f.CreateStaticShape(Shape.Platform, new RandomlySegmentedGenerationStrategy(), 0, GameSettings.VerticalCellCount * 2 / 3 + 1, GameSettings.HorizontalCellCount, GameSettings.VerticalCellCount - 3))
                .AddPlayer(platforms => platforms.Skip(1).First(p => p.GetType() == typeof(PassablePlatform)))
                .AddPowerUp(PowerUps.Double_Jump, platforms => platforms.First(p => p.GetType() == typeof(ImpassablePlatform)))
                //.AddShape(f => f.GetShape(Shape.Saw, 2, 5)) // DEMO: Factory Pattern
                .Build();

            GameState = initialGameState;
            
            //LabTest.TestPrototype(); // DEMO: Prototype Pattern
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