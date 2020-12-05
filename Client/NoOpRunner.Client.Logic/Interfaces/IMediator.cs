using System.Threading.Tasks;

namespace NoOpRunner.Client.Logic.Interfaces
{
    public interface IMediator
    {
        Task Notify(object sender, string evnt);
    }
}
