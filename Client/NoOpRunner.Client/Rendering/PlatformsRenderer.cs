using NoOpRunner.Core;
using NoOpRunner.Core.Interfaces;
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
    public class PlatformsRenderer : BaseRenderer
    {
        private ImageBrush PlatformsSprite { get; set; }
        public PlatformsRenderer(ShapesContainer shapesContainer) : base(shapesContainer)
        {
        }

        public override void RenderContainerElements(Canvas canvas, out int canvasChildIndex, out int canvasChildCount)
        {
            canvasChildIndex = 0;
            
            canvasChildCount = canvas.Children.Count;

            var rectangleWidth = canvas.ActualWidth / ShapesContainer.SizeX;
            var rectangleHeight = canvas.ActualHeight / ShapesContainer.SizeY;
            
            foreach (var pixel in ShapesContainer.Render().GetItems())
            {
                if (canvasChildIndex >= canvasChildCount)
                {
                    Rectangle rec;
                    
                    if (!DisposedObjectsPool.Contains<Rectangle>())
                    {
                        if (PlatformsSprite == null)
                        
                            PlatformsSprite = new ImageBrush(new BitmapImage(ResourcesUriHandler.GetPlatformUri()));
                        
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
                    
                    rec.SetValue(Shape.FillProperty, PlatformsSprite);
                    
                    Canvas.SetLeft(rec, rectangleWidth * pixel.X);
                    Canvas.SetBottom(rec, rectangleHeight * pixel.Y);
                    
                    canvas.Children.Add(rec);
                }
                else
                {
                    Canvas.SetLeft(canvas.Children[canvasChildIndex], rectangleWidth * pixel.X);
                    Canvas.SetBottom(canvas.Children[canvasChildIndex], rectangleHeight * pixel.Y);
                    
                    canvasChildIndex++;
                }
            }
        }
    }
}