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
            var expected = new ImpassableShapeFactory();
            var actual = FactoryProducer.GetFactory(passable: false);

            // Act

            // Assert
            expected.ShouldBeEquivalentTo(actual);
        }
    }
}
