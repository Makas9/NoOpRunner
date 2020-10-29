using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using NoOpRunner.Core.Dtos;
using NoOpRunner.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace NoOpRunner.Networking
{
    public class ConnectionManager : IConnectionManager
    {
        private IHubProxy proxy;

        private HubConnection connection;

        public IDisposable Start(string url, Action<MessageDto> callback)
        {
            HostBridge.Bridge.RegisterHostMessageHandler(callback);

            return WebApp.Start<Startup>(url);
        }

        public async Task Connect(string url, Action<MessageDto> callback)
        {
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
            await proxy.Invoke("SendToHost", message);
        }

        public async Task SendMessageToClient(MessageDto message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<GameHub>();

            await context.Clients.All.SendMessage(JsonConvert.SerializeObject(message, new JsonSerializerSettings 
            {
                TypeNameHandling = TypeNameHandling.Auto
            }));
        }
    }
}
