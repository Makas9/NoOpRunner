using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Core
{
    public class PlatformsContainer : ShapesContainer, IObserver, IVisualElement
    {
        public PlatformsContainer(int sizeX, int sizeY) : base(sizeX, sizeY)
        {
        }
        
        /// <summary>
        /// For platforms update, append generated cells
        /// </summary>
        /// <param name="message"></param>
        public void Update(MessageDto message)
        {
            if (message.MessageType != MessageType.PlatformsUpdate)
                return;

            Console.WriteLine("Observer: PlatformsContainer, say Hello World");
            
            var platformsColumn = message.Payload as IList<IList<ShapeBlock>>;

            shapes.ForEach(x =>
                ((StaticShape) x).PushAndRemove()
            );//Push and remove out of bounds

            for (int i = 0; i < shapes.Count; i++)
            {
                ((StaticShape) shapes[i]).AppendPlatform(platformsColumn[i]);//Append platforms with generated cells
            }
            
        }

        public void Display(Canvas canvas)
        {
            var rectangleWidth = canvas.ActualWidth / this.SizeX;
            var rectangleHeight = canvas.ActualHeight / this.SizeY;

            var pixels = GetShapesEnumerable().ToList();
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