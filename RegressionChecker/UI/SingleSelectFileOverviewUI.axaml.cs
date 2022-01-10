using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Selection;
using Avalonia.Markup.Xaml;
using RegressionCheckerLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RegressionChecker
{
    public class SingleSelectFileOverviewUI : UserControl, ISingleSelectFileOverviewUI, INotifyPropertyChanged
    {
        public event OnSingleFilePathSelection? onSingleFilePathSelection;
        public event OnOpenAddFilePathDialog? onOpenFilePathSelection;

        new public event PropertyChangedEventHandler? PropertyChanged;

        ObservableCollection<PathModel> paths = new ObservableCollection<PathModel>();
        public ObservableCollection<PathModel> Paths
        {
            get => paths;
            set => this.RaiseAndSetIfChanged(ref paths, value);
        }
        PathModel selectedPath;
        public PathModel SelectedPath
        {
            get => selectedPath;
            set => this.RaiseAndSetIfChanged(ref selectedPath, value);
        }

        public SingleSelectFileOverviewUI()
        {
            AvaloniaXamlLoader.Load(this);

            PropertyChanged += PropertyChangedHandler;

            DataContext = this;
        }

        private void PropertyChangedHandler(object? sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "SelectedPath")
                onSingleFilePathSelection?.Invoke(SelectedPath.Path);
        }

        public async Task SelectLatFileCommand()
        {
            onOpenFilePathSelection?.Invoke();
        }

        protected bool RaiseAndSetIfChanged<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
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

        public void AddFilePath(string path)
        {
            if (path != null)
                Paths.Add(new PathModel(){Path=path});
        }

        public string GetSelection()
        {
            return SelectedPath.Path;
        }
    }
}
