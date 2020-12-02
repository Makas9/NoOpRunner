namespace NoOpRunner.Core.Controls
{
    public class InputHandlerImplementorPlayerTwo : InputHandlerImplementor
    {
        public InputHandlerImplementorPlayerTwo()
        {
            Logging.Instance.Write("[InputHandlerImplementorPlayerTwo]: initialized", LoggingLevel.Bridge);
        }

        public override void HandlePower1(WindowPixel[,] gameScreen)
        {
            Logging.Instance.Write("Input Handler Bridge: P2 would trigger a powerup here", LoggingLevel.Bridge);
        }
    }
}
