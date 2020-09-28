using NoOpRunner.Core.Entities;
using NoOpRunner.Core.Enums;
using NoOpRunner.Core.Interfaces;
using System.Diagnostics;

namespace NoOpRunner.Core.Shapes
{
    public class Stone : BaseShape
    {
        public Stone(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {
            MapShapeX(1, 1, 1, Color.Red);
        }
    }
}
