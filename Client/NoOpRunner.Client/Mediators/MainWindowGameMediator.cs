using NoOpRunner.Client.Components;
using NoOpRunner.Client.Constants;
using NoOpRunner.Client.Logic.Interfaces;
using NoOpRunner.Client.Logic.ViewModels;
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

        public async Task Notify(object sender, string evnt)
        {
            switch (evnt)
            {
                case MediatorConstants.StartHosting:
                    game.StartHosting();
                    StartHostingButton.IsEnabled = false;
                    ConnectToHostButton.IsEnabled = false;

                    break;
                case MediatorConstants.ConnectToHost:
                    await game.ConnectToHub();
                    StartHostingButton.IsEnabled = false;
                    ConnectToHostButton.IsEnabled = false;

                    break;
                case MediatorConstants.OpenSettings:
                    mainViewModel.IsSettingsViewOpen = true;

                    break;
                case MediatorConstants.Play:
                    mainWindow.StartPlaying();
                    mainViewModel.IsPlaying = true;
                    UserInputTextBox.IsEnabled = false;
                    ExecuteUserInputButton.IsEnabled = false;

                    break;
                case MediatorConstants.ExecuteUserInput:
                    var query = UserInputTextBox.Text;
                    UserInputTextBox.Text = string.Empty;

                    mainViewModel.ExecuteUserCommand(query);

                    break;
            }
        }
    }
}
