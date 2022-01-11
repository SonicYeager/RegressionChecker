using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Themes;
using RegressionCheckerLogic;
using System.Collections.Generic;
using System.ComponentModel;

namespace RegressionChecker
{
    public partial class MainUI : Window, IMainUI, INotifyPropertyChanged
    {

        //UserControls
        public ISingleSelectFileOverviewUI? SingleSelectFileOverviewUI { get; set; }
        public IMultiSelectFileOverviewUI? MultiSelectFileOverviewUI { get; set; }
        public ISingleSelectionOverviewAutomaticAddUI? SingleSelectionOverviewAutomaticAddUI { get; set; }
        public IChartWrapperUI? ChartWrapperUI { get; set; }

        private int _closeRequested = -1;

        public MainUI()
        {
            AvaloniaXamlLoader.Load(this);

            Title = "RegressionChecker";
            DataContext = this;
            SetDark();

            SingleSelectFileOverviewUI = (ISingleSelectFileOverviewUI?)WindowHelperFunctions.FindUserControl<UserControl>( this.LogicalChildren, "SingleSelectFileOverview");
            MultiSelectFileOverviewUI = (IMultiSelectFileOverviewUI?)WindowHelperFunctions.FindUserControl<UserControl>(this.LogicalChildren, "MultiSelectFileOverview");
            SingleSelectionOverviewAutomaticAddUI = (ISingleSelectionOverviewAutomaticAddUI?)WindowHelperFunctions.FindUserControl<UserControl>(this.LogicalChildren, "SingleSelectionOverviewAutomaticAdd");
            ChartWrapperUI = (IChartWrapperUI?)WindowHelperFunctions.FindUserControl<UserControl>(this.LogicalChildren, "ChartWrapper");

            SingleSelectFileOverviewUI.onOpenFilePathSelection += async () => 
            {
                Window addFilePathDialog = new AddFilePathDialog();
                var res = await (addFilePathDialog.ShowDialog<string>(this));
                SingleSelectFileOverviewUI?.AddFilePath(res);
            };
            MultiSelectFileOverviewUI.onOpenFilePathSelection += async () => 
            {
                Window addFilePathDialog = new AddFilePathDialog();
                var res = await (addFilePathDialog.ShowDialog<string>(this));
                MultiSelectFileOverviewUI?.AddFilePath(res); 
            };
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

        public void SetDark()
        {
            Background = new SolidColorBrush(new Color(120, 40, 40, 40));
            Foreground = new SolidColorBrush(new Color(255, 250, 250, 250));

            Application.Current?.Styles.Insert(0, App.DefaultDark);

            LiveCharts.Configure(
                settings => settings
                    .AddDefaultMappers()
                    .AddSkiaSharp()
                    .AddDarkTheme(
                        theme =>
                        {
                            // you can add additional rules to the current theme
                            theme.Style
                                .HasRuleForLineSeries(lineSeries =>
                                {
                                    // this method will be called in the constructor of a line series instance

                                    lineSeries.LineSmoothness = 0.65;
                                    // ...
                                    // add more custom styles here ...
                                }).HasRuleForBarSeries(barSeries =>
                                {
                                    // this method will be called in the constructor of a column series instance
                                    // ...
                                });
                        }));
        }
        public void SetLight()
        {
            Background = new SolidColorBrush(new Color(120, 238, 238, 238));
            //HighlightedBackgound = new SolidColorBrush(new Color(255, 255, 255, 255));
            Foreground = new SolidColorBrush(new Color(255, 70, 70, 70));

            Application.Current?.Styles.Insert(0, App.DefaultLight);

            LiveCharts.Configure(
                settings => settings
                    .AddDefaultMappers()
                    .AddSkiaSharp()
                    .AddLightTheme());
        }
        public override void Show()
        {
            while (_closeRequested == -1)
                Thread.Sleep(25);

            if (_closeRequested == 1)
                base.Show();
            else
                base.Close();
        }
        public void CloseWindow()
        {
            _closeRequested = 0;
        }

        public void ShowWindow()
        {
            _closeRequested = 1;
        }
    }
}
