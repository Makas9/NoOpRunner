using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using System.Diagnostics;

namespace NoOpRunner.Core.Shapes
{
    public class Circle : BaseShape
    {
        public Circle(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeX(0, 1, 1, Color.Red);
            MapShapeX(-1, 0, 3, Color.Red);
            MapShapeX(0, -1, 1, Color.Red);
        }
    }
}
