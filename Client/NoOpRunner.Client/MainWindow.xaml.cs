using NoOpRunner.Client.Components;
using NoOpRunner.Client.Constants;
using NoOpRunner.Client.Controls;
using NoOpRunner.Client.Logic.ViewModels;
using NoOpRunner.Client.Mediators;
using NoOpRunner.Core;
using NoOpRunner.Core.Controls;
using System;
using System.Linq;
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

        private RenderingFacade renderingFacade;
        private IInputHandlerAbstraction inputHandler;

        public MainWindow()
        {
            InitializeComponent();

            // var flyweightTest = new FlyweightTestOPP();
            // flyweightTest.Show();
            
            this.SizeChanged += (s, e) =>
            {
                foreach (var canvas in CanvasGrid.Children.OfType<Canvas>())
                {
                    foreach (UIElement child in canvas.Children)
                    {
                        child.SetValue(WidthProperty, 
                            canvas.ActualWidth /GameSettings.HorizontalCellCount);
                
                        child.SetValue(HeightProperty, 
                            canvas.ActualHeight /GameSettings.VerticalCellCount);
                    }
                }
            };
            
            this.Loaded += (s, e) =>
            {
                var viewModel = (MainViewModel)DataContext;

                Game = viewModel.Game;

                ConfigureMainScreenInterface();
            };
        }

        private async Task TriggerRender()
        {
            if (Game.PlatformsContainer != null && Game.Player != null && Game.PowerUpsContainer != null)
            {
                await renderingFacade.CycleGameFrames(Game, player_window, power_ups, game_platforms);
            }
        }

        /// <summary>
        /// Set up background, more images to make depth feel
        /// </summary>
        private void SetUpBackground()
        {
            for (int i = 1; i < 6; i++)
            {
                background_panel.Children.Add(new Image()
                    {Source = new BitmapImage(ResourcesUriHandler.GetBackground(i)), Stretch = Stretch.Fill});
            }
        }

        private void ConfigureKeys()
        {
            this.KeyDown += (s, e) =>
            {
                inputHandler.HandleKeyDownEvent(e, (WindowPixel[,])Game.PlatformsContainer.RenderPixels(!Game.IsHost).Clone());
            };

            this.KeyUp += (s, e) =>
            {
                inputHandler.HandleKeyUpEvent(e, (WindowPixel[,])Game.PlatformsContainer.RenderPixels(!Game.IsHost).Clone());
            };
        }

        public void StartPlaying()
        {
            SetUpBackground();

            var viewModel = (MainViewModel)DataContext;

            InputHandlerImplementor inputHandlerImpl;
            if (Game.IsHost)
            {
                renderingFacade = new HostRenderingFacade();

                inputHandlerImpl = new InputHandlerImplementorPlayerOne(Game.Player);
            }
            else
            {
                renderingFacade = new ClientRenderingFacade();

                inputHandlerImpl = new InputHandlerImplementorPlayerTwo();
            }

            inputHandler = new InputHandlerAbstractionArrows(inputHandlerImpl);
            //inputHandler = new InputHandlerAbstractionWasd(inputHandlerImpl);

            ConfigureKeys();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(GameSettings.TimeBetweenFramesMs);
            timer.Tick += async (o, a) => { await TriggerRender(); };

            timer.Start();
            Game.IsGameStarted = true;

            background_panel.MouseLeftButtonDown += (o, a) =>
            {
                var pos = a.GetPosition(background_panel);
                var cellWidth = (int)background_panel.ActualWidth / GameSettings.HorizontalCellCount;
                var cellHeight = (int)background_panel.ActualHeight / GameSettings.VerticalCellCount;

                Game.HandleMouseClick((int)pos.X / cellWidth, (int)(background_panel.ActualHeight - pos.Y) / cellHeight);
            };
        }

        private void ConfigureMainScreenInterface()
        {
            var mainWindowMediator = new MainWindowGameMediator(this, Game, (MainViewModel)DataContext);
            var startHostingButton = new MediatorButton(mainWindowMediator, MediatorConstants.StartHosting)
            {
                Content = "Start Hosting"
            };
            Grid.SetColumn(startHostingButton, 0);

            var connectToHostBtn = new MediatorButton(mainWindowMediator, MediatorConstants.ConnectToHost)
            {
                Content = "Connect"
            };
            Grid.SetColumn(connectToHostBtn, 1);

            var openSettingsBtn = new MediatorButton(mainWindowMediator, MediatorConstants.OpenSettings)
            {
                Content = "Settings"
            };
            Grid.SetColumn(openSettingsBtn, 2);

            var playBtn = new MediatorButton(mainWindowMediator, MediatorConstants.Play)
            {
                Content = "Play"
            };
            Grid.SetColumn(playBtn, 3);

            var userInputTextBox = new MediatorTextBox(mainWindowMediator, MediatorConstants.UserInputValueChanged);
            Grid.SetColumn(userInputTextBox, 0);

            var executeUserInputBtn = new MediatorButton(mainWindowMediator, MediatorConstants.ExecuteUserInput)
            {
                Content = "Execute"
            };
            Grid.SetColumn(executeUserInputBtn, 1);

            mainWindowMediator.StartHostingButton = startHostingButton;
            mainWindowMediator.ConnectToHostButton = connectToHostBtn;
            mainWindowMediator.OpenSettingsButton = openSettingsBtn;
            mainWindowMediator.PlayButton = playBtn;
            mainWindowMediator.UserInputTextBox = userInputTextBox;
            mainWindowMediator.ExecuteUserInputButton = executeUserInputBtn;

            button_grid.Children.Add(startHostingButton);
            button_grid.Children.Add(connectToHostBtn);
            button_grid.Children.Add(openSettingsBtn);
            button_grid.Children.Add(playBtn);
            input_grid.Children.Add(userInputTextBox);
            input_grid.Children.Add(executeUserInputBtn);
        }
    }
}