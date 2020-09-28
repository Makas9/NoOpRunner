using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using System.Diagnostics;

namespace NoOpRunner.Core.Shapes
{
    public class Fence : BaseShape
    {
        public Fence(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeY(1, 1, 2, Color.Red);
        }
    }
}
