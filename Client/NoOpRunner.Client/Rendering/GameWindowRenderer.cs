using NoOpRunner.Core;
using NoOpRunner.Core.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NoOpRunner.Client.Rendering
{
    public static class GameWindowRenderer
    {
        public static void RenderMap(GameMap platforms, Canvas canvas)
        {
            var rectangleWidth = canvas.ActualWidth / platforms.SizeX;
            var rectangleHeight = canvas.ActualHeight / platforms.SizeY;

            /* Use this code to paint different shapes in different colors
            foreach (var shape in platforms.Shapes) {
                var type = shape.GetType();
            }
            */

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

        public static void RenderPlayer(Player player, Canvas canvas, GameMap platforms)
        {
            var rectangleWidth = canvas.ActualWidth / platforms.SizeX;
            var rectangleHeight = canvas.ActualHeight / platforms.SizeY;

            //first cell for display, other for hit box, need property for player hit box width and height
            var playerPixel = player.GetAnimationPixel(out int hitBoxY, out int hitBoxX);

            if (canvas.Children.Count != 0)
            {
                if (player.StateHasChanged)
                {
                    canvas.Children[0].SetValue(GifImage.GifSourceProperty,
                        player.GetStateAnimationUri);
                }

                canvas.Children[0].SetValue(Canvas.WidthProperty, rectangleWidth * hitBoxX); // sketch for all hit box
                canvas.Children[0].SetValue(Canvas.HeightProperty, rectangleHeight * hitBoxY); // sketch for all hit box

                if (player.IsPlayerTurning)
                {
                    canvas.Children[0].SetValue(UIElement.RenderTransformProperty,
                        player.IsLookingLeft
                            ? new ScaleTransform() {ScaleX = -1, CenterX = rectangleWidth / 2}
                            : new ScaleTransform() {ScaleX = 1, CenterX = rectangleWidth / 2});
                }

                SetAnimation(canvas.Children[0], rectangleWidth * playerPixel.X,
                    rectangleHeight * playerPixel.Y);
            }
            else
            {
                canvas.Children.Clear();

                var playerPixelImage = new GifImage()
                {
                    Width = rectangleWidth * hitBoxX, Height = rectangleHeight * hitBoxY,
                    GifSource = player.GetStateAnimationUri
                };

                playerPixelImage.Stretch = Stretch.Fill;

                Canvas.SetLeft(playerPixelImage, rectangleWidth * playerPixel.X * hitBoxX);
                Canvas.SetBottom(playerPixelImage, rectangleHeight * playerPixel.Y * hitBoxY);

                canvas.Children.Add(playerPixelImage);

                playerPixelImage.StartAnimation();
                
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