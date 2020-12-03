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
        Composite = 32,
        TemplateMethod = 64,
        Visitor = 128,
        Facade = 256,
        Decorator = 512,
        Command = 1024,
        Builder = 2048,
        Bridge = 4096,
        Memento = 8192
    }
}
