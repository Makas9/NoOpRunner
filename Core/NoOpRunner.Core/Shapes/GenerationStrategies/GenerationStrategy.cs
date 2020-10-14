using System.Collections.Generic;
using System.Linq;

namespace NoOpRunner.Core.Shapes.GenerationStrategies
{
    public abstract class GenerationStrategy
    {
        /// <summary>
        /// Generate some shape from ShapeBlocks in the given region.
        /// The lower boundaries are inclusive, upper - exclusive
        /// </summary>
        /// <returns>A list of ShapeBlocks making up the shape, with coords offset from (0, 0) (This is to allow easily combining
        /// the results of different generation strategies) </returns>
        public abstract List<ShapeBlock> GenerateShapeBlocks(int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY, int? startY = null);

        public static void AddShapeBlock(List<ShapeBlock> blocks, int offsetX, int offsetY)
        {
            blocks.Add(new ShapeBlock
            {
                OffsetX = offsetX,
                OffsetY = offsetY
            });
        }

        public static void MapShapeX(List<ShapeBlock> blocks, int offsetX, int offsetY, int length)
        {
            for (int i = 0; i < length; i++)
            {
                AddShapeBlock(blocks, offsetX + i, offsetY);
            }
        }

        public static void MapShapeY(List<ShapeBlock> blocks, int offsetX, int offsetY, int length)
        {
            for (int i = 0; i < length; i++)
            {
                AddShapeBlock(blocks, offsetX, offsetY + i);
            }
        }

        public static List<ShapeBlock> MakeRelative(List<ShapeBlock> blocks, int centerX, int centerY) 
            => blocks.Select(x => new ShapeBlock() { OffsetX = x.OffsetX - centerX, OffsetY = x.OffsetY - centerY }).ToList();
    }
}
