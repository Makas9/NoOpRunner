using NoOpRunner.Core;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NoOpRunner.Client
{
    public class HostRenderingFacade : RenderingFacade
    {
        public override async Task CycleGameFrames(Core.NoOpRunner game, Canvas playerCanvas, Canvas powerUpsCanvas,
            Canvas platformsCanvas)
        {
            Logging.Instance.Write("Facade pattern: Host", LoggingLevel.Facade);
            
            if (CountBetweenFrames == GameSettings.TimeBetweenMapStep)
            {
                CountBetweenFrames = 0;

                await game.OnMapMoveLoopFired();
            }

            CountBetweenFrames++;

            game.Player.OnLoopFired((WindowPixel[,]) game.PlatformsContainer.RenderPixels().Clone());

            await game.UpdateClientsGame();

            BaseCycle(game, playerCanvas, platformsCanvas, powerUpsCanvas);
        }
    }
}