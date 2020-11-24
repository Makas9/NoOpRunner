using Newtonsoft.Json;
using NoOpRunner.Core.Shapes.GenerationStrategies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoOpRunner.Core.Shapes
{
    public abstract class BaseShape
    {
        public int CenterPosX { get; set; }
        public int CenterPosY { get; set; }

        [JsonProperty]
        protected List<ShapeBlock> ShapeBlocks = new List<ShapeBlock>();
        protected IEnumerable<ShapeBlock> VisibleShapeBlocks => ShapeBlocks.Where(x => x.OffsetX < GameSettings.HorizontalCellCount);

        protected GenerationStrategy Strategy { get; set; }
        protected int lowerBoundY;
        protected int upperBoundY;
        
        private WindowPixel FlyweightPixel { get; set; }

        protected BaseShape() { } // Needed for JSON deserialization

        /// <summary>
        /// Generate a shape somewhere in the given region using the provided strategy.
        /// CenterPos will be set to the position of the first generated ShapeBlock
        /// </summary>
        public BaseShape(GenerationStrategy strategy, int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY)
        {
            var blocks = strategy.GenerateShapeBlocks(lowerBoundX, lowerBoundY, upperBoundX, upperBoundY);
            CenterPosX = blocks[0].OffsetX;
            CenterPosY = blocks[0].OffsetY;
            this.lowerBoundY = lowerBoundY;
            this.upperBoundY = upperBoundY;
            ShapeBlocks = GenerationStrategy.MakeRelative(blocks, CenterPosX, CenterPosY);
            Strategy = strategy;
        }

        public virtual List<ShapeBlock> GetNextBlocks() => throw new NotImplementedException();

        public virtual void ShiftBlocks()
        {
            CenterPosX -= 1;

            ShapeBlocks.RemoveAll(x => CenterPosX + x.OffsetX < 0);
        }

        public int getClosestY(int xCoord)
        {
            return VisibleShapeBlocks.Where(x => (x.OffsetX + CenterPosX) == xCoord).Max(x => x.OffsetY) + CenterPosY;
        }

        public virtual void OnLoopFired(WindowPixel[,] gameScreen) { }

        public virtual IEnumerable<WindowPixel> Render()
        {
            if (FlyweightPixel == null)
            {
                FlyweightPixel = new WindowPixel(0, 0, true);
            }
            
            foreach (var x in VisibleShapeBlocks)
            {
                var absX = CenterPosX + x.OffsetX;
                var absY = CenterPosY + x.OffsetY;

                FlyweightPixel.X = absX;
                FlyweightPixel.Y = absY;
                
                yield return FlyweightPixel;
            }
        }

        public bool IsHit(int x, int y)//meh?
        {
            return ShapeBlocks.Any(s => s.OffsetX + CenterPosX == x && s.OffsetY + CenterPosY == y);
        }

        public virtual void OnClick()//meh?
        {
            // Do nothing by default
        }

        public abstract bool CanOverlap(BaseShape other);
        public abstract void OnCollision(BaseShape other);

        public BaseShape Clone()
        {
            return (BaseShape)this.MemberwiseClone();
        }
        public BaseShape DeepCopy()
        {
            BaseShape clone = (BaseShape)this.MemberwiseClone();
            clone.ShapeBlocks = ShapeBlocks.Select(x => new ShapeBlock() { OffsetX = x.OffsetX, OffsetY = x.OffsetY }).ToList();

            return clone;
        }

        public List<ShapeBlock> GetShapes()
        {
            return ShapeBlocks;
        }
    }
}
