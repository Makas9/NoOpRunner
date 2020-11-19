using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoOpRunner.Core.Iterators
{
    public class WindowPixelCollection : IteratorAggregate
    {
        List<WindowPixel> collection = new List<WindowPixel>();

        public List<WindowPixel> GetItems()
        {
            return collection;
        }

        public void Add(WindowPixel item)
        {
            collection.Add(item);
        }

        public override IEnumerator GetEnumerator()
        {
            return new SequentialOrderIterator(this);
        }
    }
}
