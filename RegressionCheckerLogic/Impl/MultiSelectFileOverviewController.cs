using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegressionCheckerLogic
{
    public class MultiSelectFileOverviewController : IMultiSelectFileOverviewController
    {
        public IMultiSelectFileOverviewUI MultiSelectFileOverviewUI { get; set; }
        public IDataConverter DataConverter { get; set; }
        public ICSVFileReader CSVFileReader { get; set; }
        public IExternalProgrammLauncher ExternalProgrammLauncher { get; set; }

        public event OnReadLineChartSeriesData onReadLineChartSeriesData;
        public event OnReadRegressiveMethods onReadRegressiveMethods;
        public event OnRequestReferenceSelection onRequestReferenceSelection;
        public event OnRequestDestiantion onRequestDestiantion;
        public event OnRemoveChartSeries onRemoveChartSeries;

        private delegate void Action(int i);

        private string LatSelection { get; set; }
        private string Destination { get; set; } = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\";

        public MultiSelectFileOverviewController(IDataConverter dataConverter, ICSVFileReader csvFileReader, IExternalProgrammLauncher externalProgrammLauncher, IMultiSelectFileOverviewUI multiSelectFileOverviewUI)
        {
            DataConverter = dataConverter;
            CSVFileReader = csvFileReader;
            ExternalProgrammLauncher = externalProgrammLauncher;
            MultiSelectFileOverviewUI = multiSelectFileOverviewUI;

            MultiSelectFileOverviewUI.onMultiFilePathSelection += (List<string> paths) => { FetchLineChartSeries(paths); };
        }

        private double ConvertToDouble(string str)
        {
            return Convert.ToDouble(str, new NumberFormatInfo() { NumberDecimalSeparator = "." });
        }

        private void IterateTill(int index, Action action)
        {
            for (int i = 0; i < index; ++i)
                action.Invoke(i);
        }

        private static int GetSmallesIndex(List<CSVFile> frameTimes)
        {
            int smallestIndex = int.MaxValue;
            foreach (var frameTime in frameTimes)
                if (frameTime.Elements.Count < smallestIndex)
                    smallestIndex = frameTime.Elements.Count;
            return smallestIndex;
        }

        private List<double> GetAveragedFrameTimes(List<CSVFile> frameTimes, int smallestIndex)
        {
            List<double> averaged = new List<double>();
            for (int i = 0; i < smallestIndex; ++i)
            {
                double sum = 0;
                foreach (var frameTime in frameTimes)
                    sum += ConvertToDouble(frameTime.Elements[i][1]);
                averaged.Add((sum / (double)frameTimes.Count));
            }
            return averaged;
        }

        private static List<string> CreateAveragedEntry(List<CSVFile> frameTimes, List<double> averaged, int i)
        {
            return new List<string>() { frameTimes[0].Elements[i][0], string.Format("{0:N4}", Convert.ToString(averaged[i], new NumberFormatInfo() { NumberDecimalSeparator = "." })) };
        }

        public CSVFile GetAverageFrameTimes(List<CSVFile> frameTimes) //should be public part of the interface and externally called
        {
            CSVFile averagedFrameTimes = new() { FilePath = "", Seperator = '\0', Elements = new List<List<string>>(), Headers = frameTimes[0].Headers };
            int smallestIndex = GetSmallesIndex(frameTimes);
            List<double> averaged = GetAveragedFrameTimes(frameTimes, smallestIndex);
            IterateTill(smallestIndex, (int i) => averagedFrameTimes.Elements.Add(CreateAveragedEntry(frameTimes, averaged, i)));

            return averagedFrameTimes;
        }

        private void FetchLineChartSeries(List<string> paths)
        {
            onRequestDestiantion?.Invoke();

            if (paths.Count > 0)
            {
                List<CSVFile> ftcsvs = new List<CSVFile>();
                foreach (var path in paths)
                {
                    // tracelosparser.exe <destPath> <srcPath> [srcPath]
                    List<string> argsForTheParser = new List<string>()
                {
                    Destination,
                    path
                };
                    ExternalProgrammLauncher.LaunchPorgrammWithArgs("tracelogparser.exe", argsForTheParser);
                    ftcsvs.Add(CSVFileReader.ReadCSVFile(Path.GetFileNameWithoutExtension(path) + "_FT.csv"));
                }
                var averagedRef = GetAverageFrameTimes(ftcsvs);
                var linchartseriesdata = DataConverter.ConvertCSVFileToLineChartSeriesData(averagedRef, "Reference");
                onReadLineChartSeriesData?.Invoke(linchartseriesdata);
            }
            else
                onRemoveChartSeries?.Invoke("Reference");

            onRequestReferenceSelection?.Invoke();
            if (paths.Count > 0 && (LatSelection != null && LatSelection != ""))
            {
                // „regressioneval.exe  <zielort>  <flag>  <ftmarkedcsv> {rtmarkedcsv} <flag> <ftmarkedcsv> {rtmarkedcsv}“.
                List<string> argsForTheEvalutaion = new List<string>()
                {
                    Destination,
                    "-l",
                    Path.GetFileNameWithoutExtension(LatSelection) + "_FT.csv",
                    Path.GetFileNameWithoutExtension(LatSelection) + "_RT.csv",
                    "-r",
                };
                foreach (var sel in paths)
                {
                    argsForTheEvalutaion.Add(Path.GetFileNameWithoutExtension(sel) + "_FT.csv");
                }
                ExternalProgrammLauncher.LaunchPorgrammWithArgs("regressioneval.exe", argsForTheEvalutaion);
                var rl = CSVFileReader.ReadCSVFile(Destination + "RL_0.csv");
                var regressiveMethodEntries = DataConverter.ConvertCSVFileToRegressiveMethodEntries(rl);
                onReadRegressiveMethods?.Invoke(regressiveMethodEntries, Path.GetFileNameWithoutExtension(LatSelection) + "_RT.csv");
            }
        }

        public List<string> GetSelection()
        {
            return MultiSelectFileOverviewUI.GetSelection();
        }

        public void SetLatestSelection(string refSelection)
        {
            LatSelection = refSelection;
        }

        public void SetDestination(string dest)
        {
            Destination = dest;
        }
    }
}
