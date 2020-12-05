using NoOpRunner.Core.Dtos;

namespace NoOpRunner.Core.Interfaces
{
    public interface IMapMediator
    {
        void Notify(object sender, MediatorMessageDto message);
    }
}
