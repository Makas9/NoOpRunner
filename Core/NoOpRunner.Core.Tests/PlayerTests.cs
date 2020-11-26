using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoOpRunner.Core;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
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
        public void CanOverlap_WhenPassedImpassablePlatform_ShouldReturnFalse()
        {
            // Arrange
            var expectedValue = false;
            var shape1 = new ImpassablePlatform(new PlatformerGenerationStrategy(), 0, 0, 1, 1);

            // Act
            bool actualValue = CanOverlap(shape1);

            // Assert
            actualValue.ShouldBe(expectedValue);
        }
        [Fact]
        public void CanOverlap_WhenPassedPassablePlatform_ShouldReturnFalse()
        {
            // Arrange
            var expectedValue = false;
            var shape1 = new PassablePlatform(new PlatformerGenerationStrategy(), 0, 0, 1, 1);

            // Act
            bool actualValue = CanOverlap(shape1);

            // Assert
            actualValue.ShouldBe(expectedValue);
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
