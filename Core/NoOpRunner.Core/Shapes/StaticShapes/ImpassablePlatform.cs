namespace NoOpRunner.Core.Entities
{
    public class ImpassablePlatform : StaticShape
    {

        public ImpassablePlatform(int centerPosX, int centerPosY, int bottomPosY, int topPosY) : base(centerPosX, centerPosY, bottomPosY, topPosY)
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
