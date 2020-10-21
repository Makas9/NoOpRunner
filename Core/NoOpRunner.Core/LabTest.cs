using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.ShapeFactories;
using System.Linq;

namespace NoOpRunner.Core
{
    class LabTest
    {

        public static void TestPrototype()
        {
            AbstractFactory abstractShapeFactory = FactoryProducer.GetFactory(true);
            BaseShape shape = abstractShapeFactory.CreateEntityShape(Shape.HealthCrystal, 2, 5);

            BaseShape shallow = shape.Clone(); // Shallow Copy
            BaseShape deep = shape.DeepCopy(); // Deep Copy

            ShapeBlock ba = shape.GetShapes().First();
            ShapeBlock sh = shallow.GetShapes().First();
            ShapeBlock de = deep.GetShapes().First();

            Logging.Instance.Write("Before");
            Logging.Instance.Write("Base: " + shape.GetShapes().GetHashCode().ToString() + "(OffsetX: " + ba.OffsetX + ")");
            Logging.Instance.Write("Shallow: " + shallow.GetShapes().GetHashCode().ToString() + " (OffsetX: " + sh.OffsetX + ")");
            Logging.Instance.Write("Deep: " + deep.GetShapes().GetHashCode().ToString() + " (OffsetX: " + de.OffsetX + ")");

            ba.OffsetX = 10;

            Logging.Instance.Write("After");
            Logging.Instance.Write("Base: " + shape.GetShapes().GetHashCode().ToString() + " (OffsetX: " + ba.OffsetX + ")");
            Logging.Instance.Write("Shallow: " + shallow.GetShapes().GetHashCode().ToString() + " (OffsetX: " + sh.OffsetX + ")");
            Logging.Instance.Write("Deep: " + deep.GetShapes().GetHashCode().ToString() + " (OffsetX: " + de.OffsetX + ")");
        }

        public static void TestFactory()
        {
            ShapeFactory shapeFactory = new ShapeFactory();
            BaseShape shape = shapeFactory.GetShape(Shape.DamageCrystal, 5, 5);
        }

        public static void TestAbstractFactory()
        {
            AbstractFactory abstractShapeFactory = FactoryProducer.GetFactory(true);
            BaseShape shape = abstractShapeFactory.CreateEntityShape(Shape.HealthCrystal, 20, 5);
        }
    }
}
