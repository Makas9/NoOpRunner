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
using Shouldly;

namespace NoOpRunner.Core.Tests
{
    public class PowerUpContainerTests
    {
        /**
         * Copy these comments to seperate testing parts
        // Arrange
        // Act
        // Assert
         */

        [Fact] // mostly for stuff tested with same expected variables
        public void GetPowerUpAt_WhenValidCoordinatesProvided_ShouldReturnAppropriatePowerup()
        {
            // Arrange
            int expected = 123;

            // Act
            int actual = 123;
            
            // Assert
            // Assert.Equal(1,1); // Old Assert way
            expected.ShouldBe(actual); // New Assert way

        }

        [Theory] // testing by passing arguments diffrent sets of data
        [InlineData(4,3,7)]
        [InlineData(5,5,10)]
        public void PassingArgumentsExample_WhenAnArgumentPassExampleNeeded_ShouldReturnSum(double x, double y, double expected)
        {
            // Arrange

            // Act
            double actual = x + y;

            // Assert
            //Assert.Equal(expected, actual); // Old Assert way
            expected.ShouldBe(actual); // New Assert way
        }
    }
}
