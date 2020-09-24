using NoOpRunner.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoOpRunner.Core.Shapes
{
    public abstract class AbstractFactory
    {
        public abstract BaseShape GetShape(String shape, int x, int y);
    }
}
