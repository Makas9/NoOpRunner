using NoOpRunner.Core.Enums;

namespace NoOpRunner.Core.Interfaces
{
    public interface ISubject
    {
        void Notify(NoOpRunner sender, MessageType observerType, object arg = null);
        void AddObserver(IObserver observer, MessageType observerType);
        void RemoveObserver(IObserver observer, MessageType observerType);
    }
}