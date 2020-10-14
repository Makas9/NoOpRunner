using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.GenerationStrategies;

namespace NoOpRunner.Core.Configurators
{
    public abstract class ShapeConfigurator : IShapeConfigurator
    {
        protected int LowerBoundX { get; set; }
        protected int LowerBoundY { get; set; }
        protected int UpperBoundX { get; set; }
        protected int UpperBoundY { get; set; }

        protected GenerationStrategy GenerationStrategy { get; set; }

        public IShapeConfigurator ConfigureBounds(int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY)
        {
            LowerBoundX = lowerBoundX;
            LowerBoundY = lowerBoundY;
            UpperBoundX = upperBoundX;
            UpperBoundY = upperBoundY;

            return this;
        }

        public IShapeConfigurator ConfigureGenerationStrategy(GenerationStrategy strategy)
        {
            GenerationStrategy = strategy;

            return this;
        }

        public abstract BaseShape Build();
    }
}
