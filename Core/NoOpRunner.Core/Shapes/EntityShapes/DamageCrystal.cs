using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Shapes.GenerationStrategies;
using System;

namespace NoOpRunner.Core.Shapes.EntityShapes
{
    class DamageCrystal : EntityShape
    {
        public DamageCrystal(int x, int y) : base(new FillGenerationStrategy(), x, y, x + 1, y + 2) { }

        public override bool CanOverlap(BaseShape other)
        {
            throw new NotImplementedException();
        }

        public override void OnCollision(BaseShape other)
        {
            if (other is Player p)
            {
                p.ModifyHealth(-1);
            }
        }
    }
}
