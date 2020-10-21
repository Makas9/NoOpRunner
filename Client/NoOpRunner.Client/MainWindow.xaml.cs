using NoOpRunner.Client.Logic.ViewModels;
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

        private GameFrameCycleFacade facade;

        public MainWindow()
        {
            InitializeComponent();

            play_button.Click += (s, e) =>
            {
                SetUpBackground();

                var viewModel = (MainViewModel) DataContext;

                Game = viewModel.Game;
                
                facade = new GameFrameCycleFacade();

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
                    case Key.D1:
                        Game.HandleKeyPress(KeyPress.PowerUp1);//speed
                        return;
                    case Key.D2:
                        Game.HandleKeyPress(KeyPress.PowerUp2);//speed
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