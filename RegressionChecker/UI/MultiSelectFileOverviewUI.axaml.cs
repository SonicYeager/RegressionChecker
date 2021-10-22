using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Selection;
using Avalonia.Markup.Xaml;
using RegressionCheckerLogic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RegressionChecker
{
    public class MultiSelectFileOverviewUI : UserControl, IMultiSelectFileOverviewUI, INotifyPropertyChanged
    {
        public event OnMultiFilePathSelection? onMultiFilePathSelection;
        public event OnOpenAddFilePathDialog? onOpenFilePathSelection;

        new public event PropertyChangedEventHandler? PropertyChanged;

        ObservableCollection<PathViewModel> paths = new ObservableCollection<PathViewModel>();
        public ObservableCollection<PathViewModel> Paths
        {
            get => paths;
            set => this.RaiseAndSetIfChanged(ref paths, value);
        }
        public SelectionModel<PathViewModel> SelectedPaths { get; set; } = new() { SingleSelect = false };

        public MultiSelectFileOverviewUI()
        {
            AvaloniaXamlLoader.Load(this);

            PropertyChanged += PropertyChangedHandler;
            SelectedPaths.SelectionChanged += SelectedPathsChangedHandler;
        }

        private void SelectedPathsChangedHandler(object? sender, SelectionModelSelectionChangedEventArgs<PathViewModel> e)
        {
            List<string> selection = new();
            foreach (var item in e.SelectedItems)
                selection.Add(item.Path);
            foreach (var item in e.DeselectedItems)
                selection.Remove(item.Path);
            onMultiFilePathSelection.Invoke(selection);
        }

        private void PropertyChangedHandler(object? sender, PropertyChangedEventArgs e)
        {
            //TODO
        }

        public async Task SelectLatFileCommand()
        {
            onOpenFilePathSelection.Invoke();
        }

        public void AddFilePath(string path)
        {
            Paths.Add(new PathViewModel() { Path = path });
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

        protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
