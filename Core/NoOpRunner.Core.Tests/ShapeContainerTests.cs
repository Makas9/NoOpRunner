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
    public class ShapeContainerTests : ShapesContainer
    {
        public ShapeContainerTests() : base (120, 120)
        {
            
        }

        //[Fact(Skip ="Is not testable")]
        public override void ShiftShapes()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void AddShape_WhenCalled_ShouldHaveNewObject()
        {
            // Arrange
            int expectedLength = Shapes.GetItems().Count + 1;
            //int expectedLength = 1;

            // Act
            var shape = new PowerUp(0,0, PowerUps.Double_Jump);
            AddShape(shape);
            int actualLength = Shapes.GetItems().Count;

            // Assert
            actualLength.ShouldBe(expectedLength); // probs redundant
            Shapes.GetItems().Last().ShouldBe(shape);

        }
        [Theory]
        [InlineData(1, 1, 1, 1)]
        [InlineData(2, 2, 2, 2)]
        [InlineData(3, 3, 3, 3)]
        public void IsAtPos_WhenCalledExistingShape_ShouldReturnTrue(int x, int y, int expectedX, int expectedY)
        {
            // Arrange
            var expectedValue = true;
            var shape = new PowerUp(x, y, PowerUps.Double_Jump);
            AddShape(shape);

            // Act
            var actualValue = IsAtPos(expectedX, expectedY);

            // Assert
            actualValue.ShouldBe(expectedValue);
        }
        [Theory]
        [InlineData(1, 1, 0, 0)]
        [InlineData(1, 1, 0, 1)]
        [InlineData(1, 1, 1, 0)]
        public void IsAtPos_WhenCalledNonExistantShape_ShouldReturnFalse(int x, int y, int expectedX, int expectedY)
        {
            // Arrange
            var expectedValue = false;
            var shape = new PowerUp(x, y, PowerUps.Double_Jump);
            AddShape(shape);

            // Act
            var actualValue = IsAtPos(expectedX, expectedY);

            // Assert
            actualValue.ShouldBe(expectedValue);
        }
    }
}
