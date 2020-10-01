using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes
{
    public class ImpassablePlatform : StaticShape
    {

        public ImpassablePlatform(int centerPosX, int centerPosY, int bottomPosY, int topPosY) : base(centerPosX, centerPosY)
        {

        }

        public override bool CanOverlap(BaseShape other)
        {
            return false;
        }

        public override void OnCollision(BaseShape other)
        {
            // Do nothing
        }
    }
}
