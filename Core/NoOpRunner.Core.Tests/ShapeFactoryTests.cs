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
            ShapeFactory shapeFactory = new ShapeFactory();
            var expected = shapeFactory.GetShape(Shape.DamageCrystal, 5, 5);

            // Act
            var actual = new DamageCrystal(5, 5);

            // Assert
            expected.ShouldBeEquivalentTo(actual);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(10, 100)]
        public void GetShape_WhenValidShapeAndCoordinatesGiven_ShouldReturnAppropriateCoordinates(int x, int y)
        {
            // Arrange
            ShapeFactory shapeFactory = new ShapeFactory();
            var expected = shapeFactory.GetShape(Shape.DamageCrystal, x, y).GetCoords();

            // Act
            var actual = new DamageCrystal(x, y).GetCoords();

            // Assert
            expected.Item1.ShouldBe(actual.Item1);
            expected.Item2.ShouldBe(actual.Item2);
        }
    }
}
