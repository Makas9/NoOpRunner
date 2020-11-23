using NoOpRunner.Core;
using NoOpRunner.Core.Interfaces;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NoOpRunner.Client.Rendering
{
    /// <summary>
    /// Why not one class for all rendering you ask,
    /// I say for VE canvas on player hit or smtg(shaking platforms or blinking power ups, etc.)
    /// </summary>
    public class PlatformsRenderer : IVisualElement
    {
        private IMapPart PlatformsContainer { get; set; }

        public PlatformsRenderer(IMapPart platformsContainer)
        {
            PlatformsContainer = platformsContainer;
        }

        public void Display(Canvas canvas)
        {
            var rectangleWidth = canvas.ActualWidth / GameSettings.HorizontalCellCount;
            var rectangleHeight = canvas.ActualHeight / GameSettings.VerticalCellCount;

            var pixels = PlatformsContainer.Render().ToList();
            if (canvas.Children.Count != pixels.Count)
            {
                canvas.Children.Clear();

                var imageBrush = new ImageBrush(new BitmapImage(ResourcesUriHandler.GetPlatformUri()));

                foreach (var pixel in pixels)
                {
                    var rec = new Rectangle
                    {
                        Width = rectangleWidth,
                        Height = rectangleHeight,
                        Fill = imageBrush
                    };
                    Canvas.SetLeft(rec, rectangleWidth * pixel.X);
                    Canvas.SetBottom(rec, rectangleHeight * pixel.Y);

                    canvas.Children.Add(rec);
                }
            }
            else
            {
                for (int i = 0; i < pixels.Count; i++)
                {
                    canvas.Children[i].SetValue(FrameworkElement.WidthProperty, rectangleWidth);
                    canvas.Children[i].SetValue(FrameworkElement.HeightProperty, rectangleHeight);

                    Canvas.SetLeft(canvas.Children[i], rectangleWidth * pixels[i].X);
                    Canvas.SetBottom(canvas.Children[i], rectangleHeight * pixels[i].Y);
                }
            }
        }
    }
}