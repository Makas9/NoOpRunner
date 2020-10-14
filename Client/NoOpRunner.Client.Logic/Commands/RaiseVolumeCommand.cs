using NoOpRunner.Client.Logic.Interfaces;
using NoOpRunner.Client.Logic.ViewModels;

namespace NoOpRunner.Client.Logic.Commands
{
    public class RaiseVolumeCommand : ICommand
    {
        private readonly SettingsViewModel settingsViewModel;

        public RaiseVolumeCommand(SettingsViewModel settingsViewModel)
        {
            this.settingsViewModel = settingsViewModel;
        }

        public bool Execute()
        {
            if (PreExecute())
            {
                settingsViewModel.VolumeLevel += 5;

                return true;
            }

            return false;
        }

        public bool PreExecute()
        {
            return settingsViewModel?.VolumeLevel < 100;
        }

        public void Undo()
        {
            settingsViewModel.VolumeLevel -= 5;
        }
    }
}
