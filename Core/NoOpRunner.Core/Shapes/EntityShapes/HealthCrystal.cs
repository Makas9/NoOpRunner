﻿using System;
using NoOpRunner.Core.Shapes.GenerationStrategies;

namespace NoOpRunner.Core.Shapes.EntityShapes
{
    class HealthCrystal : EntityShape
    {
        public HealthCrystal(int x, int y) : base(new FillGenerationStrategy(), x, y, x + 1, y + 2) { }
    }
}
