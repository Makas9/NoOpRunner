namespace NoOpRunner.Core.Interfaces
{
    public interface ISubject
    {
        void Notify(object sender, object observerType = null, object arg = null);
        void AddObserver(IObserver observer, object arg = null);
        void RemoveObserver(IObserver observer, object arg = null);
    }
}