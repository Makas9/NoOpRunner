using NoOpRunner.Core.Entities;

namespace NoOpRunner.Core.Shapes
{
    public class MovingShape : BaseShape
    {
        public MovingShape(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {

        }

        public decimal VerticalAcceleration = 0m;
        public decimal HorizontalAcceleration = 0m;

        public int VerticalSpeed = 0;
        public int HorizontalSpeed = 0;

        public override void OnLoopFired(WindowPixel[,] gameScreen)
        {
            var oldCenterPosX = CenterPosX;
            var oldCenterPosY = CenterPosY;
            CenterPosX += (int)(HorizontalSpeed + HorizontalSpeed * HorizontalAcceleration);
            CenterPosY += (int)(VerticalSpeed + VerticalSpeed * VerticalAcceleration);

            var shapePixels = this.Render();
            foreach (var pixel in shapePixels)
            {
                if (IsShapeHit(gameScreen, pixel.X, pixel.Y))
                {
                    // TODO: Add a way for different shapes to handle collisions differently
                    CenterPosX = oldCenterPosX;
                    CenterPosY = oldCenterPosY;
                    return;
                }
            }
        }

        protected bool IsShapeHit(WindowPixel[,] gameScreen, int x, int y)
        {
            return x > gameScreen.GetUpperBound(0) ||
                y > gameScreen.GetUpperBound(1) ||
                x < 0 ||
                y < 0 ||
                (gameScreen[x, y] != default && gameScreen[x, y].IsShape);
        }

        public override bool CanOverlap(BaseShape other)
        {
            return false; // Not Implemented Yet
        }

        public override void OnCollision(BaseShape other)
        {
            // Not Implemented Yet
        }
    }
}
