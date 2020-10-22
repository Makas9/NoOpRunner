using NoOpRunner.Client.Controls;
using NoOpRunner.Client.Logic.ViewModels;
using NoOpRunner.Core;
using NoOpRunner.Core.Controls;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace NoOpRunner.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;

        private Core.NoOpRunner Game;

        private RenderGameFramesFacade facade;
        private IInputHandlerAbstraction inputHandler;

        public MainWindow()
        {
            InitializeComponent();

            play_button.Click += (s, e) =>
            {
                SetUpBackground();

                var viewModel = (MainViewModel) DataContext;

                Game = viewModel.Game;

                facade = new RenderGameFramesFacade();

                InputHandlerImplementor inputHandlerImpl;
                if (Game.IsHost)
                    inputHandlerImpl = new InputHandlerImplementorPlayerOne(Game.Player);
                else
                    inputHandlerImpl = new InputHandlerImplementorPlayerTwo();


                inputHandler = new InputHandlerAbstractionArrows(inputHandlerImpl);
                //inputHandler = new InputHandlerAbstractionWasd(inputHandlerImpl);

                ConfigureKeys();

                timer = new DispatcherTimer();
                timer.Interval = GameSettings.TimeBetweenFrames;
                timer.Tick += async (o, a) =>
                {
                    await TriggerRender();
                };

                timer.Start();
                Game.IsGameStarted = true;
            };
        }

        private async Task TriggerRender()
        {
            if (Game.IsHost)
            {
                await facade.HostGameCycle(Game, player_window, power_ups, game_platforms);
            }
            else//for client
            {
                if (Game.PlatformsContainer != null && Game.Player != null && Game.PowerUpsContainer != null)
                {
                    facade.ClientGameCycle(Game, player_window, game_platforms, power_ups);
                }
            }
        }

        /// <summary>
        /// Set up background, more images to make depth feel
        /// </summary>
        private void SetUpBackground()
        {
            for (int i = 1; i < 6; i++)
            {
                background_panel.Children.Add(new Image(){Source = new BitmapImage(ResourcesUriHandler.GetBackground(i)), Stretch = Stretch.Fill});
            }
        }

        private void ConfigureKeys()
        {
            this.KeyDown += (s, e) =>
            {
                inputHandler.HandleKeyDownEvent(e, (WindowPixel[,])Game.PlatformsContainer.GetShapes().Clone());
            };

            this.KeyUp += (s, e) =>
            {
                inputHandler.HandleKeyUpEvent(e, (WindowPixel[,])Game.PlatformsContainer.GetShapes().Clone());
            };
        }
    }
}
