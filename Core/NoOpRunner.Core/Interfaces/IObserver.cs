namespace NoOpRunner.Core.Interfaces
{
    public interface IObserver
    {
        void Update(NoOpRunner sender, object args);
    }
}