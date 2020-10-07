using NoOpRunner.Client.Logic.ViewModels;
using NoOpRunner.Client.Rendering;
using NoOpRunner.Core;
using NoOpRunner.Core.Enums;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        public MainWindow()
        {
            InitializeComponent();

            play_button.Click += (s, e) =>
            {
                SetUpBackground();

                var viewModel = (MainViewModel) DataContext;

                Game = viewModel.Game;

                if (!Game.IsHost)
                {
                    Game.AddObserver(new PlayerStateObserver(), MessageType.PlayerStateUpdate);
                    Game.AddObserver(new ClientGameInitObserver(), MessageType.InitialGame);
                    Game.AddObserver(new PlayerPositionObserver(), MessageType.PlayerPositionUpdate);
                }

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

            if (Game.GamePlatforms != null && Game.Player != null)
            {
                GameWindowRenderer.RenderPlayer(Game.Player, player_window, Game.GamePlatforms);

                GameWindowRenderer.RenderMap(Game.GamePlatforms, game_platforms);
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
                switch (e.Key)
                {
                    case Key.Up:
                        Game.HandleKeyPress(KeyPress.Up);
                        return;
                    case Key.Right:
                        Game.HandleKeyPress(KeyPress.Right);
                        return;
                    case Key.Left:
                        Game.HandleKeyPress(KeyPress.Left);
                        return;
                    case Key.Space:
                        Game.HandleKeyPress(KeyPress.Space);
                        return;
                    case Key.Down:
                        Game.HandleKeyPress(KeyPress.Down);
                        return;
                    default:
                        return;
                }
            };

            this.KeyUp += (s, e) =>
            {
                switch (e.Key)
                {
                    case Key.Right:
                        Game.HandleKeyRelease(KeyPress.Right);
                        return;
                    case Key.Left:
                        Game.HandleKeyRelease(KeyPress.Left);
                        return;
                    default:
                        return;
                }
            };
        }
    }
}