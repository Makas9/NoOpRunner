using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoOpRunner.Core
{
    class RandomNumber
    {
        private Random random { get; set; }

        public RandomNumber() 
        {
            this.random = new Random();
        }

        public int getRandomNumber(int from, int to)
        {
            return random.Next(from, to);
        }
    }
}
