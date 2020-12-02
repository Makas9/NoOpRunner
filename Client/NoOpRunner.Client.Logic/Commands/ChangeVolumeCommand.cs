using NoOpRunner.Client.Logic.ViewModels;
using NoOpRunner.Core;

namespace NoOpRunner.Client.Logic.Commands
{
    public sealed class ChangeVolumeCommand : BaseCommand<int>
    {
        private readonly SettingsViewModel settingsViewModel;

        private int previousVolume = 0;

        public ChangeVolumeCommand(SettingsViewModel settingsViewModel)
        {
            this.settingsViewModel = settingsViewModel;
        }

        protected override bool ExecuteInternal(int newVolume)
        {
            Logging.Instance.Write($"Template method step ExecuteInternal called from type {this.GetType()}", LoggingLevel.TemplateMethod);
            previousVolume = settingsViewModel.VolumeLevel;
            settingsViewModel.VolumeLevel = newVolume;

            return true;
        }

        protected override bool PreExecute(int newVolume)
        {
            Logging.Instance.Write($"Template method step PreExecute called from type {this.GetType()}", LoggingLevel.TemplateMethod);
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
