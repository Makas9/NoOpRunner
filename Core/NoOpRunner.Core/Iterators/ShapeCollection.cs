using Newtonsoft.Json;
using NoOpRunner.Core.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace NoOpRunner.Core.Iterators
{
    [JsonObject]
    public class ShapeCollection : IteratorAggregate
    {
        [JsonProperty]
        protected List<IMapPart> collection = new List<IMapPart>();

        public List<IMapPart> GetItems()
        {
            return collection;
        }

        public void Add(IMapPart item)
        {
            collection.Add(item);
        }

        public override IEnumerator GetEnumerator()
        {
            Logging.Instance.Write("Iterator: ShapeCollection GetEnumerator()", LoggingLevel.Iterator);

            return new BackwardIterator(this);
        }
    }
}
