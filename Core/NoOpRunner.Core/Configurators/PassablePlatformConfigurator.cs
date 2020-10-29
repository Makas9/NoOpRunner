using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.StaticShapes;

namespace NoOpRunner.Core.Configurators
{
    public class PassablePlatformConfigurator : ShapeConfigurator
    {
        public override BaseShape Build()
        {
            Logging.Instance.Write("[ShapeBuilder/PassablePlatform] Created passable platform");
            
            return new PassablePlatform(GenerationStrategy, LowerBoundX, LowerBoundY, UpperBoundX, UpperBoundY);
        }
    }
}
