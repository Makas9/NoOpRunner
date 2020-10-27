namespace NoOpRunner.Core.Controls
{
    public abstract class InputHandlerImplementor
    {
        public virtual void HandleLeft(WindowPixel[,] gameScreen) { }
        public virtual void HandleRight(WindowPixel[,] gameScreen) { }
        public virtual void HandleUp(WindowPixel[,] gameScreen) { }
        public virtual void HandleDown(WindowPixel[,] gameScreen) { }
        public virtual void HandlePower1(WindowPixel[,] gameScreen) { }
        public virtual void HandlePower2(WindowPixel[,] gameScreen) { }
        public virtual void HandlePower3(WindowPixel[,] gameScreen) { }

        public virtual void HandleLeftRelease(WindowPixel[,] gameScreen) { }
        public virtual void HandleRightRelease(WindowPixel[,] gameScreen) { }
        public virtual void HandleUpRelease(WindowPixel[,] gameScreen) { }
        public virtual void HandleDownRelease(WindowPixel[,] gameScreen) { }
    }
}
