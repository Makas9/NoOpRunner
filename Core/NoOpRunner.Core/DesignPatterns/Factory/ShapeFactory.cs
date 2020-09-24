using NoOpRunner.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoOpRunner.Core.Shapes
{
    class ShapeFactory : AbstractFactory
    {
        public override BaseShape GetShape(String shape, int x, int y)
        {
            if (shape == null)
            {
                return null;
            }
            
            if (shape.Equals("square"))
            {
                return new Square(x, y);
            }
            else if (shape.Equals("circle"))
            {
                return new Circle(x, y);
            }
            else if (shape.Equals("rectangle"))
            {
                return new Rectangle(x, y);
            }
            if (shape.Equals("stairs"))
            {
                return new Stairs(x, y);
            }
            return null;
        }

    }
}
