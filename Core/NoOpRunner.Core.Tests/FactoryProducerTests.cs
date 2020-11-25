using Xunit;
using Shouldly;
using NoOpRunner.Core.Shapes.ShapeFactories;

namespace NoOpRunner.Core.Tests
{
    public class FactoryProducerTests
    {
        [Fact]
        public void GetFactory_WhenValidBoolGiven_ShouldReturnAppropriateFactory()
        {
            // Arrange
            var expected = FactoryProducer.GetFactory(passable: false);

            // Act
            var actual = new ImpassableShapeFactory();

            // Assert
            expected.ShouldBeEquivalentTo(actual);
        }
    }
}
