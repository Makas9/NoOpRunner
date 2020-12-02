using NoOpRunner.Client.Logic.Dto;
using NoOpRunner.Client.Logic.ViewModels;
using NoOpRunner.Core;
using System.Collections.Generic;
using System.Linq;

namespace NoOpRunner.Client.Logic.Commands
{
    public sealed class ChangeResolutionCommand : BaseCommand<int>
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
            Logging.Instance.Write($"Template method step PreExecute called from type {this.GetType()}", LoggingLevel.TemplateMethod);
            return options != null && options.Any() && options.Count > request;
        }

        protected override bool ExecuteInternal(int request)
        {
            Logging.Instance.Write($"Template method step ExecuteInternal called from type {this.GetType()}", LoggingLevel.TemplateMethod);
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
