using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Core.Interfaces
{
    public interface IShapeConfigurator
    {
        IShapeConfigurator ConfigureBounds(int lowerBoundX, int lowerBoundY, int upperBoundX, int upperBoundY);

        IShapeConfigurator ConfigureGenerationStrategy(GenerationStrategy strategy);

        BaseShape Build();
    }
}
