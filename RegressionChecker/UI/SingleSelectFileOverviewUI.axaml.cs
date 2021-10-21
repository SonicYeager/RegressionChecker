using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RegressionCheckerLogic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RegressionChecker
{
    public partial class SingleSelectFileOverviewUI : UserControl, ISingleSelectFileOverviewUI, INotifyPropertyChanged
    {
        public event OnSingleFilePathSelection onSingleFilePathSelection;
        public event OnOpenAddFilePathDialog onOpenFilePathSelection;

        new public event PropertyChangedEventHandler PropertyChanged;

        ObservableCollection<PathViewModel> latestPaths = new ObservableCollection<PathViewModel>();
        public ObservableCollection<PathViewModel> LatestPaths
        {
            get => LatestPaths;
            set => this.RaiseAndSetIfChanged(ref latestPaths, value);
        }
        PathViewModel selectedLatestPaths;
        public PathViewModel SelectedLatestPaths
        {
            get => selectedLatestPaths;
            set => this.RaiseAndSetIfChanged(ref selectedLatestPaths, value);
        }

        public SingleSelectFileOverviewUI()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public async Task SelectLatFileCommand()
        {
            onOpenFilePathSelection.Invoke();
        }

        protected bool RaiseAndSetIfChanged<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                RaisePropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void AddFilePath(string path)
        {
            LatestPaths.Add(new PathViewModel(){Path=path});
        }
    }
}
