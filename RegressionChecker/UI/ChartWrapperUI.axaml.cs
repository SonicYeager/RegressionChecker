using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RegressionCheckerLogic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Avalonia;
using System.Collections.ObjectModel;
using System.Linq;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace RegressionChecker
{
    public partial class ChartWrapperUI : UserControl, IChartWrapperUI, INotifyPropertyChanged
    {
        new public event PropertyChangedEventHandler? PropertyChanged;

        public List<Axis> LineChartXAxis { get; set; } = new List<Axis> { new Axis { Name = "Frames", Labeler = (value) => "Frame " + value } };
        public List<Axis> LineChartYAxis { get; set; } = new List<Axis> { new Axis { Name = "Elapsed Time", Labeler = (value) => value + " ms" } };
        ObservableCollection<LineSeries<double>> lineSeries = new ObservableCollection<LineSeries<double>>();
        public ObservableCollection<LineSeries<double>> LineSeries
        {
            get => lineSeries;
            set => this.RaiseAndSetIfChanged(ref lineSeries, value);
        }
        ObservableCollection<LineSeries<double>> pieSeries = new ObservableCollection<LineSeries<double>>();
        public ObservableCollection<LineSeries<double>> PieSeries
        {
            get => pieSeries;
            set => this.RaiseAndSetIfChanged(ref pieSeries, value);
        }

        public CartesianChart LineChart { get; set; }
        public PieChart PieChart { get; set; }

        public ChartWrapperUI()
        {
            AvaloniaXamlLoader.Load(this);

            DataContext = this;

            LineChart = this.FindControl<CartesianChart>("LineChart");
            PieChart = this.FindControl<PieChart>("PieChart");

        }

        public void SetLineChartSeries(LineChartSeriesData seriesData)
        {
            var existingSeries = LineSeries.FirstOrDefault((LineSeries<double> series) => series.Name == seriesData.Name);
            var strokecolor = new SolidColorPaint(new SKColor(50, 50, 200)) { StrokeThickness = 1 };
            if (seriesData.Name != "Latest")
                strokecolor = new SolidColorPaint(new SKColor(200, 50, 50)) { StrokeThickness = 1 };
            var values = new List<double>();
            foreach (var item in seriesData.Values)
                values.Add(item);
            if(existingSeries != null && existingSeries.Name != "")
            {
                existingSeries = new LineSeries<double>()
                {
                    Name = seriesData.Name,
                    GeometryStroke = null,
                    GeometrySize = 5,
                    Values = values,
                    Stroke = strokecolor,
                };
            }
            else
            {
                LineSeries.Add(new LineSeries<double>()
                {
                    Name = seriesData.Name,
                    GeometryStroke = null,
                    GeometrySize = 5,
                    Values = values,
                    Stroke = strokecolor,
                });
            }
        }

        public void SetPieChartSeries(PieChartSeriesData seriesData)
        {
            var values = new List<LineSeries<double>>();
            foreach (var item in seriesData.Values)
                values.Add(new LineSeries<double>()
                { 
                    Name = item.EntryName,
                    Values = new double[1] { item.EntryValue },
                });
            PieSeries = new ObservableCollection<LineSeries<double>>(values);
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

        public void RemoveLineChartSeries(string seriesname)
        {
            LineSeries.Remove(LineSeries.FirstOrDefault((LineSeries<double> series) => series.Name == seriesname));
        }
    }
}
