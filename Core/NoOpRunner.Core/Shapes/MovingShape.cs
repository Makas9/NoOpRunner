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
            CenterPosX += (int)(HorizontalSpeed + HorizontalSpeed * HorizontalAcceleration);
            CenterPosY += (int)(VerticalSpeed + VerticalSpeed * VerticalAcceleration);
        }
    }
}
