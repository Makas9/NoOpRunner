using NoOpRunner.Client.Logic.Commands;
using NoOpRunner.Client.Logic.Dto;
using System.Collections.Generic;

namespace NoOpRunner.Client.Logic.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly CommandInvoker<BaseCommand> commandInvoker;
        private readonly MainViewModel mainViewModel;

        public SettingsViewModel(MainViewModel viewModel)
        {
            commandInvoker = new CommandInvoker<BaseCommand>();
            mainViewModel = viewModel;
        }

        private int volumeLevel = 0;
        public int VolumeLevel
        {
            get => volumeLevel;
            set
            {
                SetField(ref volumeLevel, value);
                RaisePropertyChanged(nameof(VolumeLevelDisplay));
            }
        }

        public int VolumeLevelDisplay
        {
            get => VolumeLevel;
        }

        private int selectedResolutionIndex = 0;
        public int SelectedResolutionIndex
        {
            get => selectedResolutionIndex;
            set
            {
                SetField(ref selectedResolutionIndex, value);
                RaisePropertyChanged(nameof(SelectedResolutionIndexDisplay));
            }
        }

        public int SelectedResolutionIndexDisplay
        {
            get => SelectedResolutionIndex;
        }

        public List<ResolutionOptionDto> ResolutionOptions
            => new List<ResolutionOptionDto>
            {
                new ResolutionOptionDto { Id = 0, Width=800, Height=600 },
                new ResolutionOptionDto { Id = 1, Width=1024,Height=768 },
                new ResolutionOptionDto { Id = 2, Width=1366, Height=768 },
                new ResolutionOptionDto { Id = 3, Width=1440, Height=900 },
                new ResolutionOptionDto { Id = 4, Width=1600, Height=900 },
                new ResolutionOptionDto { Id = 5, Width=1920, Height=1080 }
            };

        public void ChangeVolume(int newValue)
        {
            if (VolumeLevel != newValue)
                commandInvoker.InvokeCommand(new ChangeVolumeCommand(this), newValue);
        }

        public void ChangeResolution(int newValue)
        {
            if (SelectedResolutionIndex != newValue)
                commandInvoker.InvokeCommand(new ChangeResolutionCommand(this, ResolutionOptions), newValue);
        }

        public void Reset()
        {
            commandInvoker.UndoCommands();
        }

        public void Undo()
        {
            commandInvoker.UndoLastCommand();
        }

        public void ApplySettings()
        {
            mainViewModel.ScreenWidth = ResolutionOptions[SelectedResolutionIndex].Width;
            mainViewModel.ScreenHeight = ResolutionOptions[SelectedResolutionIndex].Height;

            commandInvoker.Reset();
        }

        public void CloseSettings()
        {
            commandInvoker.UndoCommands();

            mainViewModel.IsSettingsViewOpen = false;
        }
    }
}
