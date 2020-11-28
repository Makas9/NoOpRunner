using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.ShapeFactories;
using NoOpRunner.Core.Shapes.StaticShapes;
using System;
using System.Collections.Generic;

namespace NoOpRunner.Core.Configurators
{
    public class GameStateConfigurator : IGameStateConfigurator
    {
        private readonly GameState gameState;

        private List<BaseShape> Platforms { get; } = new List<BaseShape>();

        private bool mapInitialized;

        private bool powerUpsInitialized;

        public GameStateConfigurator()
        {
            gameState = new GameState();
        }

        public IGameStateConfigurator InitializeMap(int horizontalCount, int verticalCount)
        {
            gameState.Platforms = new PlatformsContainer(horizontalCount, verticalCount);

            mapInitialized = true;

            Logging.Instance.Write("[GameStateBuilder] Map initialized", LoggingLevel.Pattern);

            return this;
        }

        public IGameStateConfigurator InitializePowerUps(int horizontalCount, int verticalCount)
        {
            gameState.PowerUpsContainer = new PowerUpsContainer(horizontalCount, verticalCount);

            powerUpsInitialized = true;

            Logging.Instance.Write("[GameStateBuilder] Power ups initialized", LoggingLevel.Pattern);

            return this;
        }

        public IGameStateConfigurator AddShape(Func<ShapeFactory, BaseShape> action)
        {
            if (!mapInitialized) throw new Exception("Map uninitialized");

            var shapeFactory = new ShapeFactory();

            var shape = action(shapeFactory);

            gameState.Platforms.AddMapPart(shape);

            Logging.Instance.Write("[GameStateBuilder] Added shape", LoggingLevel.Pattern);

            return this;
        }

        public IGameStateConfigurator AddShape(BaseShape shape)
        {
            if (!mapInitialized) throw new Exception("Map uninitialized");

            gameState.Platforms.AddMapPart(shape);

            Logging.Instance.Write("[GameStateBuilder] Added shape", LoggingLevel.Pattern);

            return this;
        }

        public IMapPart Build()
        {
            if (!mapInitialized) throw new Exception("Map uninitialized");

            return gameState;
        }

        public IGameStateConfigurator AddImpassableShape(Func<AbstractFactory, BaseShape> action)
        {
            if (!mapInitialized) throw new Exception("Map uninitialized");

            var shapeFactory = FactoryProducer.GetFactory(passable: false);

            var shape = action(shapeFactory);

            if (shape is ImpassablePlatform)
            {
                Platforms.Add(shape);
            }

            gameState.Platforms.AddMapPart(shape);

            Logging.Instance.Write("[GameStateBuilder] Add impassable shape", LoggingLevel.Pattern);

            return this;
        }

        public IGameStateConfigurator AddPassableShape(Func<AbstractFactory, BaseShape> action)
        {
            if (!mapInitialized) throw new Exception("Map uninitialized");

            var shapeFactory = FactoryProducer.GetFactory(passable: true);

            var shape = action(shapeFactory);

            if (shape is PassablePlatform)
            {
                Platforms.Add(shape);
            }

            gameState.Platforms.AddMapPart(shape);

            Logging.Instance.Write("[GameStateBuilder] Added passable shape", LoggingLevel.Pattern);

            return this;
        }

        public IGameStateConfigurator AddPowerUp(PowerUps type, Func<IMapPart, BaseShape> action)
        {
            if (!powerUpsInitialized) throw new Exception("PowerUps uninitialized");

            var shape = action(gameState);

            gameState.PowerUpsContainer.AddMapPart(new PowerUp(shape.CenterPosX, shape.CenterPosY + 1, type));

            Logging.Instance.Write("[GameStateBuilder] Added power up", LoggingLevel.Pattern);

            return this;
        }

        public IGameStateConfigurator AddPlayer(Func<IMapPart, BaseShape> action)
        {
            if (!mapInitialized) throw new Exception("Map uninitialized");

            var shape = action(gameState);

            gameState.Player = new Player(shape.CenterPosX, shape.CenterPosY + 1);

            Logging.Instance.Write("[GameStateBuilder] Added player", LoggingLevel.Pattern);

            return this;
        }
    }
}
