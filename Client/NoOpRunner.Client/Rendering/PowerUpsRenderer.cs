using NoOpRunner.Core;
using NoOpRunner.Core.Interfaces;
using System;
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
    public class PowerUpsRenderer : BaseRenderer
    {
        public PowerUpsRenderer(PowerUpsContainer shapesContainer) : base(shapesContainer)
        {
        }

        public override void RenderContainerElements(Canvas canvas, out int canvasChildIndex, out int canvasChildCount)
        {
            canvasChildIndex = 0;
            canvasChildCount = canvas.Children.Count;

            var rectangleWidth = canvas.ActualWidth / ShapesContainer.SizeX;
            var rectangleHeight = canvas.ActualHeight / ShapesContainer.SizeY;


            var pixels = ((PowerUpsContainer) ShapesContainer).GetPowerUpsEnumerable();

            foreach (var (windowPixel, powerUp) in pixels)
            {
                if (canvasChildIndex >= canvasChildCount)
                {
                    Rectangle rec;
                    
                    if (!DisposedObjectsPool.Contains<Rectangle>())
                    {
                        rec = new Rectangle
                        {
                            Width = rectangleWidth,
                            Height = rectangleHeight
                        };
                    }
                    else
                    {
                        rec = DisposedObjectsPool.Pop<Rectangle>();

                        rec.SetValue(FrameworkElement.WidthProperty, rectangleWidth);
                        rec.SetValue(FrameworkElement.HeightProperty, rectangleHeight);
                    }

                    rec.SetValue(Shape.FillProperty,
                        new ImageBrush(new BitmapImage(ResourcesUriHandler.GetPowerUp(powerUp))));
                    
                    Canvas.SetLeft(rec, rectangleWidth * windowPixel.X);
                    Canvas.SetBottom(rec, rectangleHeight * windowPixel.Y);
                    
                    canvas.Children.Add(rec);
                }
                else
                {
                    Canvas.SetLeft(canvas.Children[canvasChildIndex], 
                        rectangleWidth * pixels[canvasChildIndex].Item1.X);
                    
                    Canvas.SetBottom(canvas.Children[canvasChildIndex], 
                        rectangleHeight * pixels[canvasChildIndex].Item1.Y);
                    
                    canvas.Children[canvasChildIndex].SetValue(Shape.FillProperty,
                        new ImageBrush(new BitmapImage(ResourcesUriHandler.GetPowerUp(powerUp))));
                    
                    canvasChildIndex++;
                }
            }
        }
    }
}