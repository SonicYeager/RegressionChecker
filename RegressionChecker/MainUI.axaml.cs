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
        public ISingleSelectFileOverviewUI SingleSelectFileOverviewUI { get; set; }
        public IMultiSelectFileOverviewUI MultiSelectFileOverviewUI { get; set; }
        public ISingleSelectionOverviewAutomaticAddUI SingleSelectionOverviewAutomaticAddUI { get; set; }
        public IChartWrapperUI ChartWrapperUI { get; set; }

        public MainUI()
        {
            AvaloniaXamlLoader.Load(this);

            //this.AttachDevTools();

            SingleSelectFileOverviewUI = (ISingleSelectFileOverviewUI)this.FindControl<UserControl>("SingleSelectFileOverview");
            MultiSelectFileOverviewUI = (IMultiSelectFileOverviewUI)this.FindControl<UserControl>("MultiSelectFileOverview");
            SingleSelectionOverviewAutomaticAddUI = (ISingleSelectionOverviewAutomaticAddUI)this.FindControl<UserControl>("SingleSelectionOverviewAutomaticAdd");
            ChartWrapperUI = (IChartWrapperUI)this.FindControl<UserControl>("ChartWrapper");

            Title = "RegressionChecker";
            DataContext = this;

            SetDark();
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
            //HighlightedBackgound = new SolidColorBrush(new Color(255, 40, 40, 40));
            Foreground = new SolidColorBrush(new Color(255, 250, 250, 250));

            Application.Current.Styles.Insert(0, App.DefaultDark);

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

            Application.Current.Styles.Insert(0, App.DefaultLight);

            LiveCharts.Configure(
                settings => settings
                    .AddDefaultMappers()
                    .AddSkiaSharp()
                    .AddLightTheme());
        }

        public override void Show()
        {
            base.Show();
        }
    }
}
