using NoOpRunner.Core.Interfaces;
using NoOpRunner.Core.Shapes;

namespace NoOpRunner.Core
{
    public class PlayerObserver :IObserver
    {
        public void Update(NoOpRunner sender, object args)
        {
            sender.Player = args as Player;
        }
    }
}