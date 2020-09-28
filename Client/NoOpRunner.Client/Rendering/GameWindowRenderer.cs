using System;
using NoOpRunner.Core;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Client.Logic.Rendering
{
    public static class GameWindowRenderer
    {
        // private static AnimationRenderer _animationRenderer;
        private static UIElement lastElement;

        public static void Render(GameWindow window, Canvas canvas) //Rename:RenderMap???
        {
            var rectangleWidth = canvas.ActualWidth / window.SizeX;
            var rectangleHeight = canvas.ActualHeight / window.SizeY;


            var pixels = window.GetCurrentWindowEnumerable().ToList();
            if (canvas.Children.Count != pixels.Count)
            {
                canvas.Children.Clear();

                var imageBrush = InitImageBrush(SpritesUriHandler.GetPlatformUri());

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

            #region Old code

// this still retarded, why generate empty pixels?? make canvas background color
            // var pixels = window.GetCurrentWindow();
            // canvas.Children.Clear();
            // foreach (var pixel in pixels)
            // {
            //     
            //     var rec = new Rectangle
            //     {
            //         Width = rectangleWidth,
            //         Height = rectangleHeight,
            //         Fill = ColorBrushMap[pixel.Color]
            //     };
            //
            //     Canvas.SetLeft(rec, rectangleWidth * pixel.X);
            //     Canvas.SetBottom(rec, rectangleHeight * pixel.Y);
            //
            //
            //     rec.MouseLeftButtonDown += (s, e) => { window.ClickShape(pixel.X, pixel.Y); };
            //
            //     canvas.Children.Add(rec);
            // }

            #endregion
        }

        public static void RenderPlayer(Player player, Canvas canvas, GameWindow window)
        {
            //TODO: player size, adjust animation speed for better UX 
            var rectangleWidth = canvas.ActualWidth / window.SizeX;
            var rectangleHeight = canvas.ActualHeight / window.SizeY;

            var playerPixels = player.Render();

            if (canvas.Children.Count == playerPixels.Count)
            {
                for (int i = 0; i < playerPixels.Count; i++)
                {
                    
                    if (player.StateMachine.CompareStates)
                    {
                        canvas.Children[i].SetValue(GifImage.GifSourceProperty,
                            player.StateMachine.GetStatusUri());
                        
                        canvas.Children[i].SetValue(Canvas.WidthProperty, rectangleWidth);
                        canvas.Children[i].SetValue(Canvas.HeightProperty, rectangleHeight);
                    }

                    if (player.StateMachine.IsTurning)
                    {
                        canvas.Children[i].SetValue(UIElement.RenderTransformProperty,
                            player.StateMachine.IsTurnedLeft
                                ? new ScaleTransform() {ScaleX = -1}
                                : new ScaleTransform() {ScaleX = 1});
                    }

                    #region Animation rectangle move, need non static method

                    SetAnimation(canvas.Children[i], rectangleWidth * playerPixels[i].X,
                        rectangleHeight * playerPixels[i].Y);

                    #endregion
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
                        GifSource = player.StateMachine.GetStatusUri()
                    };

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

            var moveAnimX =
                new DoubleAnimation(lastPositionX, newPosX, TimeSpan.FromMilliseconds(1000/20));
            var moveAnimY =
                new DoubleAnimation(lastPositionY, newPosY, TimeSpan.FromMilliseconds(1000/20));

            uiElement.BeginAnimation(Canvas.LeftProperty, moveAnimX);
            uiElement.BeginAnimation(Canvas.BottomProperty, moveAnimY);
        }
        private static ImageBrush InitImageBrush(Uri uri)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = uri;
            bitmapImage.EndInit();

            return new ImageBrush(bitmapImage);
        }

        // private static BitmapImage InitBitmapImage(Uri uri)
        // {
        //     var bitmapImage = new BitmapImage();
        //     bitmapImage.BeginInit();
        //     bitmapImage.UriSource = uri;
        //     bitmapImage.EndInit();
        //
        //     return bitmapImage;
        // }

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