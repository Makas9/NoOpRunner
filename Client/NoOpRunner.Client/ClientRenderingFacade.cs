using System.Threading.Tasks;
using System.Windows.Controls;
using NoOpRunner.Core;

namespace NoOpRunner.Client
{
    public class ClientRenderingFacade : RenderingFacade
    {
        public override async Task CycleGameFrames(Core.NoOpRunner game, Canvas playerCanvas, Canvas powerUpsCanvas, Canvas platformsCanvas)
        {
            Logging.Instance.Write("Facade pattern: Client", LoggingLevel.Pattern);
            
            BaseCycle(game, playerCanvas, platformsCanvas, powerUpsCanvas);
        }
    }
}