using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegressionCheckerLogic
{
    public class SingleSelectFileOverviewController : ISingleSelectFileOverviewController
    {
        public ISingleSelectFileOverviewUI SingleSelectFileOverviewUI { get; set; }
        public IDataConverter DataConverter { get; set; }
        public ICSVFileReader CSVFileReader { get; set; }
        public IExternalProgrammLauncher ExternalProgrammLauncher { get; set; }

        public event OnReadLineChartSeriesData onReadLineChartSeriesData;
        public event OnRequestReferenceSelection onRequestReferenceSelection;
        public event OnRequestDestiantion onRequestDestiantion;
        public event OnReadRegressiveMethods onReadRegressiveMethods;
        public event OnRemoveChartSeries onRemoveChartSeries;

        private List<string> RefSelection { get; set; }
        private string Destination { get; set; } = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\";

        public SingleSelectFileOverviewController(IDataConverter dataConverter, ICSVFileReader csvFileReader, IExternalProgrammLauncher externalProgrammLauncher,ISingleSelectFileOverviewUI singleSelectFileOverviewController)
        {
            DataConverter = dataConverter;
            CSVFileReader = csvFileReader;
            ExternalProgrammLauncher = externalProgrammLauncher;
            SingleSelectFileOverviewUI = singleSelectFileOverviewController;

            SingleSelectFileOverviewUI.onSingleFilePathSelection += (string path) => { FetchLineChartSeries(path); };
        }

        private void FetchLineChartSeries(string path)
        {
            onRequestDestiantion?.Invoke();

            if (path != "" && path != null)
            { // tracelosparser.exe <destPath> <srcPath> [srcPath]
                List<string> argsForTheParser = new List<string>()
                {
                    Destination,
                    path
                };
                ExternalProgrammLauncher.LaunchPorgrammWithArgs("tracelogparser.exe", argsForTheParser);
                var ftcsv = CSVFileReader.ReadCSVFile(Path.GetFileNameWithoutExtension(path) + "_FT.csv");
                var linchartseriesdata = DataConverter.ConvertCSVFileToLineChartSeriesData(ftcsv, "Latest");
                onReadLineChartSeriesData?.Invoke(linchartseriesdata);
            }
            else
                onRemoveChartSeries?.Invoke("Latest");


            onRequestReferenceSelection?.Invoke();
            if(RefSelection.Count != 0)
            {
                // „regressioneval.exe  <zielort>  <flag>  <ftmarkedcsv> {rtmarkedcsv} <flag> <ftmarkedcsv> {rtmarkedcsv}“.
                List<string> argsForTheEvalutaion = new List<string>()
                {
                    Destination,
                    "-l",
                    Path.GetFileNameWithoutExtension(path) + "_FT.csv",
                    Path.GetFileNameWithoutExtension(path) + "_RT.csv",
                    "-r",
                };
                foreach (var sel in RefSelection)
                {
                    argsForTheEvalutaion.Add(Path.GetFileNameWithoutExtension(sel) + "_FT.csv");
                }
                ExternalProgrammLauncher.LaunchPorgrammWithArgs("regressioneval.exe", argsForTheEvalutaion);
                var rl = CSVFileReader.ReadCSVFile(Destination + "RL_0.csv");
                var regressiveMethodEntries = DataConverter.ConvertCSVFileToRegressiveMethodEntries(rl);
                onReadRegressiveMethods?.Invoke(regressiveMethodEntries);
            }
        }

        public void SetRefernceSelection(List<string> refSelection)
        {
            RefSelection = refSelection;
        }

        public void SetDestination(string dest)
        {
            Destination = dest;
        }

        public string GetSelection()
        {
           return SingleSelectFileOverviewUI.GetSelection();
        }
    }
}
