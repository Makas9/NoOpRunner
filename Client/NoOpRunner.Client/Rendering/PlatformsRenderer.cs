using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NoOpRunner.Core;
using NoOpRunner.Core.Interfaces;

namespace NoOpRunner.Client.Rendering
{
    /// <summary>
    /// Why not one class for all rendering you ask,
    /// I say for VE canvas on player hit or smtg(shaking platforms or blinking power ups, etc.)
    /// </summary>
    public class PlatformsRenderer : IVisualElement
    {
        private PlatformsContainer PlatformsContainer { get; set; }

        public PlatformsRenderer(PlatformsContainer platformsContainer)
        {
            PlatformsContainer = platformsContainer;
        }

        public void Display(Canvas canvas)
        {
            int canvasChildIndex = 0;
            int canvasChildCount = canvas.Children.Count; 
            
            var rectangleWidth = canvas.ActualWidth / PlatformsContainer.SizeX;
            var rectangleHeight = canvas.ActualHeight / PlatformsContainer.SizeY;

            //SAVE SPACE AND TIME EVEN MORE
            foreach (var pixel in PlatformsContainer.GetShapesEnumerable())
            {
                if (canvasChildIndex >= canvasChildCount)
                {
                    var imageBrush = new ImageBrush(new BitmapImage(ResourcesUriHandler.GetPlatformUri()));

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
                else
                {
                    canvas.Children[canvasChildIndex].SetValue(FrameworkElement.WidthProperty, rectangleWidth);
                    canvas.Children[canvasChildIndex].SetValue(FrameworkElement.HeightProperty, rectangleHeight);

                    Canvas.SetLeft(canvas.Children[canvasChildIndex], rectangleWidth * pixel.X);
                    Canvas.SetBottom(canvas.Children[canvasChildIndex], rectangleHeight * pixel.Y);
                    
                    canvasChildIndex++;
                }
            }

            if (canvasChildIndex < canvasChildCount)
            {
                canvas.Children.RemoveRange(canvasChildIndex, canvas.Children.Count-canvasChildIndex);
            }
        }
    }
}