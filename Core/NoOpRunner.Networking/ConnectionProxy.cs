using NoOpRunner.Core;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace NoOpRunner.Networking
{
    public class ConnectionProxy : IConnectionManager
    {
        private LoggingConnectionManagerAdapter realSubject;

        public ConnectionProxy(LoggingConnectionManagerAdapter realSubject)
        {
            this.realSubject = realSubject;
        }

        public async Task Connect(string url, Action<MessageDto> callback)
        {
            if(CheckAccess())
            {
                LogAccess();

                await realSubject.Connect(url, callback);
            }
        }

        public async Task SendMessageToClient(MessageDto message)
        {
            if (CheckAccess())
            {
                LogAccess();

                await realSubject.SendMessageToClient(message);
            }
        }

        public async Task SendMessageToHost(MessageDto message)
        {
            if (CheckAccess())
            {
                LogAccess();

                await realSubject.SendMessageToHost(message);
            }
        }

        public IDisposable Start(string url, Action<MessageDto> callback)
        {
            if (CheckAccess())
            {
                LogAccess();

                return realSubject.Start(url, callback);
            }

            return null;
        }

        public bool CheckAccess()
        {
            Logging.Instance.Write("[Proxy]: Checking proxy access", LoggingLevel.Proxy);

            return true;
        }

        public void LogAccess()
        {
            Logging.Instance.Write("[Proxy]: Request went through proxy", LoggingLevel.Proxy);
        }
    }
}
