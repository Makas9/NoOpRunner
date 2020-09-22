using System;
using NoOpRunner.Client.Logic.ViewModels;
using NoOpRunner.Core.Enums;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using NoOpRunner.Client.Logic.Rendering;
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
            this.ContentRendered += (s, e) =>
            {
                var viewModel = (MainViewModel)DataContext;

                Game = viewModel.Game;

                Game.OnMessageReceived += (o, a) =>
                {
                    viewModel.StatusMessage = $"Message received: {a.Payload as string}";
                };

                ConfigureKeys();

                TriggerRender();
                
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(10);
                // timer.Tick += (o, a) => TriggerRender();
                timer.Tick += (o, a) => TriggerMapAndPlayerRender();
                
                timer.Start();
                
            };

            InitializeComponent();
        }

        private int renderCounter = 0;

        private void TriggerMapAndPlayerRender()
        {
            GameWindowRenderer.RenderPlayer(Game.Player, this.player_one_window, Game.GameWindow);

            #region Map update or not

            if (renderCounter == 5)
            {
                renderCounter = 0;
                
                TriggerRender();
            }
            
            renderCounter++;

            #endregion
        }
        private void TriggerRender()
        {
            var canvas = map_window;

            Game.FireLoop();

            GameWindowRenderer.Render(Game.GameWindow, canvas);
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
