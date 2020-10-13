using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.ShapeFactories;
using System;
using System.Collections.Generic;

namespace NoOpRunner.Core.Configurators
{
    public class GameStateConfigurator : IGameStateConfigurator
    {
        private readonly GameState gameState;

        private List<BaseShape> Platforms { get; } = new List<BaseShape>();

        private bool MapInitialized;

        public GameStateConfigurator()
        {
            gameState = new GameState();
        }

        public IGameStateConfigurator InitializeMap(int horizontalCount, int verticalCount)
        {
            gameState.Map = new GameMap(horizontalCount, verticalCount);

            MapInitialized = true;

            return this;
        }

        public IGameStateConfigurator AddShape(Func<ShapeFactory, BaseShape> action)
        {
            if (!MapInitialized) throw new Exception("Map uninitialized");

            var shapeFactory = new ShapeFactory();

            var shape = action(shapeFactory);

            gameState.Map.AddShape(shape);

            return this;
        }

        public IGameStateConfigurator AddShape(BaseShape shape)
        {
            if (!MapInitialized) throw new Exception("Map uninitialized");

            gameState.Map.AddShape(shape);

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

            gameState.Map.AddShape(shape);

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

            gameState.Map.AddShape(shape);

            return this;
        }

        public IGameStateConfigurator AddPowerUp(PowerUps type, Func<IReadOnlyList<BaseShape>, BaseShape> action)
        {
            if (!MapInitialized) throw new Exception("Map uninitialized");

            var shape = action(Platforms.AsReadOnly());

            gameState.Map.AddShape(new PowerUp(shape.CenterPosX, shape.CenterPosY + 1, type));

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
