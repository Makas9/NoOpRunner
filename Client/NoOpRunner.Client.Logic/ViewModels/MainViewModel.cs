using NoOpRunner.Client.Logic.Base;
using NoOpRunner.Core;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Networking;
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
            var connectionManager = new LoggingConnectionManagerAdapter(new LoggingConnectionManager(Logging.Instance));

            Game = new Core.NoOpRunner(connectionManager);
            SettingsViewModel = new SettingsViewModel();

            Game.OnMessageReceived += HandleMessageReceived;
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
            set => SetField(ref _IsPlaying, value);
        }

        private bool _IsSettingsViewOpen = false;
        public bool IsSettingsViewOpen
        {
            get => _IsSettingsViewOpen;
            set
            {
                SetField(ref _IsSettingsViewOpen, value);
                RaisePropertyChanged(nameof(IsGameViewOpen));
            }
        }

        public bool IsGameViewOpen => !IsSettingsViewOpen;

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

        private ICommand _StartPlayingCommand;
        public ICommand StartPlayingCommand =>
            _StartPlayingCommand ?? (_StartPlayingCommand = new RelayCommand(() =>
            {
                IsPlaying = true;
            }));

        private ICommand _OpenSettingsViewCommand;
        public ICommand OpenSettingsViewCommand =>
            _OpenSettingsViewCommand ?? (_OpenSettingsViewCommand = new RelayCommand(() =>
            {
                IsSettingsViewOpen = true;
            }));
    }
}
