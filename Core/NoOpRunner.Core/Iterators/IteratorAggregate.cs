using System;
using System.Collections;
using System.Collections.Generic;

namespace NoOpRunner.Core.Iterators
{
    public abstract class IteratorAggregate : IEnumerable
    {
        public abstract IEnumerator GetEnumerator();
    }
}
