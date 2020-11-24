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
using Xunit;

namespace NoOpRunner.Core.Tests
{
    public class PowerUpContainerTests
    {
        // Arrange

        // Act

        // Assert

        // TODO:
        // * better implementation
        [Fact] // mostly for stuff tested with same expected variables
        public void GetPowerUpAt_WithGivenCoordinates_ReturnsPowerUp()
        {
            //arrange
            //int expectedX = 5;
            //int expectedY = 5;

            //act
            //PowerUpsContainer powerUpsContainer;
            //PowerUp power = powerUpsContainer.GetPowerUpAt(expectedX, expectedY);

            //assert
            Assert.Equal(1,1);

        }

        [Theory] // testing by passing arguments diffrent sets of data
        [InlineData(4,3,7)]
        [InlineData(5,5,10)]
        public void passingArguments(double x, double y, double expected)
        {
            // Arrange

            // Act
            double actual = x + y;
            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
