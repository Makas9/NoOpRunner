namespace NoOpRunner.Client.Logic.Interfaces
{
    public interface ICommand
    {
        bool Execute();

        void Undo();
    }

    public interface ICommand<TRequest> : ICommand
    {
        bool Execute(TRequest request);
    }
}
