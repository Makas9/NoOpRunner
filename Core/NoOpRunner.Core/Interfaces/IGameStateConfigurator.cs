using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Shapes.ShapeFactories;
using System;

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

        IGameStateConfigurator AddPlayer(Func<IMapPart, BaseShape> action);

        IGameStateConfigurator AddPowerUp(PowerUps type, Func<IMapPart, BaseShape> action);

        IMapPart Build();
    }
}
