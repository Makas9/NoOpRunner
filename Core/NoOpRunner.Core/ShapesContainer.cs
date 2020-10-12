using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Core
{
    public class ShapesContainer
    {
        [JsonProperty] protected List<BaseShape> shapes { get; set; }

        public readonly int SizeX;//Just take from GameSettings class

        public readonly int SizeY;

        public ShapesContainer(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;

            shapes = new List<BaseShape>();
        }

        public void AddShape(BaseShape shape)
        {
            shapes.Add(shape);
        }

        public void OnLoopFired(WindowPixel[,] gameMap)
        {
            shapes.ForEach(x => x.OnLoopFired(gameMap));
        }

        public WindowPixel[,] GetShapes()
        {
            var windowPixels = new WindowPixel[SizeX, SizeY];

            foreach (var shape in shapes)
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
                        // TODO: YES, YES, YES, YES
                        throw new Exception("Shape collision occured");
                    }
                }
            }

            return windowPixels;
        }

        public IEnumerable<WindowPixel> GetShapesEnumerable()
        {
            foreach (var shape in shapes)
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