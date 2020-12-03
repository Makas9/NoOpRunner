﻿using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NoOpRunner.Core
{
    /// <summary>
    /// Power ups 
    /// </summary>
    public class PowerUpsContainer : ShapesContainer, IObserver
    {
        private readonly Random random = RandomNumber.GetInstance(); 
        public PowerUpsContainer(int sizeX, int sizeY) : base(sizeX, sizeY)
        {
        }

        public PowerUps? GeneratePowerUpOnMove()
        {
            if (random.NextDouble() > GenerationConstants.PowerUpsSpawnPossibility)
            
                return null;

            var powerUpGuess = random.NextDouble();

            return GenerationConstants.PowerUpsPossibilities
                .FirstOrDefault(x => x.Value > powerUpGuess).Key;
        }

        public IList<Tuple<WindowPixel, PowerUps>> GetPowerUpsEnumerable()
        {
            var shapesPixels = GetWindowsPixelCollection().GetItems();//WHY RETURN REVERSE???? OH MY GOD
            shapesPixels.Reverse();
            
            var shapesPowerUps = Shapes.GetItems().Select(x => ((PowerUp) x).PowerUpType).ToList();

            return shapesPixels.Zip(shapesPowerUps, (shapesPixel, shapesPowerUp)=> new Tuple<WindowPixel, PowerUps>(shapesPixel, shapesPowerUp)).ToList();
        }
        public void RemovePowerUp(int centerPosX, int centerPosY)
        {
            Shapes.GetItems().Remove(GetPowerUpAt(centerPosX, centerPosY));
        }
        public PowerUp GetPowerUpAt(int centerPosX, int centerPosY)
        {
            return Shapes.GetItems().FirstOrDefault(x => x.IsAtPos(centerPosX, centerPosY)) as PowerUp;
        }

        public override void ShiftShapes()
        {
            Logging.Instance.Write($"[Composite/{nameof(PowerUpsContainer)}] {nameof(ShiftShapes)}", LoggingLevel.Composite);

            foreach (IMapPart shape in Shapes)
                shape.ShiftShapes(); //Push cells
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