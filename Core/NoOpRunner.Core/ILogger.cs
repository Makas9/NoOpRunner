namespace NoOpRunner.Core
{
    public interface ILogger
    {
        void Write(string info, LoggingLevel level = LoggingLevel.Other);
        void DisableLevel(LoggingLevel level);
        void EnableLevel(LoggingLevel level);
    }
}
