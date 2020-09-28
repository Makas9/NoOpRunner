using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoOpRunner.Core
{
    class RandomNumber
    {
        private static Random random = null;
        private static readonly object padlock = new object();

        RandomNumber()
        {
        }

        public static Random Instance
        {
            get
            {
                lock (padlock)
                {
                    if (random == null)
                    {
                        random = new Random();
                    }
                    return random;
                }
            }
        }
    }
}
