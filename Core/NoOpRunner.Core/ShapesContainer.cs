using Newtonsoft.Json;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoOpRunner.Core
{
    public abstract class ShapesContainer : IMapPart
    {
        [JsonProperty] 
        protected List<IMapPart> Shapes { get; set; } = new List<IMapPart>();

        public int SizeX { get; set; }

        public int SizeY { get; set; }

        public ShapesContainer(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
        }

        public abstract void ShiftShapes();

        public void AddShape(BaseShape shape)
        {
            Shapes.Add(shape);
        }

        public WindowPixel[,] GetShapes(bool ignoreCollision = false)
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
                        if (!ignoreCollision)
                        {
                            throw new Exception("Shape collision occured");
                        }
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

        public void AddMapPart(IMapPart mapPart)
        {
            Logging.Instance.Write($"[Composite/{nameof(ShapesContainer)}] {nameof(AddMapPart)}", LoggingLevel.CompositePattern);

            Shapes.Add(mapPart);
        }

        public bool IsAtPos(int centerPosX, int centerPosY)
        {
            Logging.Instance.Write($"[Composite/{nameof(ShapesContainer)}] {nameof(IsAtPos)}", LoggingLevel.CompositePattern);

            return Shapes.Any(x => x.IsAtPos(centerPosX, centerPosY));
        }

        public List<List<ShapeBlock>> GetNextBlocks()
        {
            Logging.Instance.Write($"[Composite/{nameof(ShapesContainer)}] {nameof(GetNextBlocks)}", LoggingLevel.CompositePattern);

            return Shapes.SelectMany(x => x.GetNextBlocks()).ToList();
        }

        public WindowPixel[,] RenderPixels(bool ignoreCollision = false) 
        {
            Logging.Instance.Write($"[Composite/{nameof(ShapesContainer)}] {nameof(RenderPixels)}", LoggingLevel.CompositePattern);

            return GetShapes(ignoreCollision);
        }

        public List<WindowPixel> Render() 
        {
            Logging.Instance.Write($"[Composite/{nameof(ShapesContainer)}] {nameof(Render)}", LoggingLevel.CompositePattern);

            return GetShapesEnumerable().ToList();
        }

        public List<T> GetOfType<T>() where T : IMapPart
        {
            Logging.Instance.Write($"[Composite/{nameof(ShapesContainer)}] {nameof(GetOfType)}", LoggingLevel.CompositePattern);

            return Shapes.OfType<T>().ToList();
        }
    }
}