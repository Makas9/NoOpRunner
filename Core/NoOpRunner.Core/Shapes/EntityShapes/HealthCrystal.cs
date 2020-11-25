using System;
using NoOpRunner.Core.Shapes.GenerationStrategies;

namespace NoOpRunner.Core.Shapes.EntityShapes
{
    public class HealthCrystal : EntityShape
    {
        public HealthCrystal(int x, int y) : base(new FillGenerationStrategy(), x, y, x + 1, y + 2) { }

        public override bool CanOverlap(BaseShape other)
        {
            throw new NotImplementedException();
        }

        public override void OnCollision(BaseShape other)
        {
            if (other is Player p)
            {
                p.ModifyHealth(1);
            }
        }
    }
}
