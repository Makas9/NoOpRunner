using NoOpRunner.Core.Entities;

namespace NoOpRunner.Core.Shapes
{
    public abstract class StaticShape : BaseShape
    {
        public StaticShape(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {

        }
    }
}
