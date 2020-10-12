namespace NoOpRunner.Core.Shapes.ShapeFactories
{
    public class FactoryProducer
    {
        public static AbstractFactory GetFactory(bool passable)
        {
            if (passable)
            {
                return new PassableShapeFactory();
            }
            
            return new ImpassableShapeFactory();
        }
    }
}
