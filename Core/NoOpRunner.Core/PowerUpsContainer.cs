using System;
using System.Collections.Generic;
using System.Linq;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Core
{
    /// <summary>
    /// Power ups 
    /// </summary>
    public class PowerUpsContainer : ShapesContainer, IObserver
    {
        public PowerUpsContainer(int sizeX, int sizeY) : base(sizeX, sizeY)
        {
        }

        public IList<Tuple<WindowPixel, PowerUps>> GetPowerUpsEnumerable()
        {
            var shapesPixels = GetShapesEnumerable().ToList();
            var shapesPowerUps = shapes.Select(x => ((PowerUp) x).PowerUpType).ToList();

            return shapesPixels.Zip(shapesPowerUps, (shapesPixel, shapesPowerUp)=> new Tuple<WindowPixel, PowerUps>(shapesPixel, shapesPowerUp)).ToList();
        }
        public void RemovePowerUp(int centerPosX, int centerPosY)
        {
            shapes.Remove(GetPowerUpAt(centerPosX, centerPosY));
        }
        public PowerUp GetPowerUpAt(int centerPosX, int centerPosY)
        {
            return shapes.FirstOrDefault(x => x.CenterPosX == centerPosX && x.CenterPosY == centerPosY) as PowerUp;
        }

        public override void MoveWithMap()
        {
            shapes.ForEach(x => x.CenterPosX--); //Push cells

            shapes.RemoveAll(x => x.CenterPosX < 0); //Remove out of bounds
        }

        /// <summary>
        /// Push power ups, remove out of bounds and add generated
        /// </summary>
        /// <param name="message"></param>
        public void Update(MessageDto message)
        {
            if (message.MessageType != MessageType.PowerUpsUpdate) 
                return;
            
            Console.WriteLine("Observer: PowerUpsContainer, say Hello World");

            MoveWithMap();

            shapes.AddRange(message.Payload as List<BaseShape>); //Append generated power ups
        }
    }
}