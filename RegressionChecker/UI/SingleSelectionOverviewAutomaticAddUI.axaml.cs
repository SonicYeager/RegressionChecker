using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RegressionCheckerLogic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RegressionChecker
{
    public class SingleSelectionOverviewAutomaticAddUI : UserControl, ISingleSelectionOverviewAutomaticAddUI, INotifyPropertyChanged
    {
        public event OnSingleFilePathSelection? onSingleFilePathSelection;

        new public event PropertyChangedEventHandler? PropertyChanged;

        ObservableCollection<PathViewModel> paths = new ObservableCollection<PathViewModel>();
        public ObservableCollection<PathViewModel> Paths
        {
            get => paths;
            set => this.RaiseAndSetIfChanged(ref paths, value);
        }
        PathViewModel selectedPath;
        public PathViewModel SelectedPath
        {
            get => selectedPath;
            set => this.RaiseAndSetIfChanged(ref selectedPath, value);
        }

        public SingleSelectionOverviewAutomaticAddUI()
        {
            AvaloniaXamlLoader.Load(this);

            PropertyChanged += PropertyChangedHandler;
        }

        private void PropertyChangedHandler(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedPath")
                onSingleFilePathSelection?.Invoke(SelectedPath.Path);
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
