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
            ImpassableShapeFactory factory = new ImpassableShapeFactory();
            var expected = factory.CreateEntityShape(Enums.Shape.Saw, 1, 1);

            // Act
            var actual = new Saw(1, 1);
            
            // Assert
            expected.ShouldBeEquivalentTo(actual);
        }

        [Theory] 
        [InlineData(1, 2)]
        [InlineData(10, 100)]
        public void CreateEntityShape_WhenValidShapeAndCoordinatesGiven_ShouldReturnAppropriateCoordinates(int x, int y)
        {
            // Arrange
            ImpassableShapeFactory factory = new ImpassableShapeFactory();
            var expected = factory.CreateEntityShape(Enums.Shape.Saw, x, y).GetCoords();

            // Act
            var actual = new Saw(x, y).GetCoords();

            // Assert
            expected.Item1.ShouldBe(actual.Item1);
            expected.Item2.ShouldBe(actual.Item2);
        }
    }
}
