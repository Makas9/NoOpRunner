using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using NoOpRunner.Client.Rendering;
using NoOpRunner.Core;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;

namespace NoOpRunner.Client.PlayerAnimationDecorators
{
    public class PlayerDoubleJumpDecorator : PlayerDecorator
    {
        public PlayerDoubleJumpDecorator(IVisualElement player) : base(player)
        {
        }

        public override void Display(Canvas canvas)
        {
            Logging.Instance.Write("Player decorator: Displaying Double jump animation", LoggingLevel.Pattern);

            var doubleJumpAnimation = canvas.Children.OfType<GifImage>().FirstOrDefault(x=> x.VisualType == VisualElementType.DoubleJump);

            if (doubleJumpAnimation == null)
            {
                doubleJumpAnimation = new GifImage
                {
                    VisualType = VisualElementType.DoubleJump,
                    GifSource = ResourcesUriHandler.GetPlayerPowerUp(VisualElementType.DoubleJump),
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