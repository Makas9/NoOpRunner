namespace NoOpRunner.Core.Entities
{
    public class PassablePlatform : StaticShape
    {

        public PassablePlatform(int centerPosX, int centerPosY, int bottomPosY, int topPosY) : base(centerPosX, centerPosY, bottomPosY, topPosY)
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
