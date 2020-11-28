using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoOpRunner.Core.Iterators
{
    class SequentialOrderIterator : Iterator
    {
        private WindowPixelCollection collection;
        private int position = -1;

        public SequentialOrderIterator(WindowPixelCollection collection)
        {
            this.collection = collection;
        }

        public override object Current()
        {
            return collection.GetItems()[position];
        }

        public override int Key()
        {
            return position;
        }

        public override bool MoveNext()
        {
            int updatedPosition = position + 1;

            if (updatedPosition >= 0 && updatedPosition < Count())
            {
                position = updatedPosition;
                return true;
            }

            return false;
        }

        public override int Count()
        {
            return collection.GetItems().Count;
        }

        public override void Reset()
        {
            position = 0;
        }
    }
}
