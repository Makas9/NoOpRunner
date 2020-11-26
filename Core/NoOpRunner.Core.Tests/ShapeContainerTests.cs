using System;
using System.Linq;
using NoOpRunner.Core.Enums;
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

        public override void ShiftShapes()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void AddShape_WhenCalled_ShouldHaveNewObject()
        {
            // Arrange
            int expectedLength = Shapes.GetItems().Count + 1;

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
            var shape = new PowerUp(x, y, PowerUps.Double_Jump);
            AddShape(shape);

            // Act
            var actualValue = IsAtPos(expectedX, expectedY);

            // Assert
            actualValue.ShouldBeTrue();
        }

        [Theory]
        [InlineData(1, 1, 0, 0)]
        [InlineData(1, 1, 0, 1)]
        [InlineData(1, 1, 1, 0)]
        public void IsAtPos_WhenCalledNonExistantShape_ShouldReturnFalse(int x, int y, int expectedX, int expectedY)
        {
            // Arrange
            var shape = new PowerUp(x, y, PowerUps.Double_Jump);
            AddShape(shape);

            // Act
            var actualValue = IsAtPos(expectedX, expectedY);

            // Assert
            actualValue.ShouldBeFalse();
        }
    }
}
