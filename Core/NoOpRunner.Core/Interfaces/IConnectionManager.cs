using NoOpRunner.Core.Dtos;
using System;
using System.Threading.Tasks;

namespace NoOpRunner.Core.Interfaces
{
    public interface IConnectionManager
    {
        IDisposable Start(string url);

        Task Connect(string url, Action<MessageDto> callback);

        Task SendMessageToHost(MessageDto message);

        Task SendMessageToClient(MessageDto message);
    }
}
