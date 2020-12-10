using NoOpRunner.Client.Logic.Base;
using NoOpRunner.Client.Logic.Interpreter;
using NoOpRunner.Core;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Networking;
using System;
using System.Windows.Input;

namespace NoOpRunner.Client.Logic.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public Core.NoOpRunner Game { get; set; }

        public SettingsViewModel SettingsViewModel { get; set; }

        public MainViewModel()
        {
            //var connectionManager = new ConnectionManager();
            //Logging.Instance.DisableLevel(LoggingLevel.Trace); // Message logging floods the logs if enabled
            var loggingConnectionManager = new LoggingConnectionManagerAdapter(new LoggingConnectionManager(Logging.Instance));
            var connectionManager = new ConnectionProxy(loggingConnectionManager);

            Game = new Core.NoOpRunner(connectionManager);
            SettingsViewModel = new SettingsViewModel(this);

            Game.OnMessageReceived += HandleMessageReceived;
            IsPlaying = false;
        }

        public void HandleMessageReceived(object sender, MessageDto message)
        {
            if (message.MessageType == Core.Enums.MessageType.InitialConnection)
            {
                IsClientConnected = true;
                StatusMessage = "Client connected.";

                RaisePropertyChanged(nameof(IsWaitingForClientConnection));
            }
            else
            {
                StatusMessage = $"Message received: {message.Payload as string}";
            }
        }

        private string _StatusMessage = "Started";
        public string StatusMessage
        {
            get => _StatusMessage;
            set => SetField(ref _StatusMessage, value);
        }

        private bool _HostConnectButtonsEnabled = true;
        public bool HostConnectButtonsEnabled
        {
            get => _HostConnectButtonsEnabled;
            set => SetField(ref _HostConnectButtonsEnabled, value);
        }

        private bool IsClientConnected { get; set; }

        private bool _IsHosting = true;
        public bool IsHosting
        {
            get => _IsHosting;
            set => SetField(ref _IsHosting, value);
        }

        private bool _IsPlaying = true;
        public bool IsPlaying
        {
            get => _IsPlaying;
            set
            {
                SetField(ref _IsPlaying, value);
                RaisePropertyChanged(nameof(IsNotPlaying));
            }
        }

        public bool IsNotPlaying => !IsPlaying;

        private bool _IsSettingsViewOpen = false;
        public bool IsSettingsViewOpen
        {
            get => _IsSettingsViewOpen;
            set
            {
                SetField(ref _IsSettingsViewOpen, value);
                IsGameViewOpen = !value;
            }
        }

        private bool _IsGameOverScreenOpen = false;
        public bool IsGameOverScreenOpen
        {
            get => _IsGameOverScreenOpen;
            set
            {
                SetField(ref _IsGameOverScreenOpen, value);
            }
        }

        public bool PlayerOneWon { get; set; }

        private int screenWidth = 800;
        public int ScreenWidth
        {
            get => screenWidth;
            set => SetField(ref screenWidth, value);
        }

        private int screenHeight = 600;
        public int ScreenHeight
        {
            get => screenHeight;
            set => SetField(ref screenHeight, value);
        }

        public int PlayerOneHp => Game.Map != null ? Game?.Player?.HealthPoints ?? 0 : 0;

        public int PlayerTwoHp => Game.Map != null ? Game?.PlayerTwo?.CurrentHealth ?? 0 : 0;

        public string PlayerOneHpFormatted => $"Player One HP: {PlayerOneHp}";

        public string PlayerTwoHpFormatted => $"Player Two HP: {PlayerTwoHp}";

        private bool _IsGameViewOpen = true;
        public bool IsGameViewOpen
        {
            get => _IsGameViewOpen;
            set
            {
                SetField(ref _IsGameViewOpen, value);
            }
        }

        public bool IsWaitingForClientConnection => IsHosting && !IsClientConnected;

        private ICommand _StartHostCommand;
        public ICommand StartHostCommand =>
            _StartHostCommand ?? (_StartHostCommand = new RelayCommand(() =>
                                                             {
                                                                 Game.StartHosting();

                                                                 StatusMessage = "Waiting for client connection";
                                                                 HostConnectButtonsEnabled = false;
                                                                 IsHosting = true;
                                                             }));

        private ICommand _ConnectToHostCommand;
        public ICommand ConnectToHostCommand =>
            _ConnectToHostCommand ?? (_ConnectToHostCommand = new RelayCommand(async () =>
            {
                await Game.ConnectToHub();

                StatusMessage = "Connected to host";
                HostConnectButtonsEnabled = false;
            }));

        private ICommand _SendMessageCommand;
        public ICommand SendMessageCommand =>
            _SendMessageCommand ?? (_SendMessageCommand = new RelayCommand(async () =>
            {
                await Game.SendMessage();

                StatusMessage = "Message sent";
            }));

        private ICommand _OpenSettingsViewCommand;
        public ICommand OpenSettingsViewCommand =>
            _OpenSettingsViewCommand ?? (_OpenSettingsViewCommand = new RelayCommand(() =>
            {
                IsSettingsViewOpen = true;
            }));

        public void ExecuteUserCommand(string query)
        {
            try
            {
                var resultExpression = ExpressionTreeBuilder.Build(query);
                resultExpression.Interpret(new InterpreterContext(this));
            }
            catch (Exception e)
            {
                Logging.Instance.Write($"The parsing of the user query failed. Exception: {e}", LoggingLevel.Trace);
            }
        }

        public void UpdatePlayerHp()
        {
            RaisePropertyChanged(nameof(PlayerOneHpFormatted));
            RaisePropertyChanged(nameof(PlayerTwoHpFormatted));
        }
    }
}
