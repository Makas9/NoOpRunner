using System;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Core
{
    public class PlatformsContainer : ShapesContainer, IObserver
    {
        public PlatformsContainer(int sizeX, int sizeY) : base(sizeX, sizeY)
        {
        }

        public override void ShiftPlatforms()
        {
            shapes.ForEach(x =>
                ((StaticShape) x).ShiftAndUpdate()
            ); //Push and remove out of bounds
        }

        /// <summary>
        /// For platforms update, append generated cells
        /// </summary>
        /// <param name="message"></param>
        public void Update(MessageDto message)
        {
            if (message.MessageType != MessageType.PlatformsUpdate)
                return;

            Console.WriteLine("Observer: PlatformsContainer, say Hello World");

            ShiftPlatforms();            
        }
    }
}