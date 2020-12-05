using NoOpRunner.Core.Constants;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Iterators;
using NoOpRunner.Core.Shapes;
using NoOpRunner.Core.Visitors;
using System.Collections.Generic;
using System.Linq;

namespace NoOpRunner.Core
{
    public class GameState : IMapPart, IMapMediator
    {
        public IMapPart Platforms { get; set; }

        public IMapPart Player { get; set; }

        public IMapPart PowerUpsContainer { get; set; }

        public void Accept(INodeVisitor visitor)
        {
            Platforms.Accept(visitor);
            Player.Accept(visitor);
            PowerUpsContainer.Accept(visitor);
        }

        public void AddMapPart(IMapPart mapPart)
        {
            Logging.Instance.Write($"[Composite/{nameof(GameState)}] {nameof(AddMapPart)}", LoggingLevel.Composite);

            if (mapPart is StaticShape)
            {
                Platforms.AddMapPart(mapPart);
            }
            else if (mapPart is Player)
            {
                Player = mapPart;
            }
            else if (mapPart is PowerUp)
            {
                PowerUpsContainer.AddMapPart(mapPart);
            }
        }

        public List<List<ShapeBlock>> GetNextBlocks()
        {
            Logging.Instance.Write($"[Composite/{nameof(GameState)}] {nameof(GetNextBlocks)}", LoggingLevel.Composite);

            return Platforms.GetNextBlocks();
        }

        public List<T> GetOfType<T>() where T : IMapPart
        {
            Logging.Instance.Write($"[Composite/{nameof(GameState)}] {nameof(GetOfType)}", LoggingLevel.Composite);

            if (Platforms?.GetType() == typeof(T))
            {
                return new List<T> { (T)Platforms };
            }

            if (Player?.GetType() == typeof(T))
            {
                return new List<T> { (T)Player };
            }

            if (PowerUpsContainer?.GetType() == typeof(T))
            {
                return new List<T> { (T)PowerUpsContainer };
            }

            var platforms = Platforms?.GetOfType<T>() ?? new List<T>();
            var powerups = PowerUpsContainer?.GetOfType<T>() ?? new List<T>();

            return platforms.Union(powerups).ToList();
        }

        public bool IsAtPos(int centerPosX, int centerPosY)
        {
            Logging.Instance.Write($"[Composite/{nameof(GameState)}] {nameof(IsAtPos)}", LoggingLevel.Composite);

            return Platforms.IsAtPos(centerPosX, centerPosY) ||
                Player.IsAtPos(centerPosX, centerPosY) ||
                PowerUpsContainer.IsAtPos(centerPosX, centerPosY);
        }

        public void Notify(object sender, MediatorMessageDto message)
        {
            if (sender is BaseShape shape && message.Event == MediatorEvents.BaseShapeClicked)
            {
                Logging.Instance.Write($"[Mediator] Base shape clicked. Coordinates: {shape.CenterPosX}, {shape.CenterPosY}", LoggingLevel.Mediator);
            }
        }

        public WindowPixelCollection Render()
        {
            Logging.Instance.Write($"[Composite/{nameof(GameState)}] {nameof(Render)}", LoggingLevel.Composite);

            var pixelCollection = new WindowPixelCollection();

            foreach (WindowPixel pixel in Platforms.Render())
            {
                pixelCollection.Add(pixel);
            }

            return pixelCollection;
        }

        public WindowPixel[,] RenderPixels(bool ignoreCollision = false)
        {
            Logging.Instance.Write($"[Composite/{nameof(GameState)}] {nameof(RenderPixels)}", LoggingLevel.Composite);

            // Do nothing
            return null;
        }

        public void SetMapMediator(IMapMediator mediator)
        {
            Platforms?.SetMapMediator(mediator);
            Player?.SetMapMediator(mediator);
            PowerUpsContainer?.SetMapMediator(mediator);
        }

        public void ShiftShapes()
        {
            Logging.Instance.Write($"[Composite/{nameof(GameState)}] {nameof(ShiftShapes)}", LoggingLevel.Composite);

            Platforms.ShiftShapes();
            PowerUpsContainer.ShiftShapes();
        }
    }
}
