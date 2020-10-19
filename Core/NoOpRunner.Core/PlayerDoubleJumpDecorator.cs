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
    public class PlayerDoubleJumpDecorator : PlayerDecorator
    {
        public PlayerDoubleJumpDecorator(IVisualElement player) : base(player)
        {
        }

        public override void Display(Canvas canvas)
        {
            Console.WriteLine("Player decorator: Displaying Double jump animation");

            var doubleJumpAnimation = canvas.Children.OfType<GifImage>().FirstOrDefault(x=> x.VisualType == VisualElementType.DoubleJump);

            if (doubleJumpAnimation == null)
            {
                doubleJumpAnimation = new GifImage
                {
                    VisualType = VisualElementType.DoubleJump,
                    GifSource = SpritesUriHandler.GetPlayerPowerUp(VisualElementType.DoubleJump),
                    Stretch = Stretch.Fill
                };

                canvas.Children.Add(doubleJumpAnimation);
            }

            base.Display(canvas);
        }

        public override IVisualElement RemoveLayer(VisualElementType visualElementType)
        {
            if (visualElementType == VisualElementType.DoubleJump)
            {
                return Player;
            }

            UpdatePlayerWhileRemoving(visualElementType);

            return this;
        }
    }
}