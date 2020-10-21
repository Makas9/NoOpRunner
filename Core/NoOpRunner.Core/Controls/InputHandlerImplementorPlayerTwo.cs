namespace NoOpRunner.Core.Controls
{
    public class InputHandlerImplementorPlayerTwo : InputHandlerImplementor
    {
        public override void HandlePower1(WindowPixel[,] gameScreen)
        {
            Logging.Instance.Write("Input Handler Bridge: P2 would trigger a powerup here", LoggingLevel.Pattern);
        }
    }
}
