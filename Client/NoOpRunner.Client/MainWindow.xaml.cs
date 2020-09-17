using NoOpRunner.Client.Logic.Rendering;
using NoOpRunner.Client.Logic.ViewModels;
using NoOpRunner.Core.Enums;
using System;
using System.Windows;
using System.Windows.Input;
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
            this.ContentRendered += (s, e) =>
            {
                Game = ((MainViewModel)DataContext).Game;

                ConfigureKeys();

                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(10);
                timer.Tick += (o, a) => TriggerRender();

                timer.Start();
            };

            InitializeComponent();
        }

        private void TriggerRender()
        {
            var canvas = game_window;

            Game.FireLoop();

            GameWindowRenderer.Render(Game.GameWindow, canvas);
        }

        private void ConfigureKeys()
        {
            this.KeyDown += (s, e) =>
            {
                switch (e.Key)
                {
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
