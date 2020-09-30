﻿using NoOpRunner.Core.Entities;
using System.Diagnostics;

namespace NoOpRunner.Core.Shapes
{
    public abstract class EntityShape : BaseShape
    {
        public EntityShape(int centerPosX, int centerPosY) : base(centerPosX, centerPosY)
        {

        }
    }
}