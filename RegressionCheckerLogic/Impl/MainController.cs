using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegressionCheckerLogic
{
    public class MainController : IMainController
    {

        public ISingleSelectFileOverviewController SingleSelectFileOverviewController { get; set; }
        public IMultiSelectFileOverviewController MultiSelectFileOverviewController { get; set; }
        public ISingleSelectionOverviewAutomaticAddController SingleSelectionOverviewAutomaticAddController { get; set; }
        public IChartWrapperController ChartWrapperController { get; set; }
        public IMainUI MainUI { get; set; }
        public ICommandParser CommandParser { get; set; }
        public IDataConverter DataConverter { get; set; }
        public ICSVFileReader CSVFileReader { get; set; }
        public IExternalProgrammLauncher ExternalProgrammLauncher { get; set; }

        private string Destination { get; set; } = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\";

        public MainController(ISingleSelectFileOverviewController singleSelectFileOverviewController, IMultiSelectFileOverviewController multiSelectFileOverviewController, ISingleSelectionOverviewAutomaticAddController singleSelectionOverviewAutomaticAddController, IChartWrapperController chartWrapperController, IMainUI mainUI, ICommandParser commandParser, IDataConverter dataConverter, ICSVFileReader csvFileReader, IExternalProgrammLauncher externalProgrammLauncher)
        {
            SingleSelectFileOverviewController = singleSelectFileOverviewController;
            MultiSelectFileOverviewController = multiSelectFileOverviewController;
            SingleSelectionOverviewAutomaticAddController = singleSelectionOverviewAutomaticAddController;
            ChartWrapperController = chartWrapperController;
            MainUI = mainUI;
            CommandParser = commandParser;
            DataConverter = dataConverter;
            CSVFileReader = csvFileReader;
            ExternalProgrammLauncher = externalProgrammLauncher;

            SingleSelectFileOverviewController.onReadLineChartSeriesData += (LineChartSeriesData lineChartSeriesData) => { ChartWrapperController.SetLineChartSeries(lineChartSeriesData); };
            SingleSelectFileOverviewController.onReadRegressiveMethods += (List<RegressiveMethodEntry> entries, string path) => { SingleSelectionOverviewAutomaticAddController.SetRegressiveMethods(entries, path); };
            SingleSelectFileOverviewController.onRequestDestiantion += () => { SingleSelectFileOverviewController.SetDestination(Destination); };
            SingleSelectFileOverviewController.onRequestReferenceSelection += () => { SingleSelectFileOverviewController.SetRefernceSelection(MultiSelectFileOverviewController.GetSelection()); };
            SingleSelectFileOverviewController.onRemoveChartSeries += (string seriesname) =>
            { 
                ChartWrapperController.RemoveLineChartSeries(seriesname);
                SingleSelectionOverviewAutomaticAddController.RemoveRegressiveMethods(); 
            };

            MultiSelectFileOverviewController.onReadLineChartSeriesData += (LineChartSeriesData lineChartSeriesData) => { ChartWrapperController.SetLineChartSeries(lineChartSeriesData); };
            MultiSelectFileOverviewController.onReadRegressiveMethods += (List<RegressiveMethodEntry> entries, string path) => { SingleSelectionOverviewAutomaticAddController.SetRegressiveMethods(entries, path); };
            MultiSelectFileOverviewController.onRequestDestiantion += () => { MultiSelectFileOverviewController.SetDestination(Destination); };
            MultiSelectFileOverviewController.onRequestReferenceSelection += () => { MultiSelectFileOverviewController.SetLatestSelection(SingleSelectFileOverviewController.GetSelection()); };
            MultiSelectFileOverviewController.onRemoveChartSeries += (string seriesname) => 
            { 
                ChartWrapperController.RemoveLineChartSeries(seriesname);
                SingleSelectionOverviewAutomaticAddController.RemoveRegressiveMethods();
            };

            SingleSelectionOverviewAutomaticAddController.onReadPieChartSeriesData += (PieChartSeriesData data) => { ChartWrapperController.SetPieChartSeries(data); };
        }

        public void Run(List<string> args, OnRequestExit onRequestExit)
        {
            if(!(args.Count <= 1))
            {
                var commandData = CommandParser.ParseCommandArgs(args);
                Destination = commandData.DestinationPath;
                if (!commandData.NoGUI)
                {
                    SingleSelectFileOverviewController.AddFilePath(commandData.LatestFilePaths);
                    foreach (var path in commandData.ReferenceFilePaths)
                        MultiSelectFileOverviewController.AddFilePath(path);
                    MainUI.ShowWindow();
                }
                else
                {
                    MainUI.CloseWindow();
                    if (!(commandData.SourceFilePaths.Count <= 0))
                    {
                        foreach (var path in commandData.SourceFilePaths)
                        {
                            List<string> argsForTheParser = new List<string>()
                            {
                                Destination,
                                path
                            };
                            ExternalProgrammLauncher.LaunchPorgrammWithArgs("tracelogparser.exe", argsForTheParser);
                        }
                    }
                    else
                    {
                        List<string> argsForTheParser = new List<string>()
                        {
                            Destination,
                            commandData.LatestFilePaths,
                        };
                        ExternalProgrammLauncher.LaunchPorgrammWithArgs("tracelogparser.exe", argsForTheParser);

                        if (commandData.ReferenceFilePaths.Count != 0)
                        {
                            // „regressioneval.exe  <zielort>  <flag>  <ftmarkedcsv> {rtmarkedcsv} <flag> <ftmarkedcsv> {rtmarkedcsv}“.
                            List<string> argsForTheEvalutaion = new List<string>()
                            {
                                Destination,
                                "-l",
                                Path.GetFileNameWithoutExtension(commandData.LatestFilePaths) + "_FT.csv",
                                Path.GetFileNameWithoutExtension(commandData.LatestFilePaths) + "_RT.csv",
                                "-r",
                            };
                            foreach (var sel in commandData.ReferenceFilePaths)
                            {
                                argsForTheEvalutaion.Add(Path.GetFileNameWithoutExtension(sel) + "_FT.csv");
                            }
                            ExternalProgrammLauncher.LaunchPorgrammWithArgs("regressioneval.exe", argsForTheEvalutaion);
                        }
                    }
                    onRequestExit.Invoke();
                }
            }
            else 
            {
                MainUI.ShowWindow();
            }
        }
    }
}
