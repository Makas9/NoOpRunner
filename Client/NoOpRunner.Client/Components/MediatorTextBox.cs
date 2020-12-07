using NoOpRunner.Client.Logic.Interfaces;
using System.Windows.Controls;

namespace NoOpRunner.Client.Components
{
    public class MediatorTextBox : TextBox
    {
        public MediatorTextBox(IMediator mediator, string evnt)
        {
            this.TextChanged += async (s, e) =>
            {
                await mediator.Notify(this, evnt);
            };
        }
    }
}
