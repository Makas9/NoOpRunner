using NoOpRunner.Core.Builders;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.GenerationStrategies;
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
            get => GameState?.PowerUpsContainer;
            set => GameState.PowerUpsContainer = value;
        }

        private GameState GameState { get; set; }

        public bool IsHost { get; private set; }

        private readonly IConnectionManager connectionManager;

        public NoOpRunner(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
            Observers = new List<IObserver>();
        }

        public async Task OnMapMoveLoopFired()
        {
            PowerUpsContainer.ShiftShapes();
            
            PlatformsContainer.ShiftShapes();
            
            await connectionManager.SendMessageToClient(new MessageDto()
            {
                MessageType = MessageType.PlatformsUpdate,
                Payload = PlatformsContainer.GetNextBlocks()
            });

            Player.OnMapMoveLoopFired((WindowPixel[,])PlatformsContainer.GetShapes().Clone());

            //Send null for now, because make client push power ups by himself will make a lot of sync problems
            await connectionManager.SendMessageToClient(new MessageDto()
            {
                MessageType = MessageType.PowerUpsUpdate
            });
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
                    messageDto.Payload = GameState;

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

                    GameState = message.Payload as GameState;

                    AddObserver(GameState.Player);
                    AddObserver(GameState.Platforms);
                    AddObserver(GameState.PowerUpsContainer);

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

        public async Task UpdateClientsGame()
        {
            await connectionManager.SendMessageToClient(new MessageDto()
            {
                MessageType = MessageType.PlayerUpdate,
                Payload = new PlayerStateDto()
                {
                    State = Player.State,
                    CenterPosX = Player.CenterPosX,
                    CenterPosY = Player.CenterPosY,
                    IsLookingLeft = Player.IsLookingLeft,
                    PlayerOnePowerUps = Player.PlayerOnePowerUps
                }
            });
        }

        private void InitializeGameState()
        {
            var gameeStateBuilder = new GameStateBuilder();
            var initialGameState = gameeStateBuilder.Configure()
                .InitializeMap(GameSettings.HorizontalCellCount, GameSettings.VerticalCellCount)
                .InitializePowerUps(GameSettings.HorizontalCellCount, GameSettings.VerticalCellCount)
                .AddImpassableShape(f => f.CreateStaticShape(Shape.Platform, new RandomlySegmentedGenerationStrategy(), 0, 0, GameSettings.HorizontalCellCount, GameSettings.VerticalCellCount / 3))
                .AddPassableShape(f => f.CreateStaticShape(Shape.Platform, new PlatformerGenerationStrategy(), 0, GameSettings.VerticalCellCount / 3 + 1, GameSettings.HorizontalCellCount, GameSettings.VerticalCellCount * 2 / 3))
                .AddPassableShape(f => f.CreateStaticShape(Shape.Platform, new RandomlySegmentedGenerationStrategy(), 0, GameSettings.VerticalCellCount * 2 / 3 + 1, GameSettings.HorizontalCellCount, GameSettings.VerticalCellCount - 3))
                .AddPlayer(platforms => platforms.Skip(1).First(p => p.GetType() == typeof(PassablePlatform)))
                .AddPowerUp(PowerUps.Speed_Boost, platforms => platforms.First(p => p.GetType() == typeof(ImpassablePlatform)))
                .AddPowerUp(PowerUps.Double_Jump, platforms  =>
                {

                    var plat = platforms.First(p => p.GetType() == typeof(ImpassablePlatform));
                    return new PowerUp(plat.CenterPosX+1, plat.CenterPosY, PowerUps.Double_Jump);
                })
                .AddPowerUp(PowerUps.Invulnerability, platforms  =>
                {

                    var plat = platforms.First(p => p.GetType() == typeof(ImpassablePlatform));
                    return new PowerUp(plat.CenterPosX+2, plat.CenterPosY, PowerUps.Invulnerability);
                })
                //.AddShape(f => f.GetShape(Shape.Saw, 2, 5)) // DEMO: Factory Pattern
                .Build();

            GameState = initialGameState;

            //LabTest.TestPrototype(); // DEMO: Prototype Pattern
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
