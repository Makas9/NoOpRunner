using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.StaticShapes;

namespace NoOpRunner.Core.Configurators
{
    public class ImpassablePlatformConfigurator : ShapeConfigurator
    {
        public override BaseShape Build()
        {
            return new ImpassablePlatform(GenerationStrategy, LowerBoundX, LowerBoundY, UpperBoundX, UpperBoundY);
        }
    }
}
