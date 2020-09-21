using NoOpRunner.Client.Logic.Base;
using NoOpRunner.Networking;
using System.Windows.Input;

namespace NoOpRunner.Client.Logic.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public Core.NoOpRunner Game { get; set; }

        public MainViewModel()
        {
            var connectionManager = new ConnectionManager();

            Game = new Core.NoOpRunner(connectionManager);
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

        private ICommand _StartHostCommand;

        public ICommand StartHostCommand =>
            _StartHostCommand ?? (_StartHostCommand = new RelayCommand(() =>
                                                             {
                                                                 Game.StartHosting();

                                                                 StatusMessage = "Hosting";
                                                                 HostConnectButtonsEnabled = false;
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
    }
}
