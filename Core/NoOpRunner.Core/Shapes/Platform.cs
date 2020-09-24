using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;
using System;
using System.Collections.Generic;

namespace NoOpRunner.Core.Shapes
{
    public class Platform : BaseShape
    {
        public int bottomPosY { get; set; } // Bottom of x platform
        public int topPosY { get; set; } // Max height of x platform

        private Random randomNumber;

        public int lastPosX = 0,
                   lastPosY = 0;

        private List<int> xCoords = new List<int>();
        private List<int> yCoords = new List<int>();

        public Platform(int centerPosX, int centerPosY, int bottomPosY, int topPosY, Random random) : base(centerPosX, centerPosY)
        {
            randomNumber = random;

            while (lastPosX < 30) // TODO
            {
                int blockLength = randomLength(4);
                int blockHeight = randomHeight(3); // Lower than jump height

                addShape(true, lastPosX, lastPosY, blockLength);

                lastPosX += blockLength;
                if (blockHeight < 0)
                {
                    addShape(false, lastPosX, 1, lastPosY);
                    lastPosY = 0;
                } else
                {
                    if (lastPosY + blockHeight > topPosY)
                    {
                        addShape(false, lastPosX, lastPosY, (topPosY - lastPosY) < bottomPosY ? bottomPosY : (topPosY - lastPosY));
                        lastPosY = topPosY;
                    } else
                    {
                        addShape(false, lastPosX, lastPosY, blockHeight);
                        lastPosY = lastPosY + blockHeight;
                    }
                }
            }
        }

        public void addShape(bool horizontal, int posX, int posY, int blockLength)
        {
            if(horizontal) MapShapeX(posX, posY, blockLength, Color.Red);
            else MapShapeY(posX, posY, blockLength, Color.Red);

            for(int i = 0; i < blockLength; i++)
            {
                if (horizontal)
                {
                    xCoords.Add(posX + i);
                    yCoords.Add(posY);
                }
                else {
                    xCoords.Add(posX);
                    yCoords.Add(posY + i);
                }
            }
        }

        public int[] getCoordsX()
        {
            return xCoords.ToArray();
        }

        public int[] getCoordsY()
        {
            return yCoords.ToArray();
        }


        public int randomHeight(int maxLength)
        {
            int number = randomNumber.Next(maxLength * -1, maxLength);
            while (number == 0)
            {
                number = randomNumber.Next(maxLength * -1, maxLength);
            }

            return randomNumber.Next(maxLength * -1, maxLength);
        }

        public int randomLength(int maxLength)
        {
            return randomNumber.Next(2, maxLength);
        }
    }
}
