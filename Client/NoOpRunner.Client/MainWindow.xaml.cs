using NoOpRunner.Client.Logic.Rendering;
using NoOpRunner.Client.Logic.ViewModels;
using NoOpRunner.Core.Enums;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using NoOpRunner.Core;

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

                ConfigureKeys();

                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(1000 / 30); // ~30 fps
                timer.Tick += (o, a) => TriggerRender();

                timer.Start();
            };
        }

        private void TriggerRender()
        {
            var canvas = game_window;

            Game.FireLoop();

            GameWindowRenderer.RenderPlayer(Game.Player, player_window, Game.GameWindow);

            GameWindowRenderer.Render(Game.GameWindow, canvas);
        }

        private void SetUpBackground()
        {
            game_window.Background = new ImageBrush(InitBitmapImage(SpritesUriHandler.GetBackground()));
        }

        private static BitmapImage InitBitmapImage(Uri uri)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = uri;
            bitmapImage.EndInit();

            return bitmapImage;
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
        }
    }
}