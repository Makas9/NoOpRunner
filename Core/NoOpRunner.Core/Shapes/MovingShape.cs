using System;
using NoOpRunner.Core.Shapes.GenerationStrategies;
using NoOpRunner.Core.Visitors;

namespace NoOpRunner.Core.Shapes
{
    public abstract class MovingShape : BaseShape
    {
        public MovingShape(int x, int y) : base(new FillGenerationStrategy(), x, y, x + 1, y + 1) { }

        public MovingShape(GenerationStrategy strategy, int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY)
            : base(strategy, lowerBoundX, lowerBoundY, upperBoundX, upperBoundY) { }

        public decimal VerticalAcceleration = 0m;
        public decimal HorizontalAcceleration = 0m;

        public int VerticalSpeed = 0;
        public int HorizontalSpeed = 0;

        public int VerticalSpeedLimit = 1;
        public int HorizontalSpeedLimit = 1;

        protected int HorizontalSpeedModifier = 0;
        protected int HorizontalSpeedModifierRate = 1;

        public override void OnLoopFired(WindowPixel[,] gameScreen)
        {
            var oldCenterPosX = CenterPosX;
            var oldCenterPosY = CenterPosY;
            HorizontalSpeed = Math.Min((int)(HorizontalSpeed + HorizontalAcceleration), HorizontalSpeedLimit) + HorizontalSpeedModifier;
            VerticalSpeed = Math.Min((int)(VerticalSpeed + VerticalAcceleration), VerticalSpeedLimit);

            if (HorizontalSpeedModifier >= HorizontalSpeedModifierRate)
                HorizontalSpeedModifier -= HorizontalSpeedModifierRate;
            else if (HorizontalSpeedModifier < 0)
                HorizontalSpeedModifier += HorizontalSpeedModifierRate;

            CenterPosX += HorizontalSpeed;
            CenterPosY += VerticalSpeed;

            var shapePixels = this.Render();
            foreach (WindowPixel pixel in shapePixels)
            {
                if (!IsShapeHit(gameScreen, pixel.X, pixel.Y)) 
                    continue;
                
                // TODO: Add a way for different shapes to handle collisions differently
                CenterPosX = oldCenterPosX;
                CenterPosY = oldCenterPosY;
                return;
            }
        }

        protected bool IsShapeHit(WindowPixel[,] gameScreen, int x, int y)
        {
            var shape = Map?.GetAtPos(x, y);

            if (OnCollision(shape as BaseShape))
            {
                return false;
            }

            return x > gameScreen.GetUpperBound(0) ||
                y > gameScreen.GetUpperBound(1) ||
                x < 0 ||
                y < 0 ||
                shape != null;
        }

        public override void Accept(INodeVisitor visitor)
        {
            visitor.VisitMovingShape(this);
        }
    }
}
