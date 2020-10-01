using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes
{
    public class PassablePlatform : StaticShape
    {

        public PassablePlatform(int centerPosX, int centerPosY, int bottomPosY, int topPosY) : base(centerPosX, centerPosY)
        {

        }
        public override bool CanOverlap(BaseShape other)
        {
            return true;
        }

        public override void OnCollision(BaseShape other)
        {
            // Do nothing
        }
    }
}
