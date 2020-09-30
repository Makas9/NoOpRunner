using NoOpRunner.Core.Entities;

namespace NoOpRunner.Core.Shapes
{
    public abstract class GeometricShape : BaseShape
    {
        public GeometricShape(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {

        }
    }
}
