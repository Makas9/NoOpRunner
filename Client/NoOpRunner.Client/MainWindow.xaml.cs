using NoOpRunner.Client.Controls;
using NoOpRunner.Client.Logic.ViewModels;
using NoOpRunner.Client.Rendering;
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

        private IInputHandlerAbstraction inputHandler;

        public MainWindow()
        {
            InitializeComponent();

            play_button.Click += (s, e) =>
            {
                SetUpBackground();

                var viewModel = (MainViewModel) DataContext;

                Game = viewModel.Game;

                InputHandlerImplementor inputHandlerImpl;
                if (Game.IsHost)
                    inputHandlerImpl = new InputHandlerImplementorPlayerOne(Game.Player);
                else
                    inputHandlerImpl = new InputHandlerImplementorPlayerTwo();


                inputHandler = new InputHandlerAbstractionArrows(inputHandlerImpl);
                //inputHandler = new InputHandlerAbstractionWasd(inputHandlerImpl);

                ConfigureKeys();

                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(70);
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
                await Game.FireHostLoop();
            }

            if (Game.PlatformsContainer != null && Game.Player != null && Game.PowerUpsContainer != null)
            {
                GameWindowRenderer.RenderPowerUps(Game.PowerUpsContainer, power_ups);//for now
                
                GameWindowRenderer.RenderPlayer(Game.Player, player_window, Game.PlatformsContainer);

                GameWindowRenderer.RenderPlatforms(Game.PlatformsContainer, game_platforms);
            }


            //Use one more canvas for power-ups and one more for traps
        }

        /// <summary>
        /// Set up background, more images to make depth feel
        /// </summary>
        private void SetUpBackground()
        {
            for (int i = 1; i < 6; i++)
            {
                background_panel.Children.Add(new Image(){Source = new BitmapImage(SpritesUriHandler.GetBackground(i)), Stretch = Stretch.Fill});
            }
        }

        private void ConfigureKeys()
        {
            this.KeyDown += (s, e) =>
            {
                inputHandler.HandleKeyEvent(e, (WindowPixel[,])Game.PlatformsContainer.GetShapes().Clone());
            };

            this.KeyUp += (s, e) =>
            {
                inputHandler.HandleKeyUpEvent(e, (WindowPixel[,])Game.PlatformsContainer.GetShapes().Clone());
            };
        }
    }
}