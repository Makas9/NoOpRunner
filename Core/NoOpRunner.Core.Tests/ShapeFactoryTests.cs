using NoOpRunner.Core.Enums;
using Xunit;
using Shouldly;
using NoOpRunner.Core.Shapes.ShapeFactories;
using NoOpRunner.Core.Shapes.EntityShapes;

namespace NoOpRunner.Core.Tests
{
    public class ShapeFactoryTests
    {
        [Fact]
        public void GetShape_WhenValidShapeAndCoordinatesGiven_ShouldReturnAppropriateShape()
        {
            // Arrange
            var expected = new DamageCrystal(5, 5);
            ShapeFactory shapeFactory = new ShapeFactory();
            var actual = shapeFactory.GetShape(Shape.DamageCrystal, 5, 5);

            // Act

            // Assert
            expected.ShouldBeEquivalentTo(actual);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(10, 100)]
        public void GetShape_WhenValidShapeAndCoordinatesGiven_ShouldReturnAppropriateCoordinates(int x, int y)
        {
            // Arrange
            var expected = new DamageCrystal(x, y).GetCoords();
            ShapeFactory shapeFactory = new ShapeFactory();
            var actual = shapeFactory.GetShape(Shape.DamageCrystal, x, y).GetCoords();

            // Act

            // Assert
            expected.Item1.ShouldBe(actual.Item1);
            expected.Item2.ShouldBe(actual.Item2);
        }
    }
}
