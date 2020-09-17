namespace NoOpRunner.Client.Logic.ViewModels
{
    public class MainViewModel
    {
        public Core.NoOpRunner Game { get; set; }

        public MainViewModel()
        {
            Game = new Core.NoOpRunner();
        }
    }
}
 