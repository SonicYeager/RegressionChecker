using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RegressionCheckerLogic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.SKCharts;
using LiveChartsCore;

namespace RegressionChecker
{
    public partial class ChartWrapperUI : UserControl, IChartWrapperUI, INotifyPropertyChanged
    {
        new public event PropertyChangedEventHandler? PropertyChanged;

        public List<Axis> LineChartXAxis { get; set; } = new List<Axis> { new Axis { Name = "Frames", Labeler = (value) => "Frame " + value } };
        public List<Axis> LineChartYAxis { get; set; } = new List<Axis> { new Axis { Name = "Elapsed Time", Labeler = (value) => value + " ms" } };

        public SKCartesianChart LineChart { get; set; }
        public SKPieChart PieChart { get; set; }

        public ChartWrapperUI()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void SetLineChartSeries(LineChartSeriesData seriesData)
        {
            throw new System.NotImplementedException();
        }

        public void SetPieChartSeries(PieChartSeriesData seriesData)
        {
            throw new System.NotImplementedException();
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
    }
}
