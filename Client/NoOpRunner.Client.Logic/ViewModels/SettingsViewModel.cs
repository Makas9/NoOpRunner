using NoOpRunner.Client.Logic.Commands;
using NoOpRunner.Client.Logic.Dto;
using NoOpRunner.Client.Logic.Interfaces;
using System;
using System.Collections.Generic;

namespace NoOpRunner.Client.Logic.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly CommandInvoker<BaseCommand> commandInvoker;
        private readonly MainViewModel mainViewModel;
        private readonly MementoCaretaker mementoCaretaker;

        public SettingsViewModel(MainViewModel viewModel)
        {
            commandInvoker = new CommandInvoker<BaseCommand>();
            mainViewModel = viewModel;
            mementoCaretaker = new MementoCaretaker();
        }

        private int volumeLevel = 20;//FUCK YOU
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

        public IMemento CreateMemento()
        {
            return new Memento(this);
        }

        public void SetMemento(IMemento m)
        {
            if (!(m is Memento mem))
                throw new ArgumentException("The provided memento did not originate this originator");

            VolumeLevel = mem.VolumeLevel;
            SelectedResolutionIndex = mem.SelectedResolutionIndex;
        }

        public void SaveSnapshot()
        {
            mementoCaretaker.AddMemento(CreateMemento());
        }

        public void RestoreSnapshot()
        {
            var lastMemento = mementoCaretaker.GetLastMemento();
            if (lastMemento != null)
                SetMemento(lastMemento);
        }

        private class Memento : IMemento
        {
            public int VolumeLevel { get; }
            public int SelectedResolutionIndex { get; }

            public DateTime CreationTime { get; } // Just to have some public metadata

            public Memento(SettingsViewModel settings)
            {
                // Private fields are accessed here instead of the public properties to showcase that the memento has access to the settings' internal state
                VolumeLevel = settings.volumeLevel;
                SelectedResolutionIndex = settings.selectedResolutionIndex;
                CreationTime = DateTime.Now;
            }
        }
    }
}
