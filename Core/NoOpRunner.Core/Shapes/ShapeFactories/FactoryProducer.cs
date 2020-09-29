namespace NoOpRunner.Core.Shapes.ShapeFactories
{
    public class FactoryProducer
    {
        public static AbstractFactory GetFactory(bool realistic)
        {
            if (realistic)
            {
                return new RealisticShapeFactory();
            }
            
            return new GeometricShapeFactory();
        }
    }
}
