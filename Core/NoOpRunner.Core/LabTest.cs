using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.ShapeFactories;
using System;
using System.Collections.Generic;
using System.Linq;
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

            BaseShape shallow = shape.ShallowCopy(); // Shallow Copy

            BaseShape deep = shape.DeepCopy(); // Deep Copy

            Logging.Instance.Write("Before");
            Logging.Instance.Write("Base: " + shape.ShapesToString());
            Logging.Instance.Write("Shallow (Prototype): " + shallow.ShapesToString());
            Logging.Instance.Write("Deep: " + deep.ShapesToString());

            shape.AddBlock(10, 10);

            Logging.Instance.Write("After");
            Logging.Instance.Write("Base: " + shape.ShapesToString());
            Logging.Instance.Write("Shallow (Prototype): " + shallow.ShapesToString());
            Logging.Instance.Write("Deep: " + deep.ShapesToString());

            //Logging.Instance.Write(shape.GetHashCode().ToString());
            //Logging.Instance.Write(shallow.GetHashCode().ToString());
            //Logging.Instance.Write(deep.GetHashCode().ToString());
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
