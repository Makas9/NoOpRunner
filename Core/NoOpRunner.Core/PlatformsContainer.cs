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
            Shapes.ForEach(x => x.ShiftBlocks()); //Push and remove out of bounds
        }

        public List<List<ShapeBlock>> GetNextBlocks()
        {
            return Shapes.Select(x => x.GetNextBlocks()).ToList();
        }

        /// <summary>
        /// For platforms update from client side, append generated cells
        /// </summary>
        /// <param name="message"></param>
        public void Update(MessageDto message)
        {
            if (message.MessageType != MessageType.PlatformsUpdate)
                return;

            Console.WriteLine("Observer: PlatformsContainer, say Hello World");

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