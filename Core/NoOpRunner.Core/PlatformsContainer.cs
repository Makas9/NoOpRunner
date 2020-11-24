using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoOpRunner.Core
{
    public class PlatformsContainer : ShapesContainer, IObserver
    {
        public PlatformsContainer(int sizeX, int sizeY) : base(sizeX, sizeY)
        {
        }

        public override void ShiftShapes()
        {
            foreach (BaseShape shape in Shapes)
                shape.ShiftBlocks(); //Push and remove out of bounds
        }

        public List<List<ShapeBlock>> GetNextBlocks()
        {
            List<List<ShapeBlock>> result = new List<List<ShapeBlock>>();

            var iterator = Shapes.GetEnumerator();
            while (iterator.MoveNext())
            {
                BaseShape shape = (BaseShape)iterator.Current;
                result.Add(shape.GetNextBlocks());
            }

            return result;
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

            var shapes = Shapes.GetItems();
            for (int i = 0; i < shapes.Count; ++i)
                if (shapes[i] is StaticShape staticShape)
                    staticShape.AddShapeBlocks(generatedBlocks[i]);
        }
    }
}