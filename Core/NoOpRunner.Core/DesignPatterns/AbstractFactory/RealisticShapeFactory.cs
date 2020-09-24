using NoOpRunner.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoOpRunner.Core.Shapes
{
    class RealisticShapeFactory : AbstractFactory
    {
        public override BaseShape GetShape(String shape, int x, int y)
        {
            if (shape == null)
            {
                return null;
            }
            
            if (shape.Equals("stairs"))
            {
                return new Stairs(x, y);
            }

            return null;
        }

    }
}
