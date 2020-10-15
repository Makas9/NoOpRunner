using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.ShapeFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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

            Logging.Instance.Write("Base: " + shape.GetShapes().GetHashCode().ToString());
            Logging.Instance.Write("Shallow: " + shallow.GetShapes().GetHashCode().ToString());
            Logging.Instance.Write("Deep: " + deep.GetShapes().GetHashCode().ToString());
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
