using System;
using NoOpRunner.Core.Shapes.GenerationStrategies;

namespace NoOpRunner.Core.Shapes.EntityShapes
{
    class Saw : EntityShape
    {
        public Saw(int x, int y) : base(new FillGenerationStrategy(), x, y, x + 1, y + 1) { }

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
