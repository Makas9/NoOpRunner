using NoOpRunner.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoOpRunner.Core
{
    public class GameWindow//GameMap ??? 
    {
        private List<BaseShape> shapes { get; set; }

        public IReadOnlyCollection<BaseShape> Shapes => shapes.AsReadOnly();

        public readonly int SizeX;

        public readonly int SizeY;

        public GameWindow(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;

            shapes = new List<BaseShape>();
        }

        public void AddShape(BaseShape shape)
        {
            shapes.Add(shape);
        }

        public void OnLoopFired(WindowPixel[,] gameScreen)
        {
            shapes.ForEach(x => x.OnLoopFired(gameScreen));
        }

        public void ClickShape(int x, int y)
        {
            shapes.FirstOrDefault(s => s.IsHit(x, y))?.OnClick();
        }

        public WindowPixel[,] GetCurrentWindow()// Could be generator and list
        {
            var windowPixels = new WindowPixel[SizeX, SizeY];

            foreach (var shape in Shapes)
            {
                var shapePixels = shape.Render();

                foreach (var pixel in shapePixels)
                {
                    if (pixel.X < 0 ||
                        pixel.Y < 0 ||
                        pixel.X >= SizeX ||
                        pixel.Y >= SizeY)
                    {
                        throw new Exception("Shape pixel outside the bounds of the game window");
                    }
                    else if (windowPixels[pixel.X, pixel.Y] == default)
                    {
                        windowPixels[pixel.X, pixel.Y] = pixel;
                    }
                    else
                    {
                        throw new Exception("Shape collision occured");
                    }
                }
            }

            // save space and time that give speed or breakup
            // for (int i = 0; i <= windowPixels.GetUpperBound(0); i++) 
            // {
            //     for (int j = 0; j <= windowPixels.GetUpperBound(1); j++)
            //     {
            //         if (windowPixels[i, j] == default)
            //         {
            //             windowPixels[i, j] = new WindowPixel(i, j, Enums.Color.Black, false);
            //         }
            //     }
            // }

            return windowPixels;
        }

        public IEnumerable<WindowPixel> GetCurrentWindowEnumerable()//generator no?
        {
            foreach (var shape in Shapes)
            {
                var shapePixels = shape.Render();

                foreach (var pixel in shapePixels)
                {
                    yield return pixel;
                }
            }
        }
    }
}