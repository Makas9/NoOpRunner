using NoOpRunner.Core.Builders;
using NoOpRunner.Core.Configurators;
using NoOpRunner.Core.Shapes.GenerationStrategies;
using NoOpRunner.Core.Shapes.StaticShapes;
using Shouldly;
using Xunit;

namespace NoOpRunner.Core.Tests
{
    public class ShapeBuilderTests
    {
        [Fact]
        public void ShapeBuilder_WhenPassablePlatformConfiguratorIsUsed_ShouldCreatePassablePlatform()
        {
            // Arrange
            var shapeBuilderConfigurator = new ShapeBuilder<PassablePlatformConfigurator>().Configure();
            shapeBuilderConfigurator.ConfigureBounds(1, 2, 3, 4);
            shapeBuilderConfigurator.ConfigureGenerationStrategy(new FillGenerationStrategy());

            // Act
            var result = shapeBuilderConfigurator.Build();

            // Assert
            result.ShouldBeAssignableTo<PassablePlatform>();
            result.CenterPosX.ShouldBeInRange(1, 4);
            result.CenterPosY.ShouldBeInRange(1, 4);
        }

        [Fact]
        public void ShapeBuilder_WhenImpassablePlatformConfiguratorIsUsed_ShouldCreateImpassablePlatform()
        {
            // Arrange
            var shapeBuilderConfigurator = new ShapeBuilder<ImpassablePlatformConfigurator>().Configure();
            shapeBuilderConfigurator.ConfigureBounds(1, 2, 3, 4);
            shapeBuilderConfigurator.ConfigureGenerationStrategy(new FillGenerationStrategy());

            // Act
            var result = shapeBuilderConfigurator.Build();

            // Assert
            result.ShouldBeAssignableTo<ImpassablePlatform>();
            result.CenterPosX.ShouldBeInRange(1, 4);
            result.CenterPosY.ShouldBeInRange(1, 4);
        }
    }
}
