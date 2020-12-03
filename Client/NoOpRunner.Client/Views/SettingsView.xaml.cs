using NoOpRunner.Client.Logic.ViewModels;
using System.Windows.Controls;

namespace NoOpRunner.Client.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                var dataContext = (SettingsViewModel)DataContext;

                this.volumeSlider.ValueChanged += (o, a) =>
                {
                    dataContext.ChangeVolume((int)((Slider)o).Value);
                };

                this.resetButton.Click += (o, a) =>
                {
                    dataContext.Reset();
                };

                this.undoButton.Click += (o, a) =>
                {
                    dataContext.Undo();
                };

                this.resolutionDropdown.SelectionChanged += (o, a) =>
                {
                    dataContext.ChangeResolution((int)((ComboBox)o).SelectedIndex);
                };

                this.applyButton.Click += (o, a) =>
                {
                    dataContext.ApplySettings();
                };

                this.closeButton.Click += (o, a) =>
                {
                    dataContext.CloseSettings();
                };

                this.saveSnapshotButton.Click += (o, a) =>
                {
                    dataContext.SaveSnapshot();
                };

                this.restoreSnapshotButton.Click += (o, a) =>
                {
                    dataContext.RestoreSnapshot();
                };
            };
        }
    }
}
