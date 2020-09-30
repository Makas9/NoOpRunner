using NoOpRunner.Core.Entities;
using System.Diagnostics;

namespace NoOpRunner.Core.Shapes
{
    public abstract class EntityShape : BaseShape
    {
        public EntityShape(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {

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
