using NoOpRunner.Client.Logic.Interfaces;
using System.Windows.Controls;

namespace NoOpRunner.Client.Components
{
    public class MediatorButton : Button
    {
        public MediatorButton(IMediator mediator, string evnt)
        {
            this.Click += async (s, e) =>
            {
                await mediator.Notify(this, evnt);
            };
        }
    }
}
