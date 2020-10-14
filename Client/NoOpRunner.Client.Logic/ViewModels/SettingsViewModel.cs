using NoOpRunner.Client.Logic.Commands;

namespace NoOpRunner.Client.Logic.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly CommandInvoker commandInvoker;

        public SettingsViewModel()
        {
            commandInvoker = new CommandInvoker();
        }

        private int _VolumeLevel = 50;
        public int VolumeLevel
        {
            get => _VolumeLevel;
            set => SetField(ref _VolumeLevel, value);
        }

        public void RaiseVolume()
        {
            commandInvoker.InvokeCommand(new RaiseVolumeCommand(this));
        }

        public void LowerVolume()
        {
            commandInvoker.InvokeCommand(new LowerVolumeCommand(this));
        }

        public void Reset()
        {
            commandInvoker.UndoCommands();
        }

        public void Undo()
        {
            commandInvoker.UndoLastCommand();
        }
    }
}
