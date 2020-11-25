using Xunit;
using Shouldly;
using NoOpRunner.Core.Shapes.ShapeFactories;
using NoOpRunner.Core.Shapes.EntityShapes;

namespace NoOpRunner.Core.Tests
{
    public class ImpassableShapeFactoryTests
    {
        [Fact]
        public void CreateEntityShape_WhenValidShapeAndCoordinatesGiven_ShouldReturnAppropriateEntityShape()
        {
            // Arrange
            var expected = new Saw(1, 1);
            ImpassableShapeFactory factory = new ImpassableShapeFactory();

            // Act
            var actual = factory.CreateEntityShape(Enums.Shape.Saw, 1, 1);

            // Assert
            expected.ShouldBeEquivalentTo(actual);
        }

        [Theory] 
        [InlineData(1, 2)]
        [InlineData(10, 100)]
        public void CreateEntityShape_WhenValidShapeAndCoordinatesGiven_ShouldReturnAppropriateCoordinates(int x, int y)
        {
            // Arrange
            var expected = new Saw(x, y).GetCoords();
            ImpassableShapeFactory factory = new ImpassableShapeFactory();

            // Act
            var actual = factory.CreateEntityShape(Enums.Shape.Saw, x, y).GetCoords();

            // Assert
            expected.Item1.ShouldBe(actual.Item1);
            expected.Item2.ShouldBe(actual.Item2);
        }
    }
}
