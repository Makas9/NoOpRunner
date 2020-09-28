using System;

namespace NoOpRunner.Core.DesignPatterns._Singleton
{
    class RandomNumber
    {
        private static Random random = null;
        private static object padlock = new object();

        private RandomNumber()
        {
        }

        public static Random GetInstance()
        {
            if (random == null)
            {
                lock (padlock)
                {
                    if (random == null)
                        random = new Random();

                    return random;
                }
            }

            return random;
        }
    }
}
