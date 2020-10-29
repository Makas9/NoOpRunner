using Microsoft.AspNet.SignalR;
using NoOpRunner.Core.Dtos;
using System.Threading.Tasks;

namespace NoOpRunner.Networking
{
    public class GameHub : Hub
    {
        public void SendToHost(MessageDto message)
        {
            HostBridge.Bridge.HandleHostMessage(message);
        }

        public async Task SendToClient(MessageDto message)
        {
            await Clients.AllExcept(Context.ConnectionId).SendMessage(message);
        }
    }
}
