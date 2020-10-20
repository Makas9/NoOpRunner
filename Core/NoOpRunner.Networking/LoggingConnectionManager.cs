using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using NoOpRunner.Core;
using NoOpRunner.Core.Dtos;
using System;
using System.Threading.Tasks;


namespace NoOpRunner.Networking
{
    // Pretend that we can't just refactor this to implement IConnectionManager
    public class LoggingConnectionManager
    {
        private IHubProxy proxy;

        private HubConnection connection;

        private ILogger logger;

        public LoggingConnectionManager(ILogger logger)
        {
            this.logger = logger;
        }

        public IDisposable StartConnectionManager(string url, Action<MessageDto> callback)
        {
            logger.Write("ConnectionManager: Start triggered", LoggingLevel.Trace);
            HostBridge.Bridge.RegisterHostMessageHandler(callback);

            return WebApp.Start<Startup>(url);
        }

        public async Task CreateConnection(string url, Action<MessageDto> callback)
        {
            logger.Write("ConnectionManager: Create Connection triggered.", LoggingLevel.Trace);
            connection = connection ?? new HubConnection(url);

            proxy = proxy ?? connection.CreateHubProxy(nameof(GameHub));

            proxy.On<string>("SendMessage", messageJson =>
            {
                callback(JsonConvert.DeserializeObject<MessageDto>(messageJson, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                }));
            });

            await connection.Start();

            await proxy.Invoke("SendToHost", new MessageDto { MessageType = Core.Enums.MessageType.InitialConnection });
        }

        public async Task SendMessageToHost(MessageDto message)
        {
            logger.Write("ConnectionManager: Message sent to Host", LoggingLevel.Trace);
            await proxy.Invoke("SendToHost", message);
        }

        public async Task SendMessageToClient(MessageDto message)
        {
            logger.Write("ConnectionManager: Message sent to Client", LoggingLevel.Trace);
            var context = GlobalHost.ConnectionManager.GetHubContext<GameHub>();

            await context.Clients.All.SendMessage(JsonConvert.SerializeObject(message, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            }));
        }

        public void ChangeLogger(ILogger logger)
        {
            this.logger = logger;
        }
    }
}
