using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.StaticShapes;

namespace NoOpRunner.Core.Configurators
{
    public class ImpassablePlatformConfigurator : ShapeConfigurator
    {
        public override BaseShape Build()
        {
            Logging.Instance.Write("[ShapeBuilder/ImpassablePlatform] Created impassable platform", LoggingLevel.Pattern);

            return new ImpassablePlatform(GenerationStrategy, LowerBoundX, LowerBoundY, UpperBoundX, UpperBoundY);
        }
    }
}
