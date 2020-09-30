using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Shapes
{
    public class Platform : BaseShape
    {
        public int BottomPosY { get; set; } // Bottom of x platform
        public int TopPosY { get; set; } // Max height of x platform

        public int LastPosX = 0,
                   LastPosY = 0;

        public Platform(int centerPosX, int centerPosY, int bottomPosY, int topPosY) : base(centerPosX, centerPosY)
        {
            while (LastPosX < 28)
            {
                int blockLength = RandomLength(4);
                int blockHeight = RandomHeight(4); // Lower than jump height

                AddShape(true, LastPosX, LastPosY, blockLength);
                LastPosX += blockLength;

                if(blockHeight < 0)
                {
                    AddShape(false, LastPosX, 1, LastPosY);
                    LastPosY = 0;
                } else
                {
                    if (LastPosY + blockHeight > topPosY)
                    {
                        int length = (topPosY - LastPosY) < bottomPosY ? bottomPosY : (topPosY - LastPosY);
                        AddShape(false, LastPosX, LastPosY, length);
                        LastPosY = topPosY;
                    } else
                    {
                        AddShape(false, LastPosX, LastPosY, blockHeight);
                        LastPosY = LastPosY + blockHeight;
                    }
                }
            }
        }

        private void AddShape(bool horizontal, int posX, int posY, int blockLength)
        {
            if (horizontal) 
                MapShapeX(posX, posY, blockLength, Color.Red);
            else
                MapShapeY(posX, posY, blockLength, Color.Red);
        }

        private int RandomHeight(int maxLength)
        {
            int number = RandomNumber.GetInstance().Next(maxLength * -1, maxLength);
            while (number == 0)
            {
                number = RandomNumber.GetInstance().Next(maxLength * -1, maxLength);
            }

            return RandomNumber.GetInstance().Next(maxLength*-1, maxLength);
        }

        private int RandomLength(int maxLength)
        {
            return RandomNumber.GetInstance().Next(2, maxLength);
        }
    }
}
