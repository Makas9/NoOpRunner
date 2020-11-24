using Newtonsoft.Json;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoOpRunner.Core.Iterators
{
    [JsonObject]
    public class ShapeCollection : IteratorAggregate
    {
        [JsonProperty]
        protected List<BaseShape> collection = new List<BaseShape>();
        bool lastFirst = true;

        public void Reverse()
        {
            lastFirst = !lastFirst;
        }

        public List<BaseShape> GetItems()
        {
            return collection;
        }

        public void Add(BaseShape item)
        {
            collection.Add(item);
        }

        public override IEnumerator GetEnumerator()
        {
            Logging.Instance.Write("Iterator: ShapeCollection GetEnumerator()", LoggingLevel.Iterator);
            return new LatestOrderIterator(this, lastFirst);
        }
    }
}
