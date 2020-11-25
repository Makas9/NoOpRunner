﻿using Xunit;
using Shouldly;
using NoOpRunner.Core.Shapes.ShapeFactories;
using NoOpRunner.Core.Shapes.EntityShapes;

namespace NoOpRunner.Core.Tests
{
    public class PassableShapeFactoryTests
    {
        [Fact]
        public void CreateEntityShape_WhenValidShapeAndCoordinatesGiven_ShouldReturnAppropriateEntityShape()
        {
            // Arrange
            PassableShapeFactory factory = new PassableShapeFactory();
            var expected = factory.CreateEntityShape(Enums.Shape.HealthCrystal, 1, 1);

            // Act
            var actual = new HealthCrystal(1, 1);
            
            // Assert
            expected.ShouldBeEquivalentTo(actual);
        }

        [Theory] 
        [InlineData(1, 2)]
        [InlineData(10, 100)]
        public void CreateEntityShape_WhenValidShapeAndCoordinatesGiven_ShouldReturnAppropriateCoordinates(int x, int y)
        {
            // Arrange
            PassableShapeFactory factory = new PassableShapeFactory();
            var expected = factory.CreateEntityShape(Enums.Shape.HealthCrystal, x, y).GetCoords();

            // Act
            var actual = new HealthCrystal(x, y).GetCoords();

            // Assert
            expected.Item1.ShouldBe(actual.Item1);
            expected.Item2.ShouldBe(actual.Item2);
        }
    }
}
