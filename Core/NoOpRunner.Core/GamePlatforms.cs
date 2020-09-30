﻿using NoOpRunner.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoOpRunner.Core
{
    public class GamePlatforms
    {
        private List<BaseShape> shapes { get; set; }

        public IReadOnlyCollection<BaseShape> Shapes => shapes.AsReadOnly();

        public readonly int SizeX;

        public readonly int SizeY;

        public GamePlatforms(int sizeX, int sizeY)
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

        public void ClickShape(int x, int y)
        {
            shapes.FirstOrDefault(s => s.IsHit(x, y))?.OnClick();
        }

        public WindowPixel[,] GetCurrentMap()
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

        public IEnumerable<WindowPixel> GetCurrentMapEnumerable()
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