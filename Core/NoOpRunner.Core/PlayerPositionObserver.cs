using System;
using NoOpRunner.Core.Interfaces;

namespace NoOpRunner.Core
{
    public class PlayerPositionObserver : IObserver
    {
        public void Update(object sender, object args)
        {
            var (item1, item2) = (ValueTuple<int, int>) args;
            
            ((NoOpRunner) sender).Player.CenterPosX = item1;
            ((NoOpRunner) sender).Player.CenterPosY = item2;
        }
    }
}