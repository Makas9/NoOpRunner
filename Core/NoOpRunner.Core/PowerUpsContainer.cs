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
            var shapesPixels = GetWindowsPixelCollection().GetItems();
            var shapesPowerUps = Shapes.GetItems().Select(x => ((PowerUp) x).PowerUpType);

            return shapesPixels.Zip(shapesPowerUps, (shapesPixel, shapesPowerUp)=> new Tuple<WindowPixel, PowerUps>(shapesPixel, shapesPowerUp)).ToList();
        }
        public void RemovePowerUp(int centerPosX, int centerPosY)
        {
            Shapes.GetItems().Remove(GetPowerUpAt(centerPosX, centerPosY));
        }
        public PowerUp GetPowerUpAt(int centerPosX, int centerPosY)
        {
            return Shapes.GetItems().FirstOrDefault(x => x.CenterPosX == centerPosX && x.CenterPosY == centerPosY) as PowerUp;
        }

        public override void ShiftShapes()
        {
            foreach (BaseShape shape in Shapes)
                shape.CenterPosX--; //Push cells

            Shapes.GetItems().RemoveAll(x => x.CenterPosX < 0); //Remove out of bounds
        }

        /// <summary>
        /// Push power ups, remove out of bounds and add generated
        /// </summary>
        /// <param name="message"></param>
        public void Update(MessageDto message)
        {
            if (message.MessageType != MessageType.PowerUpsUpdate) 
                return;

            Logging.Instance.Write("Observer: power ups got update", LoggingLevel.Pattern);

            ShiftShapes();

            if (message.Payload !=null)
                Shapes.GetItems().AddRange(message.Payload as List<BaseShape>); //Append generated power ups    
        }
    }
}