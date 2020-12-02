using NoOpRunner.Client.Logic.ViewModels;

namespace NoOpRunner.Client.Logic.Interpreter
{
    public class InterpreterContext
    {
        public MainViewModel ViewModel { get; }

        public InterpreterContext(MainViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
