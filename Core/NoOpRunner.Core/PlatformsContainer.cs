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
            Logging.Instance.Write($"[Composite/{nameof(PlatformsContainer)}] {nameof(ShiftShapes)}", LoggingLevel.CompositePattern);

            Shapes.ForEach(x => x.ShiftShapes()); //Push and remove out of bounds
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
            for (int i = 0; i < Shapes.Count; ++i)
            {
                if (Shapes[i] is StaticShape staticShape)
                    staticShape.AddShapeBlocks(generatedBlocks[i]);
            }    
        }
    }
}