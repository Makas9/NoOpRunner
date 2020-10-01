using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes
{
    public abstract class StaticShape : BaseShape
    {
        public int bottomPosY { get; set; } // Bottom of x platform
        public int topPosY { get; set; } // Max height of x platform

        public int lastPosX = 0,
                   lastPosY = 0;

        public StaticShape(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            while (lastPosX < 28)
            {
                int blockLength = RandomLength(4);
                int blockHeight = RandomHeight(4); // Lower than jump height

                AddShape(true, lastPosX, lastPosY, blockLength);
                lastPosX += blockLength;

                if (blockHeight < 0)
                {
                    AddShape(false, lastPosX, 1, lastPosY);
                    lastPosY = 0;
                }
                else
                {
                    if (lastPosY + blockHeight > topPosY)
                    {
                        int length = (topPosY - lastPosY) < bottomPosY ? bottomPosY : (topPosY - lastPosY);
                        AddShape(false, lastPosX, lastPosY, length);
                        lastPosY = topPosY;
                    }
                    else
                    {
                        AddShape(false, lastPosX, lastPosY, blockHeight);
                        lastPosY = lastPosY + blockHeight;
                    }
                }
            }
        }

        protected void AddShape(bool horizontal, int posX, int posY, int blockLength)
        {
            if (horizontal)
                MapShapeX(posX, posY, blockLength, Color.Red);
            else
                MapShapeY(posX, posY, blockLength, Color.Red);
        }

        protected int RandomHeight(int maxLength)
        {
            int number = RandomNumber.GetInstance().Next(maxLength * -1, maxLength);
            while (number == 0)
            {
                number = RandomNumber.GetInstance().Next(maxLength * -1, maxLength);
            }

            return RandomNumber.GetInstance().Next(maxLength * -1, maxLength);
        }

        protected int RandomLength(int maxLength)
        {
            return RandomNumber.GetInstance().Next(2, maxLength);
        }
    }
}
