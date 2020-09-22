using System;
using NoOpRunner.Core;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Client.Logic.Rendering
{
    public static class GameWindowRenderer
    {
        public static void Render(GameWindow window, Canvas canvas) //Rename:RenderMap???
        {
            var rectangleWidth = canvas.ActualWidth / window.SizeX;
            var rectangleHeight = canvas.ActualHeight / window.SizeY;

            canvas.Children.Clear();

            // this still retarded, why generate empty pixels?? make canvas background color
            var pixels = window.GetCurrentWindow();
            foreach (var pixel in pixels)
            {
                var rec = new Rectangle
                {
                    Width = rectangleWidth,
                    Height = rectangleHeight,
                    Fill = ColorBrushMap[pixel.Color]
                };

                Canvas.SetLeft(rec, rectangleWidth * pixel.X);
                Canvas.SetBottom(rec, rectangleHeight * pixel.Y);


                rec.MouseLeftButtonDown += (s, e) => { window.ClickShape(pixel.X, pixel.Y); };

                canvas.Children.Add(rec);
            }
        }

        public static void RenderPlayer(Player player, Canvas canvas, GameWindow window)
        {
            player.OnLoopFired((WindowPixel[,]) window.GetCurrentWindow().Clone());

            var rectangleWidth = canvas.ActualWidth / window.SizeX;
            var rectangleHeight = canvas.ActualHeight / window.SizeY;

            var playerPixels = player.Render();

            if (canvas.Children.Count == playerPixels.Count)
            {
                var canvasPixels = canvas.Children;

                for (int i = 0; i < playerPixels.Count; i++)
                {
                    canvas.Children[i].SetValue(Canvas.WidthProperty, rectangleWidth);
                    canvas.Children[i].SetValue(Canvas.HeightProperty, rectangleHeight);
                    
                    Canvas.SetLeft(canvas.Children[i], rectangleWidth * playerPixels[i].X);
                    Canvas.SetBottom(canvas.Children[i], rectangleHeight * playerPixels[i].Y);

                    #region Animation rectangle move, need non static method
                    
                    // var lastPositionY = (int) canvasPixels[i].GetValue(Canvas.BottomProperty);
                    // var lastPositionX = (int) canvasPixels[i].GetValue(Canvas.LeftProperty);
                    
                    // var moveAnimX =
                    //     new DoubleAnimation(lastPositionX, playerPixels[i].X, TimeSpan.FromMilliseconds(10));
                    // var moveAnimY =
                    //     new DoubleAnimation(lastPositionY, playerPixels[i].Y, TimeSpan.FromMilliseconds(10));

                    // Rectangle.BeginAnimation(Canvas.LeftProperty, moveAnimX); // Need non static method
                    // Rectangle.BeginAnimation(Canvas.BottomProperty, moveAnimY);

                    #endregion
                }
            }
            else
            {
                canvas.Children.Clear();

                foreach (var windowPixel in playerPixels)
                {
                    var playerPixel = new Rectangle()
                        {Width = rectangleWidth, Height = rectangleHeight, Fill = ColorBrushMap[windowPixel.Color]};

                    Canvas.SetLeft(playerPixel, rectangleWidth * windowPixel.X);
                    Canvas.SetBottom(playerPixel, rectangleHeight * windowPixel.Y);

                    canvas.Children.Add(playerPixel);
                }
            }
        }

        private static Dictionary<Core.Enums.Color, SolidColorBrush> ColorBrushMap =
            new Dictionary<Core.Enums.Color, SolidColorBrush>
            {
                {Core.Enums.Color.Red, Brushes.Red},
                {Core.Enums.Color.Green, Brushes.Green},
                {Core.Enums.Color.Yellow, Brushes.Yellow},
                {Core.Enums.Color.Blue, Brushes.Blue},
                {Core.Enums.Color.Black, Brushes.Black}
            };
    }
}