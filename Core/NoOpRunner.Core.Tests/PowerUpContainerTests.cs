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
    public class PowerUpContainerTests : PowerUpsContainer
    {
        public PowerUpContainerTests() : base(120, 120)
        {

        }
        /**
         * Copy these comments to seperate testing parts
        // Arrange
        // Act
        // Assert
         */

        [Fact]
        public void GetPowerUpAt_WhenValidCoordinatesProvided_ShouldReturnAppropriatePowerup()
        {
            // Arrange
            var expectedShape = new PowerUp(0, 0, PowerUps.Double_Jump);
            AddShape(expectedShape);

            // Act
            var actualShape = GetPowerUpAt(0, 0);

            // Assert
            actualShape.ShouldBe(expectedShape);
        }

        [Fact]
        public void GetPowerUpAt_WhenInValidCoordinatesProvided_ShouldReturnNull()
        {
            // Arrange
            PowerUp expectedValue = null;

            // Act
            var actualShape = GetPowerUpAt(1, 1);
        
            // Assert
            actualShape.ShouldBe(expectedValue);
        }

        [Fact]
        public void RemovePowerUp_WhenCalled_ShouldRemoveObjectFromList()
        {
            // Arrange
            var shape2 = new PowerUp(1, 1, PowerUps.Double_Jump);
            var shape3 = new PowerUp(2, 2, PowerUps.Double_Jump);
            var expectedShape = new PowerUp(0, 0, PowerUps.Double_Jump);
            AddShape(shape2);
            AddShape(shape3);
            AddShape(expectedShape);
            var expectedLength = Shapes.GetItems().Count - 1;

            // Act
            RemovePowerUp(0, 0);
            var actualLength = Shapes.GetItems().Count;

            // Assert
            actualLength.ShouldBe(expectedLength);
            Shapes.GetItems().Last().ShouldNotBe(expectedShape);
        }
        [Theory]
        [InlineData(10, 10, 9, 10)]
        public void ShiftShapes_WhenCalled_ShouldShiftObjectsHorizontallyToLeft(int x, int y, int expectedX, int expectedY)
        {
            // Arrange
            var expectedValue = true;
            var shape2 = new PowerUp(1, 1, PowerUps.Double_Jump);
            var shape3 = new PowerUp(2, 2, PowerUps.Double_Jump);
            var expectedShape = new PowerUp(x, y, PowerUps.Double_Jump);
            AddShape(shape2);
            AddShape(shape3);
            AddShape(expectedShape);

            // Act
            ShiftShapes();

            // Assert
            Shapes.GetItems().Exists(o => o.IsAtPos(expectedX, expectedY)).ShouldBe(expectedValue);
        }
    }
}
