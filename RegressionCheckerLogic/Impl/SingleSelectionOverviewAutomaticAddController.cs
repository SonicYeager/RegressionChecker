using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegressionCheckerLogic
{
    public class SingleSelectionOverviewAutomaticAddController : ISingleSelectionOverviewAutomaticAddController
    {
        public ISingleSelectionOverviewAutomaticAddUI SingleSelectionOverviewAutomaticAdd { get; set; }
        public IDataConverter DataConverter { get; set; }
        public ICSVFileReader CSVFileReader { get; set; }
        public IExternalProgrammLauncher ExternalProgrammLauncher { get; set; }

        public event OnReadPieChartSeriesData onReadPieChartSeriesData;

        public SingleSelectionOverviewAutomaticAddController(IDataConverter dataConverter, ICSVFileReader csvFileReader, IExternalProgrammLauncher externalProgrammLauncher,ISingleSelectionOverviewAutomaticAddUI singleSelectionOverviewAutomaticAddUI)
        {
            DataConverter = dataConverter;
            CSVFileReader = csvFileReader;
            ExternalProgrammLauncher = externalProgrammLauncher;
            SingleSelectionOverviewAutomaticAdd = singleSelectionOverviewAutomaticAddUI;

            SingleSelectionOverviewAutomaticAdd.onRegressiveEntrySelection += (RegressiveMethodEntry entry, string path) => { GetRTChartSeries(entry, path); };
        }

        private void GetRTChartSeries(RegressiveMethodEntry entry, string path)
        {
            var csvfile = CSVFileReader.ReadCSVFile(path);
            PieChartSeriesData pieChartSeriesData = DataConverter.ConvertCSVFileToPieChartSeriesData(csvfile, entry.FrameNumber);
            onReadPieChartSeriesData?.Invoke(pieChartSeriesData);
        }

        public void SetRegressiveMethods(List<RegressiveMethodEntry> regressiveMethodEntries, string path)
        {
            SingleSelectionOverviewAutomaticAdd.SetRegressiveMethods(regressiveMethodEntries, path);
        }

        public void RemoveRegressiveMethods()
        {
            SingleSelectionOverviewAutomaticAdd.RemoveRegressiveMethods();
        }
    }
}
