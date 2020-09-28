using System;

namespace NoOpRunner.Core
{
    class RandomNumber
    {
        private static Random random = new Random();

        public static Random GetInstance()
        {
            return random;
        }
    }
}
