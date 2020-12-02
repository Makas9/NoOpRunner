using System;

namespace NoOpRunner.Core
{
    [Flags] public enum LoggingLevel
    {
        None = 0,
        Trace = 1,
        Pattern = 2,
        Other = 4,
        State = 8,
        Iterator = 16,
        CompositePattern = 32,
        Visitor = 128
    }
}
