using NoOpRunner.Core.Shapes.GenerationStrategies;

namespace NoOpRunner.Core.Shapes.EntityShapes
{
    class Rocket : EntityShape
    {
        public Rocket(int x, int y) : base(new FillGenerationStrategy(), x, y, x + 1, y + 1) { }
    }
}
