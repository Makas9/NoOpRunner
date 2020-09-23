using NoOpRunner.Core.Dtos;
using System;

namespace NoOpRunner.Networking
{
    public class HostBridge
    {
        private HostBridge()
        {

        }

        public static HostBridge Bridge { get; } = new HostBridge();

        private Action<MessageDto> MessageHandler;

        public void RegisterHostMessageHandler(Action<MessageDto> handler)
        {
            MessageHandler = handler;
        }

        public void HandleHostMessage(MessageDto message)
        {
            MessageHandler?.Invoke(message);
        }
    }
}
