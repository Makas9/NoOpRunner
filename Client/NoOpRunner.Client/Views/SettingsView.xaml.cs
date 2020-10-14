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

            this.lowerVolumeButton.Click += (o, a) =>
            {
                var dataContext = (SettingsViewModel)DataContext;

                dataContext.LowerVolume();
            };

            this.raiseVolumeButton.Click += (o, a) =>
            {
                var dataContext = (SettingsViewModel)DataContext;

                dataContext.RaiseVolume();
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
        }
    }
}
