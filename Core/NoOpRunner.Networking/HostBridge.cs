using NoOpRunner.Core.Dtos;
using System;

namespace NoOpRunner.Networking
{
    public class HostBridge
    {
        private HostBridge()
        {

        }

        private static readonly Lazy<HostBridge> lazy = new Lazy<HostBridge>(() => new HostBridge());

        public static HostBridge Bridge => lazy.Value;

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
