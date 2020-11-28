using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes;
using Xunit;
using Shouldly;

namespace NoOpRunner.Core.Tests
{
    public class PlatformsContainerTests : PlatformsContainer
    {
        public PlatformsContainerTests() : base(120, 120) 
        {
            
        }
        [Theory]
        [InlineData(10, 10, 9, 10)]
        [InlineData(20, 20, 19, 20)]
        [InlineData(1, 1, 0, 1)]
        public void ShiftShapes_WhenCalled_ShouldShiftObjectsHorizontallyToLeft(int x, int y, int expectedX, int expectedY)
        {
            // Arrange
            var expectedShape = new PowerUp(x, y, PowerUps.Double_Jump);
            AddShape(expectedShape);

            // Act
            ShiftShapes();

            // Assert
            Shapes.GetItems().Exists(o => o.IsAtPos(expectedX, expectedY)).ShouldBeTrue();
        }

    }
}
