using Newtonsoft.Json;
using NoOpRunner.Core.Iterators;
using NoOpRunner.Core.Shapes;
using System;
using System.Collections.Generic;

namespace NoOpRunner.Core
{
    public abstract class ShapesContainer
    {
        [JsonProperty]
        protected ShapeCollection Shapes { get; set; }

        public int SizeX { get; set; }

        public int SizeY { get; set; }

        public ShapesContainer(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;

            Shapes = new ShapeCollection();
            Shapes.Reverse(); // Start from beginning
        }

        public abstract void ShiftShapes();

        public void AddShape(BaseShape shape)
        {
            Shapes.Add(shape);
        }

        public WindowPixel[,] GetShapes(bool ignoreCollision = false)
        {
            var windowPixels = new WindowPixel[SizeX, SizeY];

            foreach (BaseShape shape in Shapes)
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
                        if (!ignoreCollision)
                        {
                            throw new Exception("Shape collision occured");
                        }
                    }
                }
            }

            return windowPixels;
        }

        public WindowPixelCollection GetWindowsPixelCollection()
        {
            var pixels = new WindowPixelCollection();
            foreach (BaseShape shape in Shapes)
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

                    pixels.Add(pixel);
                }
            }

            return pixels;
        }
    }
}