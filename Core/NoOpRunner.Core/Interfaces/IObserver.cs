using NoOpRunner.Core.Dtos;

namespace NoOpRunner.Core.Interfaces
{
    public interface IObserver
    {
        void Update(MessageDto message);
    }
}