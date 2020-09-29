using NoOpRunner.Core.Dtos;
using System;

namespace NoOpRunner.Networking
{
    public class HostBridge
    {
        private HostBridge()
        {

        }

        private static readonly Lazy<HostBridge> Lazy = new Lazy<HostBridge>(() => new HostBridge());

        public static HostBridge Bridge => Lazy.Value;

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
