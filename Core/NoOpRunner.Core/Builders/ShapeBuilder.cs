using NoOpRunner.Core.Interfaces;

namespace NoOpRunner.Core.Builders
{
    public class ShapeBuilder<TShapeConfigurator> where TShapeConfigurator: IShapeConfigurator, new()
    {
        private readonly TShapeConfigurator configurator;

        public ShapeBuilder()
        {
            configurator = new TShapeConfigurator();
        }

        public TShapeConfigurator Configure()
        {
            return configurator;
        }
    }
}
 