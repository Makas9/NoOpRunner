using NoOpRunner.Core.Builders;
using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.EntityShapes;
using NoOpRunner.Core.Shapes.GenerationStrategies;
using NoOpRunner.Core.Shapes.StaticShapes;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace NoOpRunner.Core.Tests
{
    public class GameStateBuilderTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GameStateBuilder_WhenMapIsUninitialized_AndShapeIsAdded_ShouldThrowException(bool initializeMap)
        {
            // Arrange
            var gameStateConfigurator = new GameStateBuilder().Configure();

            if (initializeMap)
            {
                gameStateConfigurator.InitializeMap(1, 1);
            }

            // Act
            var action = new Action(() => gameStateConfigurator.AddShape(f => f.GetShape(Enums.Shape.Saw, 1, 1)));

            // Assert
            if (!initializeMap)
                action.ShouldThrow<Exception>();
            else
                action.ShouldNotThrow();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GameStateBuilder_WhenPowerUpsAndMapAreUninitialized_AndPowerUpIsAdded_ShouldThrowException(bool initialize)
        {
            // Arrange
            var gameStateConfigurator = new GameStateBuilder().Configure();

            if (initialize)
            {
                gameStateConfigurator.InitializeMap(1, 1);
                gameStateConfigurator.InitializePowerUps(1, 1);
            }

            // Act
            var action = new Action(() => gameStateConfigurator.AddPowerUp(Enums.PowerUps.Rocket, p => new PassablePlatform(new FillGenerationStrategy(), 1, 1, 2, 2)));

            // Assert
            if (!initialize)
                action.ShouldThrow<Exception>();
            else
                action.ShouldNotThrow();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GameStateBuilder_WhenMapIsUninitialized_AndPlayerIsAdded_ShouldThrowException(bool initializeMap)
        {
            // Arrange
            var gameStateConfigurator = new GameStateBuilder().Configure();

            if (initializeMap)
            {
                gameStateConfigurator.InitializeMap(1, 1);
            }

            // Act
            var action = new Action(() => gameStateConfigurator.AddPlayer(p => new PassablePlatform(new FillGenerationStrategy(), 1, 1, 2, 2)));

            // Assert
            if (!initializeMap)
                action.ShouldThrow<Exception>();
            else
                action.ShouldNotThrow();
        }

        [Fact]
        public void GameStateBuilder_WhenShapeIsAddedWithFactory_ShouldBeInMap()
        {
            // Arrange
            var gameStateConfigurator = new GameStateBuilder().Configure();
            gameStateConfigurator.InitializeMap(2, 2);

            // Act
            gameStateConfigurator.AddShape(f => f.GetShape(Enums.Shape.Saw, 1, 1));
            gameStateConfigurator.AddShape(f => f.GetShape(Enums.Shape.Rocket, 2, 2));

            var result = gameStateConfigurator.Build();

            // Assert
            var shapes = result.GetOfType<BaseShape>();

            shapes.Count.ShouldBe(2);
            shapes.ShouldContain(x => x.CenterPosX == 1 && x.CenterPosY == 1);
            shapes.ShouldContain(x => x.CenterPosX == 2 && x.CenterPosY == 2);
        }

        [Fact]
        public void GameStateBuilder_WhenShapeIsAdded_ShouldBeInMap()
        {
            // Arrange
            var gameStateConfigurator = new GameStateBuilder().Configure();
            gameStateConfigurator.InitializeMap(2, 2);

            // Act
            gameStateConfigurator.AddShape(new Rocket(1, 1));
            gameStateConfigurator.AddShape(new Saw(2, 2));

            var result = gameStateConfigurator.Build();

            // Assert
            var shapes = result.GetOfType<BaseShape>();

            shapes.Count.ShouldBe(2);
            shapes.ShouldContain(x => x.CenterPosX == 1 && x.CenterPosY == 1);
            shapes.ShouldContain(x => x.CenterPosX == 2 && x.CenterPosY == 2);
        }

        [Fact]
        public void GameStateBuilder_WhenPassablePlatformIsAdded_CorrectTypeShouldBeInMap()
        {
            // Arrange
            var gameStateConfigurator = new GameStateBuilder().Configure();
            gameStateConfigurator.InitializeMap(2, 2);

            // Act
            gameStateConfigurator.AddPassableShape(f => f.CreateStaticShape(Enums.Shape.Platform, new FillGenerationStrategy(), 1, 1, 2, 2));

            var result = gameStateConfigurator.Build();

            // Assert
            var shapes = result.GetOfType<PassablePlatform>();

            shapes.Count.ShouldBe(1);
            shapes.First().CenterPosX.ShouldBe(1);
            shapes.First().CenterPosY.ShouldBe(1);
        }

        [Fact]
        public void GameStateBuilder_WhenImpassablePlatformIsAdded_CorrectTypeShouldBeInMap()
        {
            // Arrange
            var gameStateConfigurator = new GameStateBuilder().Configure();
            gameStateConfigurator.InitializeMap(2, 2);

            // Act
            gameStateConfigurator.AddImpassableShape(f => f.CreateStaticShape(Enums.Shape.Platform, new FillGenerationStrategy(), 1, 1, 2, 2));

            var result = gameStateConfigurator.Build();

            // Assert
            var shapes = result.GetOfType<ImpassablePlatform>();

            shapes.Count.ShouldBe(1);
            shapes.First().CenterPosX.ShouldBe(1);
            shapes.First().CenterPosY.ShouldBe(1);
        }

        [Fact]
        public void GameStateBuilder_WhenPlayerIsAdded_ShouldBeInMap()
        {
            // Arrange
            var gameStateConfigurator = new GameStateBuilder().Configure();
            gameStateConfigurator.InitializeMap(2, 2);

            // Act
            gameStateConfigurator.AddPlayer(m => new PassablePlatform(new FillGenerationStrategy(), 1, 1, 2, 2));

            var result = gameStateConfigurator.Build();

            // Assert
            var player = result.GetOfType<Player>().FirstOrDefault();
            player.ShouldNotBeNull();
            player.CenterPosX.ShouldBe(1);
            player.CenterPosY.ShouldBe(2);
        }

        [Fact]
        public void GameStateBuilder_WhenPowerupIsAdded_ShouldBeInMap()
        {
            // Arrange
            var gameStateConfigurator = new GameStateBuilder().Configure();
            gameStateConfigurator.InitializeMap(1, 1);
            gameStateConfigurator.InitializePowerUps(4, 4);

            // Act
            gameStateConfigurator.AddPowerUp(Enums.PowerUps.Rocket, m => new PassablePlatform(new FillGenerationStrategy(), 1, 1, 2, 2));
            gameStateConfigurator.AddPowerUp(Enums.PowerUps.Saw, m => new PassablePlatform(new FillGenerationStrategy(), 3, 3, 4, 4));

            var result = gameStateConfigurator.Build();

            // Assert
            var shapes = result.GetOfType<PowerUp>();

            shapes.Count.ShouldBe(2);
            shapes.ShouldContain(x => x.CenterPosX == 1 && x.CenterPosY == 2 && x.PowerUpType == Enums.PowerUps.Rocket);
            shapes.ShouldContain(x => x.CenterPosX == 3 && x.CenterPosY == 4 && x.PowerUpType == Enums.PowerUps.Saw);
        }
    }
}
