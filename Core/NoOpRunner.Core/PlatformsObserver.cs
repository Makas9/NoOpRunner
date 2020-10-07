using NoOpRunner.Core.Interfaces;

namespace NoOpRunner.Core
{
    public class PlatformsObserver : IObserver
    {
        public void Update(NoOpRunner sender, object args)
        {
            sender.GamePlatforms = args as GamePlatforms;
        }
    }
}