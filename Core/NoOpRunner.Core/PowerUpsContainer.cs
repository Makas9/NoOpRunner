using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;
using Color = System.Windows.Media.Color;

namespace NoOpRunner.Core
{
    /// <summary>
    /// Power ups 
    /// </summary>
    public class PowerUpsContainer : ShapesContainer, IObserver, IVisualElement
    {
        public PowerUpsContainer(int sizeX, int sizeY) : base(sizeX, sizeY)
        {
        }

        public void RemovePowerUp(int centerPosX, int centerPosY)
        {
            shapes.Remove(GetPowerUpAt(centerPosX, centerPosY));
        }
        public PowerUp GetPowerUpAt(int centerPosX, int centerPosY)
        {
            return shapes.FirstOrDefault(x => x.CenterPosX == centerPosX && x.CenterPosY == centerPosY) as PowerUp;
        }
        public void ClickShape(int x, int y)
        {
            shapes.FirstOrDefault(s => s.IsHit(x, y))?.OnClick();
        }

        /// <summary>
        /// Push power ups, remove out of bounds and add generated
        /// </summary>
        /// <param name="message"></param>
        public void Update(MessageDto message)
        {
            if (message.MessageType != MessageType.PowerUpsUpdate) 
                return;
            
            Console.WriteLine("Observer: PowerUpsContainer, say Hello World");
            
            shapes.ForEach(x => x.CenterPosX--); //Push cells

            //Will need in the future, update player two instance
            var pickedUpPowerUps = shapes.Where(x => x.CenterPosX < 0);
                
            shapes.RemoveAll(x => x.CenterPosX < 0); //Remove out of bounds

            shapes.AddRange(message.Payload as List<BaseShape>); //Append generated power ups
        }

        public void Display(Canvas canvas)
        {
            var rectangleWidth = canvas.ActualWidth / this.SizeX;
            var rectangleHeight = canvas.ActualHeight / this.SizeY;


            var pixels = GetShapesEnumerable().ToList();
            
            if (canvas.Children.Count != pixels.Count)
            {
                canvas.Children.Clear();

                foreach (PowerUp powerUp in shapes)
                {
                    var rec = new Rectangle
                    {
                        Width = rectangleWidth,
                        Height = rectangleHeight,
                        Fill = new ImageBrush(new BitmapImage(ResourcesUriHandler.GetPowerUp(powerUp.PowerUpType))),
                        Stretch = Stretch.Fill
                    };
                    Canvas.SetLeft(rec, rectangleWidth * powerUp.CenterPosX);
                    Canvas.SetBottom(rec, rectangleHeight * powerUp.CenterPosY);

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
    }
}