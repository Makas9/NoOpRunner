using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoOpRunner.Core
{
    class RandomNumber
    {
        private static readonly object padlock = new object();
        private static RandomNumber instance = null;
        private Random random;

        RandomNumber()
        {
            random = new Random();
        }

        public static RandomNumber Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new RandomNumber();
                    }
                    return instance;
                }
            }
        }

        public int getRandom(int from, int to)
        {
            return random.Next(from, to);
        }
    }
}
