namespace NoOpRunner.Core.Iterators
{
    class BackwardIterator : Iterator
    {
        private ShapeCollection collection;
        private int position = -1;

        public BackwardIterator(ShapeCollection collection)
        {
            this.collection = collection;
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
            int updatedPosition = position + -1;

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
            position = Count() - 1;
        }
    }
}
