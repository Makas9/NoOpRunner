using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NoOpRunner.Core;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Client.Rendering
{
    public class PlayerRenderer : IVisualElement
    {
        private Player Player { get; set; }

        public PlayerRenderer(Player player)
        {
            Player = player;
        }

                public void Display(Canvas canvas)
        {
            var animationPixel = Player.GetAnimationPixel(out int hitBoxY, out int hitBoxX);

            var gifWidth = canvas.ActualWidth / GameSettings.HorizontalCellCount;
            var gifHeight = canvas.ActualHeight / GameSettings.VerticalCellCount;

            var playerAnimation = canvas.Children.OfType<GifImage>()
                .FirstOrDefault(x => x.VisualType == VisualElementType.Player);

            Player.DoAction();

            if (playerAnimation == null)
            {
                playerAnimation = new GifImage()
                {
                    Width = gifWidth,
                    Height = gifHeight,
                    VisualType = VisualElementType.Player,
                    GifSource = Player.StateUri,
                    Stretch = Stretch.Fill
                };

                Canvas.SetLeft(playerAnimation, gifWidth * animationPixel.X * hitBoxX);
                Canvas.SetBottom(playerAnimation, gifHeight * animationPixel.Y * hitBoxY);

                canvas.Children.Add(playerAnimation);

                playerAnimation.StartAnimation();
            }
            else
            {
                if (Player.StateHasChanged)
                {
                    playerAnimation.SetValue(GifImage.GifSourceProperty,
                        Player.StateUri);
                }
            }

            foreach (UIElement canvasChild in canvas.Children)
            {
                //Move
                RenderingHelper.AnimateUiElementMove(canvasChild, gifWidth * animationPixel.X,
                    gifHeight * animationPixel.Y);

                //Resize
                canvasChild.SetValue(FrameworkElement.WidthProperty, gifWidth * hitBoxX); // sketch for all hit box
                canvasChild.SetValue(FrameworkElement.HeightProperty, gifHeight * hitBoxY); // sketch for all hit box

                //Mirror
                if (Player.IsTurning)
                {
                    canvasChild.SetValue(UIElement.RenderTransformProperty,
                        Player.IsLookingLeft
                            ? new ScaleTransform() {ScaleX = -1, CenterX = gifWidth / 2}
                            : new ScaleTransform() {ScaleX = 1, CenterX = gifWidth / 2});
                }
            }
        }
    }
}