using Newtonsoft.Json;
using NoOpRunner.Core.Shapes;
using System;
using System.Collections.Generic;

namespace NoOpRunner.Core
{
    public class ShapesContainer
    {
        [JsonProperty] 
        protected List<BaseShape> shapes { get; set; }

        public int SizeX { get; set; }

        public int SizeY { get; set; }

        public ShapesContainer(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;

            shapes = new List<BaseShape>();
        }

        public virtual void MoveWithMap()
        {
            throw new NotImplementedException();
        }

        public void AddShape(BaseShape shape)
        {
            shapes.Add(shape);
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