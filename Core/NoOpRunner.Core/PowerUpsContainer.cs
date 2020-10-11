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

        public void ClickShape(int x, int y)
        {
            shapes.FirstOrDefault(s => s.IsHit(x, y))?.OnClick();
        }

        /// <summary>
        /// Push power ups, remove out of bounds and add generated
        /// </summary>
        /// <param name="message"></param>
        public void Update(MessageDto message)
        {
            if (message.MessageType == MessageType.PowerUpsUpdate)
            {
                shapes.ForEach(x => x.CenterPosX--); //Push cells

                //Will need in the future, update player two instance
                var pickedUpPowerUps = shapes.Where(x => x.CenterPosX < 0);
                
                shapes.RemoveAll(x => x.CenterPosX < 0); //Remove out of bounds

                shapes.AddRange(message.Payload as List<BaseShape>); //Append generated power ups
            }
        }
    }
}