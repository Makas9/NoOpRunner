using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;
using System;

namespace NoOpRunner.Core.Shapes
{
    public class Platform : BaseShape
    {
        private Random random = new Random();
        public int bottomPosY { get; set; } // Bottom of x platform
        public int topPosY { get; set; } // Max height of x platform

        public int lastPosX = 0,
                   lastPosY = 0;

        public Platform(int centerPosX, int centerPosY, int bottomPosY, int topPosY) : base(centerPosX, centerPosY)
        {
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
                    if (lastPosY + blockHeight > topPosY)
                    {
                        MapShapeY(lastPosX, lastPosY, (topPosY - lastPosY), Color.Red);
                        lastPosY = topPosY;
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
