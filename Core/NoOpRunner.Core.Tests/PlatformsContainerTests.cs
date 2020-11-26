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
    public class PlatformsContainerTests : PlatformsContainer
    {
        public PlatformsContainerTests() : base(120, 120) 
        {
            
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
