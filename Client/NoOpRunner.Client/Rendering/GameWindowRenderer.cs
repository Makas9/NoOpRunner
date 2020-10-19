using NoOpRunner.Core;
using NoOpRunner.Core.Shapes;
using System.Windows.Controls;

namespace NoOpRunner.Client.Rendering
{
    public static class GameWindowRenderer
    {
        //Will fix with Facade pattern, just wait
        public static void RenderPowerUps(PowerUpsContainer powerUps, Canvas canvas)
        {
            powerUps.Display(canvas);
        }

        public static void RenderPlatforms(PlatformsContainer platforms, Canvas canvas)
        {
            platforms.Display(canvas);
        }

        public static void RenderPlayer(Player player, Canvas canvas)
        {
            player.Display(canvas);
        }
    }
}