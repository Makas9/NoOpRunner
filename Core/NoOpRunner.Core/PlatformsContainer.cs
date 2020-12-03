using System;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;
using System.Collections.Generic;

namespace NoOpRunner.Core
{
    public class PlatformsContainer : ShapesContainer, IObserver
    {
        public PlatformsContainer(int sizeX, int sizeY) : base(sizeX, sizeY)
        {
        }

        public override void ShiftShapes()
        {
            Logging.Instance.Write($"[Composite/{nameof(PlatformsContainer)}] {nameof(ShiftShapes)}", LoggingLevel.Composite);

            foreach (IMapPart shape in Shapes)
                shape.ShiftShapes(); //Push and remove out of bounds
        }

        public Tuple<int, int> GetPlatformCenterPositions(int platformLevel)
        {
            var shapes = Shapes.GetItems();
            return new Tuple<int, int>(((BaseShape)shapes[platformLevel]).CenterPosX, ((BaseShape)shapes[platformLevel]).CenterPosY);
        }
        
        /// <summary>
        /// For platforms update from client side, append generated cells
        /// </summary>
        /// <param name="message"></param>
        public void Update(MessageDto message)
        {
            if (message.MessageType != MessageType.PlatformsUpdate)
                return;

            Logging.Instance.Write("Observer: platforms got update", LoggingLevel.Pattern);
            
            ShiftShapes();

            var generatedBlocks = message.Payload as List<List<ShapeBlock>>;
            
            if (generatedBlocks == null)
            
                throw new NullReferenceException("Lost package");

            var shapes = Shapes.GetItems();
            for (int i = 0; i < shapes.Count; ++i)
                if (shapes[i] is StaticShape staticShape)
                    staticShape.AddShapeBlocks(generatedBlocks[i]);
        }
    }
}