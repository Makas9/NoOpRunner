using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using System.Diagnostics;

namespace NoOpRunner.Core.Shapes
{
    public class Stairs : BaseShape
    {
        public Stairs(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeX(1, 1, 1, Color.Red);
            MapShapeX(0, 0, 2, Color.Red);
            MapShapeX(-1, -1, 3, Color.Red);
        }
    }
}
