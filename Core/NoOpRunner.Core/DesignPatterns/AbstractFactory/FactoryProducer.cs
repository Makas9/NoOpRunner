using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoOpRunner.Core.Shapes
{
    public class FactoryProducer
    {
        public static AbstractFactory GetFactory(Boolean realistic)
        {
            if (realistic)
            {
                return new RealisticShapeFactory();
            }
            
            return new ShapeFactory();
        }
    }
}
