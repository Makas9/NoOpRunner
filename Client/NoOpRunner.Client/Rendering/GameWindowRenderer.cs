using NoOpRunner.Core;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NoOpRunner.Client.Logic.Rendering
{
    public static class GameWindowRenderer
    {
        public static void Render(GameWindow window, Canvas canvas)
        {
            var rectangleWidth = canvas.ActualWidth / window.SizeX;
            var rectangleHeight = canvas.ActualHeight / window.SizeY;

            canvas.Children.Clear();

            foreach (var pixel in window.GetCurrentWindow())
            {
                var rec = new Rectangle
                {
                    Width = rectangleWidth,
                    Height = rectangleHeight,
                    Fill = ColorBrushMap[pixel.Color]
                };

                Canvas.SetLeft(rec, rectangleWidth * pixel.X);
                Canvas.SetBottom(rec, rectangleHeight * pixel.Y);

                rec.MouseLeftButtonDown += (s, e) =>
                {
                    window.ClickShape(pixel.X, pixel.Y);
                };

                canvas.Children.Add(rec);
            }
        }

        private static Dictionary<Core.Enums.Color, SolidColorBrush> ColorBrushMap = new Dictionary<Core.Enums.Color, SolidColorBrush>
        {
            { Core.Enums.Color.Red, Brushes.Red },
            { Core.Enums.Color.Green, Brushes.Green },
            { Core.Enums.Color.Yellow, Brushes.Yellow },
            { Core.Enums.Color.Blue, Brushes.Blue },
            { Core.Enums.Color.Black, Brushes.Black }
        };
    }
}
