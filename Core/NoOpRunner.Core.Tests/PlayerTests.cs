using System;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.StaticShapes;
using Xunit;
using Shouldly;
using NoOpRunner.Core.Shapes.GenerationStrategies;

namespace NoOpRunner.Core.Tests
{
    public class PlayerTests : Player
    {
        public PlayerTests() : base(5,5)
        {

        }

        [Fact]
        public void CanOverlap_WhenPassedImpassablePlatform_ShouldReturnFalseDependingOnPlayerState()
        {
            // Arrange
            var shape1 = new ImpassablePlatform(new PlatformerGenerationStrategy(), 0, 0, 1, 1);

            // Act
            bool actualValue = CanOverlap(shape1);

            // Assert
            actualValue.ShouldBeFalse();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CanOverlap_WhenPassedPassablePlatform_ShouldReturnTrueDependingOnPlayerState(bool canPassPlatforms)
        {
            // Arrange
            var shape1 = new PassablePlatform(new PlatformerGenerationStrategy(), 0, 0, 1, 1);
            CanPassPlatforms = canPassPlatforms;

            // Act
            bool actualValue = CanOverlap(shape1);

            // Assert
            actualValue.ShouldBe(canPassPlatforms);
        }

        [Fact]
        public void CanOverlap_WhenPlayerOverlapsWithAnythingElse_ShouldThrowException()
        {
            // Arrange
            var shape1 = new PowerUp(0, 0, PowerUps.Double_Jump);

            // Act

            // Assert
            Should.Throw<NotImplementedException>(() => CanOverlap(shape1));
        }
    }
}
