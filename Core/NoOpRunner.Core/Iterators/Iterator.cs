using System.Collections;

namespace NoOpRunner.Core
{
    abstract class Iterator : IEnumerator
    {
        object IEnumerator.Current => Current();

        public abstract int Key();

        public abstract object Current();

        public abstract bool MoveNext();

        public abstract int Count();

        public abstract void Reset();
    }
}
