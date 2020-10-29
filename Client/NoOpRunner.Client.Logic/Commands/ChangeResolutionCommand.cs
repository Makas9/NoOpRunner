using NoOpRunner.Client.Logic.Dto;
using NoOpRunner.Client.Logic.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace NoOpRunner.Client.Logic.Commands
{
    public class ChangeResolutionCommand : BaseCommand<int>
    {
        private readonly SettingsViewModel settings;
        private readonly List<ResolutionOptionDto> options;

        private int previousValue = 0;

        public ChangeResolutionCommand(SettingsViewModel settings, List<ResolutionOptionDto> options)
        {
            this.settings = settings;
            this.options = options;
        }

        protected override bool PreExecute(int request)
        {
            return options != null && options.Any() && options.Count > request;
        }

        protected override bool ExecuteInternal(int request)
        {
            previousValue = settings.SelectedResolutionIndex;

            settings.SelectedResolutionIndex = request;

            return true;
        }

        public override void Undo()
        {
            settings.SelectedResolutionIndex = previousValue;
        }
    }
}
