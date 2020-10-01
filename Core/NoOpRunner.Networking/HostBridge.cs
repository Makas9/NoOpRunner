using NoOpRunner.Core;
using NoOpRunner.Core.Dtos;
using System;

namespace NoOpRunner.Networking
{
    public sealed class HostBridge
    {
        private HostBridge() { }

        private static HostBridge bridge;
        private static readonly object bolt = new object();

        public static HostBridge Bridge 
        {
            get
            {
                if (bridge == null)
                {
                    lock (bolt)
                    {
                        if (bridge == null)
                        {
                            bridge = new HostBridge();
                            Logging.Instance.Write("HostBridge initialized");
                        }
                    }
                }

                return bridge;
            }
        }

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
