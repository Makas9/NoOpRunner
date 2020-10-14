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

        private bool MapInitialized;

        private bool PowerUpsInitialized;

        public GameStateConfigurator()
        {
            gameState = new GameState();
        }

        public IGameStateConfigurator InitializeMap(int horizontalCount, int verticalCount)
        {
            gameState.Platforms = new PlatformsContainer(horizontalCount, verticalCount);

            MapInitialized = true;

            return this;
        }

        public IGameStateConfigurator InitializePowerUps(int horizontalCount, int verticalCount)
        {
            gameState.PowerUpsContainer = new PowerUpsContainer(horizontalCount, verticalCount);

            PowerUpsInitialized = true;

            return this;
        }

        public IGameStateConfigurator AddShape(Func<ShapeFactory, BaseShape> action)
        {
            if (!MapInitialized) throw new Exception("Map uninitialized");

            var shapeFactory = new ShapeFactory();

            var shape = action(shapeFactory);

            gameState.Platforms.AddShape(shape);

            return this;
        }

        public IGameStateConfigurator AddShape(BaseShape shape)
        {
            if (!MapInitialized) throw new Exception("Map uninitialized");

            gameState.Platforms.AddShape(shape);

            return this;
        }

        public GameState Build()
        {
            if (!MapInitialized) throw new Exception("Map uninitialized");

            return gameState;
        }

        public IGameStateConfigurator AddImpassableShape(Func<AbstractFactory, BaseShape> action)
        {
            if (!MapInitialized) throw new Exception("Map uninitialized");

            var shapeFactory = FactoryProducer.GetFactory(passable: false);

            var shape = action(shapeFactory);

            if (shape is ImpassablePlatform)
            {
                Platforms.Add(shape);
            }

            gameState.Platforms.AddShape(shape);

            return this;
        }

        public IGameStateConfigurator AddPassableShape(Func<AbstractFactory, BaseShape> action)
        {
            if (!MapInitialized) throw new Exception("Map uninitialized");

            var shapeFactory = FactoryProducer.GetFactory(passable: true);

            var shape = action(shapeFactory);

            if (shape is PassablePlatform)
            {
                Platforms.Add(shape);
            }

            gameState.Platforms.AddShape(shape);

            return this;
        }

        public IGameStateConfigurator AddPowerUp(PowerUps type, Func<IReadOnlyList<BaseShape>, BaseShape> action)
        {
            if (!PowerUpsInitialized) throw new Exception("PowerUps uninitialized");

            var shape = action(Platforms.AsReadOnly());

            gameState.PowerUpsContainer.AddShape(new PowerUp(shape.CenterPosX, shape.CenterPosY + 1, type));

            return this;
        }

        public IGameStateConfigurator AddPlayer(Func<IReadOnlyList<BaseShape>, BaseShape> action)
        {
            if (!MapInitialized) throw new Exception("Map uninitialized");

            var shape = action(Platforms.AsReadOnly());

            gameState.Player = new Player(shape.CenterPosX, shape.CenterPosY + 1);

            return this;
        }
    }
}
