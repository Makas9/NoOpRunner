using NoOpRunner.Client.Components;
using NoOpRunner.Client.Constants;
using NoOpRunner.Client.Logic.Interfaces;
using NoOpRunner.Client.Logic.ViewModels;
using NoOpRunner.Core;
using System.Threading.Tasks;

namespace NoOpRunner.Client.Mediators
{
    public class MainWindowGameMediator : IMediator
    {
        private readonly MainWindow mainWindow;
        private readonly Core.NoOpRunner game;
        private readonly MainViewModel mainViewModel;

        public MediatorButton StartHostingButton { get; set; }
        public MediatorButton ConnectToHostButton { get; set; }
        public MediatorButton OpenSettingsButton { get; set; }
        public MediatorButton PlayButton { get; set; }

        public MediatorButton ExecuteUserInputButton { get; set; }
        public MediatorTextBox UserInputTextBox { get; set; }

        public MainWindowGameMediator(MainWindow mainWindow, Core.NoOpRunner game, MainViewModel mainViewModel)
        {
            this.mainWindow = mainWindow;
            this.game = game;
            this.mainViewModel = mainViewModel;
        }

        public async Task Notify(object s, string e)
        {
            Logging.Instance.Write($"[Mediator] Received event: {e}", LoggingLevel.Mediator);

            switch (e)
            {
                case MediatorConstants.StartHosting:
                    Logging.Instance.Write($"[Mediator] Starting to host", LoggingLevel.Mediator);

                    game.StartHosting();
                    StartHostingButton.IsEnabled = false;
                    ConnectToHostButton.IsEnabled = false;

                    break;
                case MediatorConstants.ConnectToHost:
                    Logging.Instance.Write($"[Mediator] Connecting to host", LoggingLevel.Mediator);

                    await game.ConnectToHub();
                    StartHostingButton.IsEnabled = false;
                    ConnectToHostButton.IsEnabled = false;

                    break;
                case MediatorConstants.OpenSettings:
                    Logging.Instance.Write($"[Mediator] Opening settings view", LoggingLevel.Mediator);

                    mainViewModel.IsSettingsViewOpen = true;

                    break;
                case MediatorConstants.Play:
                    Logging.Instance.Write($"[Mediator] Starting play mode", LoggingLevel.Mediator);

                    mainWindow.StartPlaying();
                    mainViewModel.IsPlaying = true;
                    UserInputTextBox.IsEnabled = false;
                    ExecuteUserInputButton.IsEnabled = false;

                    break;
                case MediatorConstants.ExecuteUserInput:
                    Logging.Instance.Write($"[Mediator] Executing user input", LoggingLevel.Mediator);

                    var query = UserInputTextBox.Text;
                    UserInputTextBox.Text = string.Empty;

                    mainViewModel.ExecuteUserCommand(query);

                    break;
            }
        }
    }
}
