using System.Collections;

namespace NoOpRunner.Core.Iterators
{
    public abstract class IteratorAggregate : IEnumerable
    {
        public abstract IEnumerator GetEnumerator();
    }
}
