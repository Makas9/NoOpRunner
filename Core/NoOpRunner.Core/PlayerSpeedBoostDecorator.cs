using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using NoOpRunner.Core.Decorators;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Rendering;

namespace NoOpRunner.Core
{
    public class PlayerSpeedBoostDecorator : PlayerDecorator
    {
        private MediaPlayer MediaPlayer { get; set; }

        private static readonly int CyclesExists = GameSettings.Fps.Milliseconds/GameSettings.Fps.Milliseconds*1000*2;

        public PlayerSpeedBoostDecorator(IVisualElement player) : base(player)
        {
            MediaPlayer = new MediaPlayer();

            MediaPlayer.Open(ResourcesUriHandler.GetPowerUpSound(PowerUps.Speed_Boost));

            MediaPlayer.MediaEnded += (o,a) =>
            {
                MediaPlayer.Position = TimeSpan.Zero;
                MediaPlayer.Play();
            };

            MediaPlayer.Volume = 0.1;
            MediaPlayer.Play();
        }

        public override void Display(Canvas canvas)
        {
            Console.WriteLine("Player decorator: Displaying Speed boost animation");

            var speedBoostAnimation = canvas.Children.OfType<GifImage>().FirstOrDefault(x=> x.VisualType == VisualElementType.SpeedBoost);

            if (speedBoostAnimation == null)
            {
                speedBoostAnimation = new GifImage
                {
                    VisualType = VisualElementType.SpeedBoost,
                    GifSource = ResourcesUriHandler.GetPlayerPowerUp(VisualElementType.SpeedBoost),
                    Stretch = Stretch.Fill
                };

                canvas.Children.Add(speedBoostAnimation);
            }

            base.Display(canvas);
        }

        public override IVisualElement RemoveLayer(VisualElementType visualElementType)
        {
            if (visualElementType == VisualElementType.SpeedBoost)
            {
                MediaPlayer.Pause();
                MediaPlayer.Close();
                
                
                
                return Player;
            }

            UpdatePlayerWhileRemoving(visualElementType);

            return this;
        }
    }
}