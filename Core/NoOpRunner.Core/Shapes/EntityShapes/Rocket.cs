using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Shapes.GenerationStrategies;
using System;

namespace NoOpRunner.Core.Shapes.EntityShapes
{
    class Rocket : EntityShape
    {
        public Rocket(int x, int y) : base(new FillGenerationStrategy(), x, y, x + 1, y + 1) { }

        public override bool CanOverlap(BaseShape other) => false;

        public override void OnCollision(BaseShape other)
        {
            if (other is Player p)
            {
                //p.ModifyHealth(heal: false, 1);
                throw new NotImplementedException();
            }
        }
    }
}
