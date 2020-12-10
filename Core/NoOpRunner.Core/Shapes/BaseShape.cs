using Newtonsoft.Json;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Iterators;
using NoOpRunner.Core.Shapes.GenerationStrategies;
using NoOpRunner.Core.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoOpRunner.Core.Shapes
{
    public abstract class BaseShape : IMapPart
    {
        public int CenterPosX { get; set; }
        public int CenterPosY { get; set; }

        public bool IsStatic { get; set; }

        [JsonProperty]
        protected List<ShapeBlock> ShapeBlocks = new List<ShapeBlock>();
        protected IEnumerable<ShapeBlock> VisibleShapeBlocks => ShapeBlocks.Where(x => x.OffsetX < GameSettings.HorizontalCellCount);

        protected IMapPart Map { get; set; }
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

        public virtual List<List<ShapeBlock>> GetNextBlocks() => throw new NotImplementedException();

        public virtual void ShiftBlocks()
        {
            Logging.Instance.Write($"[Composite/{nameof(BaseShape)}] {nameof(ShiftBlocks)}", LoggingLevel.Composite);

            if (IsStatic)
                return;

            CenterPosX -= 1;

            ShapeBlocks.RemoveAll(x => CenterPosX + x.OffsetX < 0);
        }

        public int getClosestY(int xCoord)
        {
            return VisibleShapeBlocks.Where(x => (x.OffsetX + CenterPosX) == xCoord).Max(x => x.OffsetY) + CenterPosY;
        }

        public (int[], int[]) GetCoords()
        {
            List<int> xCoords = new List<int>();
            List<int> yCoords = new List<int>();

            foreach (var x in VisibleShapeBlocks)
            {
                var absX = CenterPosX + x.OffsetX;
                var absY = CenterPosY + x.OffsetY;

                xCoords.Add(absX);
                yCoords.Add(absY);
            }

            return (xCoords.ToArray(), yCoords.ToArray());
        }
        
        public virtual void OnLoopFired(WindowPixel[,] gameScreen) { }

        public virtual WindowPixelCollection Render()
        {
            Logging.Instance.Write($"[Composite/{nameof(BaseShape)}] {nameof(Render)}", LoggingLevel.Composite);

            var windowPixelCollection = new WindowPixelCollection();

            foreach (var x in VisibleShapeBlocks)
            {
                var absX = CenterPosX + x.OffsetX;
                var absY = CenterPosY + x.OffsetY;

                windowPixelCollection.Add(new WindowPixel(absX, absY, isShape: true));
            }

            return windowPixelCollection;
        }

        public bool IsHit(int x, int y)//meh?
        {
            return ShapeBlocks.Any(s => s.OffsetX + CenterPosX == x && s.OffsetY + CenterPosY == y);
        }

        public virtual void OnClick()//meh?
        {
            // Do nothing by default
        }

        public virtual bool CanOverlap(BaseShape other) => false;
        public virtual bool OnCollision(BaseShape other) => false;

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

        public List<List<ShapeBlock>> GetShapes()
        {
            return new List<List<ShapeBlock>>() { ShapeBlocks };
        }

        public void AddMapPart(IMapPart mapPart)
        {
            Logging.Instance.Write($"[Composite/{nameof(BaseShape)}] {nameof(AddMapPart)}", LoggingLevel.Composite);

            // Do nothing
        }

        public virtual IMapPart GetAtPos(int centerPosX, int centerPosY)
        {
            Logging.Instance.Write($"[Composite/{nameof(BaseShape)}] {nameof(GetAtPos)}", LoggingLevel.Composite);

            return ShapeBlocks.Any(x => x.OffsetX + CenterPosX == centerPosX && x.OffsetY + CenterPosY == centerPosY) ? this : null;
        }

        public void ShiftShapes()
        {
            Logging.Instance.Write($"[Composite/{nameof(BaseShape)}] {nameof(ShiftShapes)}", LoggingLevel.Composite);

            ShiftBlocks();
        }

        public WindowPixel[,] RenderPixels(bool ignoreCollision = false)
        {
            Logging.Instance.Write($"[Composite/{nameof(BaseShape)}] {nameof(RenderPixels)}", LoggingLevel.Composite);

            // Do nothing

            return null;
        }

        public List<T> GetOfType<T>() where T : IMapPart
        {
            Logging.Instance.Write($"[Composite/{nameof(BaseShape)}] {nameof(GetOfType)}", LoggingLevel.Composite);

            return new List<T>();
        }

        public virtual void Accept(INodeVisitor visitor)
        {
            throw new NotImplementedException("Visitor does not know how to handle the provided type");
        }

        public void SetMap(IMapPart map)
        {
            Map = map;
        }
    }
}
