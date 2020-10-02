using System;

namespace NoOpRunner.Core
{
    public sealed class Logging
    {
        private Logging() { }

        private static readonly object bolt = new object();
        private static Logging instance;

        public static Logging Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (bolt)
                    {
                        if (instance == null)
                        {
                            instance = new Logging();
                            instance.Write("Logger initialized");
                        }
                    }
                }

                return instance;
            }
        }

        public void Write(string info)
        {
            Console.WriteLine(info);
        }
    }
}
