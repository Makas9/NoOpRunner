using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Core.Configurators
{
    public class PassablePlatformConfigurator : ShapeConfigurator
    {
        public override BaseShape Build()
        {
            return new PassablePlatform(GenerationStrategy, LowerBoundX, LowerBoundY, UpperBoundX, UpperBoundY);
        }
    }
}
