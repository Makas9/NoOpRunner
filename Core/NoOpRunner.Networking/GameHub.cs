using Microsoft.AspNet.SignalR;
using NoOpRunner.Core.Dtos;
using System.Threading.Tasks;

namespace NoOpRunner.Networking
{
    public class GameHub : Hub
    {
        public async Task SendToHost(MessageDto message)
        {
            // TODO
        }

        public async Task SendToClient(MessageDto message)
        {
            await Clients.AllExcept(Context.ConnectionId).SendMessage(message);
        }
    }
}
