using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.ShapeFactories;
using System;
using System.Collections.Generic;

namespace NoOpRunner.Core.Interfaces
{
    public interface IGameStateConfigurator
    {
        IGameStateConfigurator InitializeMap(int horizontalCount, int verticalCount);

        IGameStateConfigurator InitializePowerUps(int horizontalCount, int verticalCount);

        IGameStateConfigurator AddShape(Func<ShapeFactory, BaseShape> action);

        IGameStateConfigurator AddShape(BaseShape shape);

        IGameStateConfigurator AddImpassableShape(Func<AbstractFactory, BaseShape> action);
        
        IGameStateConfigurator AddPassableShape(Func<AbstractFactory, BaseShape> action);

        IGameStateConfigurator AddPlayer(Func<IReadOnlyList<BaseShape>, BaseShape> action);

        IGameStateConfigurator AddPowerUp(PowerUps type, Func<IReadOnlyList<BaseShape>, BaseShape> action);

        GameState Build();
    }
}
