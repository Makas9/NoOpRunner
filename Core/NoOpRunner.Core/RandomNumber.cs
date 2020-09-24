using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoOpRunner.Core
{
    class RandomNumber
    {
        private Random random;

        private static readonly Lazy<RandomNumber> _instance = new Lazy<RandomNumber>(() => new RandomNumber());

        public static RandomNumber Instance
        {
            get { return _instance.Value; }
        }

        private RandomNumber()
        {
            random = new Random();
        }

        public int GetRandom(int from, int to)
        {
            return random.Next(from, to);
        }
    }
}
