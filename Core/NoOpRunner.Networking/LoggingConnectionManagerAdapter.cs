using NoOpRunner.Core;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace NoOpRunner.Networking
{
    public class LoggingConnectionManagerAdapter : IConnectionManager
    {
        private LoggingConnectionManager adaptee;
        public LoggingConnectionManagerAdapter(LoggingConnectionManager adaptee)
        {
            this.adaptee = adaptee;
        }

        public async Task Connect(string url, Action<MessageDto> callback)
        {
            Logging.Instance.Write("[LoggingConnectionManagerAdapter]: Connect called and delegated to adaptee", LoggingLevel.Pattern);
            await adaptee.CreateConnection(url, callback);
        }

        public async Task SendMessageToClient(MessageDto message)
        {
            await adaptee.SendMessageToClient(message);
        }

        public async Task SendMessageToHost(MessageDto message)
        {
            await adaptee.SendMessageToHost(message);
        }

        public IDisposable Start(string url, Action<MessageDto> callback)
        {
            return adaptee.StartConnectionManager(url, callback);
        }
    }
}
