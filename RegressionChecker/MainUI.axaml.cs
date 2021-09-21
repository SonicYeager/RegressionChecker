using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RegressionCheckerLogic;
using System.Collections.Generic;

namespace RegressionChecker
{
    public partial class MainUI : Window, IMainUI
    {
        public MainUI()
        {
            AvaloniaXamlLoader.Load(this);

            this.AttachDevTools();
        }

        public void AddLineChartSeries(LineChartSeriesData seriesData)
        {
            throw new System.NotImplementedException();
        }

        public void AddPieChartSeries(PieChartSeriesData seriesData)
        {
            throw new System.NotImplementedException();
        }

        public void SetRegressiveMethods(List<RegressiveMethodEntry> regressiveMethods)
        {
            throw new System.NotImplementedException();
        }
    }
}
