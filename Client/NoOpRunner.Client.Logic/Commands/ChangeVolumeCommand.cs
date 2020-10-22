using NoOpRunner.Client.Logic.ViewModels;

namespace NoOpRunner.Client.Logic.Commands
{
    public class ChangeVolumeCommand : BaseCommand<int>
    {
        private readonly SettingsViewModel settingsViewModel;

        private int previousVolume = 0;

        public ChangeVolumeCommand(SettingsViewModel settingsViewModel)
        {
            this.settingsViewModel = settingsViewModel;
        }

        protected override bool ExecuteInternal(int newVolume)
        {
            previousVolume = settingsViewModel.VolumeLevel;
            settingsViewModel.VolumeLevel = newVolume;

            return true;
        }

        protected override bool PreExecute(int newVolume)
        {
            if (settingsViewModel == null)
                return false;

            if (newVolume < 0)
                return newVolume >= 0;

            return newVolume <= 100;
        }

        public override void Undo()
        {
            settingsViewModel.VolumeLevel = previousVolume;
        }
    }
}
