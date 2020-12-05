using System;

namespace NoOpRunner.Core
{
    public sealed class Logging : ILogger
    {
        private LoggingLevel enabledLevels = LoggingLevel.Other | LoggingLevel.Trace | LoggingLevel.State | LoggingLevel.TemplateMethod | LoggingLevel.Visitor | LoggingLevel.Memento | LoggingLevel.Mediator;
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

        public void Write(string info, LoggingLevel level = LoggingLevel.Other)
        {
            if (!enabledLevels.HasFlag(level))
                return;

            Console.WriteLine(info);
        }

        public void DisableLevel(LoggingLevel level)
        {
            enabledLevels &= ~level;
        }

        public void EnableLevel(LoggingLevel level)
        {
            enabledLevels |= level;
        }

        public LoggingLevel GetLevels() => enabledLevels;
    }
}
