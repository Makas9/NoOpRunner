using System.Collections.Generic;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Entities;

namespace NoOpRunner.Core
{
    public class PlatformsContainer : ShapesContainer, IObserver
    {
        public PlatformsContainer(int sizeX, int sizeY) : base(sizeX, sizeY)
        {
        }

        /// <summary>
        /// For platforms update, append generated cells
        /// </summary>
        /// <param name="message"></param>
        public void Update(MessageDto message)
        {
            if (message.MessageType != MessageType.PlatformsUpdate)
                return;

            var platformsColumn = message.Payload as IList<IList<ShapeBlock>>;

            shapes.ForEach(x =>
                ((StaticShape) x).PushAndRemove()
            );//Push and remove out of bounds

            for (int i = 0; i < shapes.Count; i++)
            {
                ((StaticShape) shapes[i]).AppendPlatform(platformsColumn[i]);//Append platforms with generated cells
            }
            
        }
    }
}