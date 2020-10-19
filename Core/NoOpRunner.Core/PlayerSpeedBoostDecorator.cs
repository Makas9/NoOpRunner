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
        protected PlayerSpeedBoostDecorator(IVisualElement player) : base(player)
        {
        }

        public override void Display(Canvas canvas)
        {
            Console.WriteLine("Player decorator: Displaying Speed boost animation");

            var speedBoostAnimation = canvas.Children.OfType<GifImage>().FirstOrDefault(x=> x.VisualType == VisualElementType.SpeedBoost);

            if (speedBoostAnimation == null)
            {
                speedBoostAnimation = new GifImage
                {
                    VisualType = VisualElementType.DoubleJump,
                    GifSource = SpritesUriHandler.GetPlayerPowerUp(VisualElementType.SpeedBoost),
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
                return Player;
            }

            UpdatePlayerWhileRemoving(visualElementType);

            return this;
        }
    }
}