using NoOpRunner.Core.Dtos;

namespace NoOpRunner.Core.Interfaces
{
    public interface ISubject
    {
        void Notify(MessageDto message);
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
    }
}