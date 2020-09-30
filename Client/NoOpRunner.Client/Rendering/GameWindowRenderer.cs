using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NoOpRunner.Core;
using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Client.Rendering
{
    public static class GameWindowRenderer
    {
        public static void RenderMap(GamePlatforms platforms, Canvas canvas)
        {
            var rectangleWidth = canvas.ActualWidth / platforms.SizeX;
            var rectangleHeight = canvas.ActualHeight / platforms.SizeY;


            var pixels = platforms.GetCurrentMapEnumerable().ToList();
            if (canvas.Children.Count != pixels.Count)
            {
                canvas.Children.Clear();

                var imageBrush = new ImageBrush(new BitmapImage(SpritesUriHandler.GetPlatformUri()));

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
                    canvas.Children[i].SetValue(Canvas.WidthProperty, rectangleWidth);
                    canvas.Children[i].SetValue(Canvas.HeightProperty, rectangleHeight);

                    Canvas.SetLeft(canvas.Children[i], rectangleWidth * pixels[i].X);
                    Canvas.SetBottom(canvas.Children[i], rectangleHeight * pixels[i].Y);
                }
            }
        }

        public static void RenderPlayer(Player player, Canvas canvas, GamePlatforms platforms)
        {
            var rectangleWidth = canvas.ActualWidth / platforms.SizeX;
            var rectangleHeight = canvas.ActualHeight / platforms.SizeY;

            var playerPixels = player.Render();

            if (canvas.Children.Count == playerPixels.Count)
            {
                for (int i = 0; i < playerPixels.Count; i++)
                {
                    if (player.StateHasChanged)
                    {
                        canvas.Children[i].SetValue(GifImage.GifSourceProperty,
                            player.GetStateAnimationUri);
                    }

                    canvas.Children[i].SetValue(Canvas.WidthProperty, rectangleWidth);
                    canvas.Children[i].SetValue(Canvas.HeightProperty, rectangleHeight);

                    if (player.IsPlayerTurning)
                    {
                        canvas.Children[i].SetValue(UIElement.RenderTransformProperty,
                            player.IsLookingLeft
                                ? new ScaleTransform() {ScaleX = -1, CenterX = rectangleWidth/2}
                                : new ScaleTransform() {ScaleX = 1, CenterX = rectangleWidth/2});
                    }

                    SetAnimation(canvas.Children[i], rectangleWidth * playerPixels[i].X,
                        rectangleHeight * playerPixels[i].Y);
                    

                }
            }
            else
            {
                canvas.Children.Clear();

                foreach (var windowPixel in playerPixels)
                {
                    var playerPixel = new GifImage()
                    {
                        Width = rectangleWidth, Height = rectangleHeight,
                        GifSource = player.GetStateAnimationUri
                    };
                    
                    playerPixel.Stretch = Stretch.Fill;
                    
                    Canvas.SetLeft(playerPixel, rectangleWidth * windowPixel.X);
                    Canvas.SetBottom(playerPixel, rectangleHeight * windowPixel.Y);

                    canvas.Children.Add(playerPixel);

                    playerPixel.StartAnimation();
                }
            }
        }

        private static void SetAnimation(UIElement uiElement, double newPosX, double newPosY)
        {
            var lastPositionY = (double) uiElement.GetValue(Canvas.BottomProperty);
            var lastPositionX = (double) uiElement.GetValue(Canvas.LeftProperty);

            //Animation duration(time object slide from point A to B)
            var animationDuration = TimeSpan.FromMilliseconds(1000 / 20);

            var moveAnimX =
                new DoubleAnimation(lastPositionX, newPosX, animationDuration);
            var moveAnimY =
                new DoubleAnimation(lastPositionY, newPosY, animationDuration);

            uiElement.BeginAnimation(Canvas.LeftProperty, moveAnimX);
            uiElement.BeginAnimation(Canvas.BottomProperty, moveAnimY);
        }

        /// <summary>
        /// For new shapes, while sprites not found online
        /// </summary>
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