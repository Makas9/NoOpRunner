using NoOpRunner.Core;
using NoOpRunner.Core.Interfaces;
using System;
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
    public class PowerUpsRenderer : IVisualElement
    {
        private PowerUpsContainer PowerUpsContainer { get; set; }

        public PowerUpsRenderer(PowerUpsContainer powerUpsContainer)
        {
            PowerUpsContainer = powerUpsContainer;
        }

        public void Display(Canvas canvas)
        {
            var rectangleWidth = canvas.ActualWidth / PowerUpsContainer.SizeX;
            var rectangleHeight = canvas.ActualHeight / PowerUpsContainer.SizeY;

            var pixels = PowerUpsContainer.GetPowerUpsEnumerable();
            
            if (canvas.Children.Count != pixels.Count)
            {
                canvas.Children.Clear();

                foreach (var (windowPixel, powerUp) in pixels)
                {
                    var rec = new Rectangle
                    {
                        Width = rectangleWidth,
                        Height = rectangleHeight,
                        Fill = new ImageBrush(new BitmapImage(ResourcesUriHandler.GetPowerUp(powerUp))),
                        Stretch = Stretch.Fill
                    };
                    Canvas.SetLeft(rec, rectangleWidth * windowPixel.X);
                    Canvas.SetBottom(rec, rectangleHeight * windowPixel.Y);

                    canvas.Children.Add(rec);
                }
            }
            else
            {
                for (int i = 0; i < pixels.Count; i++)
                {
                    canvas.Children[i].SetValue(Canvas.WidthProperty, rectangleWidth);
                    canvas.Children[i].SetValue(Canvas.HeightProperty, rectangleHeight);

                    Canvas.SetLeft(canvas.Children[i], rectangleWidth * pixels[i].Item1.X);
                    Canvas.SetBottom(canvas.Children[i], rectangleHeight * pixels[i].Item1.Y);
                }
            }
        }
    }
}