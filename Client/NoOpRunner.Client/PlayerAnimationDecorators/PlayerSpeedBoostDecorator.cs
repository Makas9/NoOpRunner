using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using NoOpRunner.Client.Rendering;
using NoOpRunner.Core;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;

namespace NoOpRunner.Client.PlayerAnimationDecorators
{
    public class PlayerSpeedBoostDecorator : PlayerDecorator
    {
        private MediaPlayer MediaPlayer { get; set; }

        public PlayerSpeedBoostDecorator(IVisualElement player) : base(player)
        {
            MediaPlayer = new MediaPlayer();

            MediaPlayer.Open(ResourcesUriHandler.GetPowerUpSound(PowerUps.Speed_Boost));

            MediaPlayer.MediaEnded += (o, a) =>
            {
                MediaPlayer.Position = TimeSpan.Zero;
                MediaPlayer.Play();
            };

            MediaPlayer.Volume = 0.01;
            MediaPlayer.Play();
        }

        public override void Display(Canvas canvas)
        {
            Console.WriteLine("Player decorator: Displaying Speed boost animation");

            var speedBoostAnimation = canvas.Children.OfType<GifImage>()
                .FirstOrDefault(x => x.VisualType == VisualElementType.SpeedBoost);

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