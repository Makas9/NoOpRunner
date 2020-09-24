using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoOpRunner.Core.Shapes
{
    public class FactoryProducer
    {
        public static AbstractFactory getFactory(Boolean realistic)
        {
            if (realistic)
            {
                return new RealisticShapeFactory();
            }
            else
            {
                return new ShapeFactory();
            }
        }
    }
}
