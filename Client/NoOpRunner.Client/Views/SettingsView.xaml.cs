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

            this.volumeSlider.ValueChanged += (o, a) =>
            {
                var dataContext = (SettingsViewModel)DataContext;

                dataContext.ChangeVolume((int)((Slider)o).Value);
            };

            this.resetButton.Click += (o, a) =>
            {
                var dataContext = (SettingsViewModel)DataContext;

                dataContext.Reset();
            };

            this.undoButton.Click += (o, a) =>
            {
                var dataContext = (SettingsViewModel)DataContext;

                dataContext.Undo();
            };

            this.resolutionDropdown.SelectionChanged += (o, a) =>
            {
                var dataContext = (SettingsViewModel)DataContext;

                dataContext.ChangeResolution((int)((ComboBox)o).SelectedIndex);
            };

            this.applyButton.Click+= (o, a) =>
            {
                var dataContext = (SettingsViewModel)DataContext;

                dataContext.ApplySettings();
            };

            this.closeButton.Click += (o, a) =>
            {
                var dataContext = (SettingsViewModel)DataContext;

                dataContext.CloseSettings();
            };
        }
    }
}
