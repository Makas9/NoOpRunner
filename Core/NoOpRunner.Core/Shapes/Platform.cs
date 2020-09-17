using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;
using System;

namespace NoOpRunner.Core.Shapes
{
    public class Platform : BaseShape
    {
        private Random random = new Random();
        public int roofPosY { get; set; }

        public int lastPosX = 0,
                   lastPosY = 0;

        public Platform(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {

            roofPosY = 10; // Max height of x platform
            while (lastPosX < 30) // TODO
            {
                int blockLength = randomLength(4);
                int blockHeight = randomHeight(4); // Lower than jump height

                MapShapeX(lastPosX, lastPosY, blockLength, Color.Red);
                lastPosX += blockLength;
                if(blockHeight < 0)
                {
                    MapShapeY(lastPosX, 1, lastPosY, Color.Red);
                    lastPosY = 0;
                } else
                {
                    if (lastPosY + blockHeight > roofPosY)
                    {
                        MapShapeY(lastPosX, lastPosY, (roofPosY- lastPosY), Color.Red);
                        lastPosY = roofPosY;
                    } else
                    {
                        MapShapeY(lastPosX, lastPosY, blockHeight, Color.Red);
                        lastPosY = lastPosY + blockHeight;
                    }
                }
            }
        }


        public int randomHeight(int maxLength)
        {
            int number = random.Next(maxLength * -1, maxLength);
            while (number == 0)
            {
                number = random.Next(maxLength * -1, maxLength);
            }

            return random.Next(maxLength*-1, maxLength);
        }

        public int randomLength(int maxLength)
        {
            return random.Next(2, maxLength);
        }
    }
}
