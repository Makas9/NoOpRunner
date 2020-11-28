using Shouldly;
using Xunit;

namespace NoOpRunner.Core.Tests
{
    public class LoggingTests
    {
        [Fact]
        public void DisableLevel_WhenCalled_ShouldDisableAppropriateLevel()
        {
            // Arrange
            var logger = Logging.Instance;

            // Act
            logger.DisableLevel(LoggingLevel.Trace);

            // Assert
            var levels = logger.GetLevels();
            levels.HasFlag(LoggingLevel.Trace).ShouldBeFalse();
        }

        [Fact]
        public void EnableLevel_WhenCalled_ShouldEnableAppropriateLevel()
        {
            // Arrange
            var logger = Logging.Instance;

            // Act
            logger.EnableLevel(LoggingLevel.Iterator);

            // Assert
            var levels = logger.GetLevels();
            levels.HasFlag(LoggingLevel.Iterator).ShouldBeTrue();
        }
    }
}
