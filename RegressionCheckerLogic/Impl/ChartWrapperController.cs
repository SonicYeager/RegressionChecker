using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegressionCheckerLogic
{
    public class ChartWrapperController : IChartWrapperController
    {
        public IChartWrapperUI ChartWrapperUI { get; set; }
        public IDataConverter DataConverter { get; set; }
        public ICSVFileReader CSVFileReader { get; set; }
        public IExternalProgrammLauncher ExternalProgrammLauncher { get; set; }

        public ChartWrapperController(IDataConverter dataConverter, ICSVFileReader csvFileReader, IExternalProgrammLauncher externalProgrammLauncher, IChartWrapperUI chartWrapperUI)
        {
            DataConverter = dataConverter;
            CSVFileReader = csvFileReader;
            ExternalProgrammLauncher = externalProgrammLauncher;
            ChartWrapperUI = chartWrapperUI;
        }

        public void SetLineChartSeries(LineChartSeriesData seriesData)
        {
            ChartWrapperUI.SetLineChartSeries(seriesData);
        }

        public void SetPieChartSeries(PieChartSeriesData seriesData)
        {
            ChartWrapperUI.SetPieChartSeries(seriesData);
        }

        public void RemoveLineChartSeries(string seriesname)
        {
            ChartWrapperUI.RemoveLineChartSeries(seriesname);
        }
    }
}
