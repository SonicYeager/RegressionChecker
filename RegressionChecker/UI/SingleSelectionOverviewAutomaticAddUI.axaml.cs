using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RegressionCheckerLogic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace RegressionChecker
{
    public class SingleSelectionOverviewAutomaticAddUI : UserControl, ISingleSelectionOverviewAutomaticAddUI, INotifyPropertyChanged
    {
        public event OnRegressiveEntrySelection? onRegressiveEntrySelection;

        new public event PropertyChangedEventHandler? PropertyChanged;

        ObservableCollection<RegressiveMethodEntryModel> paths = new ObservableCollection<RegressiveMethodEntryModel>();
        public ObservableCollection<RegressiveMethodEntryModel> Paths
        {
            get => paths;
            set => this.RaiseAndSetIfChanged(ref paths, value);
        }
        RegressiveMethodEntryModel selectedPath;
        public RegressiveMethodEntryModel SelectedPath
        {
            get => selectedPath;
            set => this.RaiseAndSetIfChanged(ref selectedPath, value);
        }

        private List<RegressiveMethodEntry> _regressiveMethodEntries = new List<RegressiveMethodEntry>();
        private string _path = "";

        public SingleSelectionOverviewAutomaticAddUI()
        {
            AvaloniaXamlLoader.Load(this);

            PropertyChanged += PropertyChangedHandler;

            DataContext = this;
        }

        private void PropertyChangedHandler(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedPath")
            {
                RegressiveMethodEntry regressiveMethodEntry = new RegressiveMethodEntry()
                {
                    MethodName = SelectedPath.MethodName,
                    FrameNumber = SelectedPath.FrameNumber,
                    Runtime = Convert.ToDouble(SelectedPath.Runtime, new NumberFormatInfo() { NumberDecimalSeparator = "." })
                };

                onRegressiveEntrySelection?.Invoke(regressiveMethodEntry, _path);
            }
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

        public void SetRegressiveMethods(List<RegressiveMethodEntry> regressiveMethodEntries, string path)
        {
            Paths.Clear();
            _path = path;
            foreach (var entry in regressiveMethodEntries)
                Paths.Add(new RegressiveMethodEntryModel() { MethodName = entry.MethodName, FrameNumber = entry.FrameNumber, Runtime = entry.Runtime.ToString() });
            _regressiveMethodEntries = regressiveMethodEntries;
        }

        public void RemoveRegressiveMethods()
        {
            Paths.Clear();
        }
    }
}
