using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoOpRunner.Core.Iterators
{
    class LatestOrderIterator : Iterator
    {
        private ShapeCollection collection;
        private int position = -1;
        private bool reverse = true;

        public LatestOrderIterator(ShapeCollection collection, bool reverse = true)
        {
            this.collection = collection;
            this.reverse = reverse;

            if (reverse)
                this.position = Count();
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
            int updatedPosition = position + (reverse ? -1 : 1);

            if (updatedPosition >= 0 && updatedPosition < collection.GetItems().Count)
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
            position = reverse ? Count() - 1 : 0;
        }
    }
}
