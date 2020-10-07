using System;
using NoOpRunner.Core.Interfaces;

namespace NoOpRunner.Core
{
    public class PlayerPositionObserver : IObserver
    {
        public void Update(NoOpRunner sender, object args)
        {
            var (item1, item2) = (ValueTuple<int, int>) args;
            
            sender.Player.CenterPosX = item1;
            sender.Player.CenterPosY = item2;
        }
    }
}