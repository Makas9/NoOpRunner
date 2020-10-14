namespace NoOpRunner.Client.Logic.Interfaces
{
    public interface ICommand
    {
        bool PreExecute();

        bool Execute();

        void Undo();
    }
}
