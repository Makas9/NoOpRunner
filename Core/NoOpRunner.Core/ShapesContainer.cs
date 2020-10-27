using Newtonsoft.Json;
using NoOpRunner.Core.Shapes;
using System;
using System.Collections.Generic;

namespace NoOpRunner.Core
{
    public abstract class ShapesContainer
    {
        [JsonProperty] 
        protected List<BaseShape> Shapes { get; set; }

        public int SizeX { get; set; }

        public int SizeY { get; set; }

        public ShapesContainer(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;

            Shapes = new List<BaseShape>();
        }

        public abstract void ShiftShapes();

        public void AddShape(BaseShape shape)
        {
            Shapes.Add(shape);
        }

        public WindowPixel[,] GetShapes()
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

            return windowPixels;
        }

        public IEnumerable<WindowPixel> GetShapesEnumerable()
        {
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
                    yield return pixel;
                }
            }
        }
    }
}