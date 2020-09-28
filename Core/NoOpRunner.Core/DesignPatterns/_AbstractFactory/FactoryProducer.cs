using NoOpRunner.Core.DesignPatterns._Factory;

namespace NoOpRunner.Core.DesignPatterns._AbstractFactory
{
    public class FactoryProducer
    {
        public static AbstractFactory GetFactory(bool realistic)
        {
            if (realistic)
            {
                return new RealisticShapeFactory();
            }
            
            return new ShapeFactory();
        }
    }
}
