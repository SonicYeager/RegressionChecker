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

        private string Destination { get; set; } = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\";

        public MainController(ISingleSelectFileOverviewController singleSelectFileOverviewController, IMultiSelectFileOverviewController multiSelectFileOverviewController, ISingleSelectionOverviewAutomaticAddController singleSelectionOverviewAutomaticAddController, IChartWrapperController chartWrapperController, IMainUI mainUI, ICommandParser commandParser)
        {
            SingleSelectFileOverviewController = singleSelectFileOverviewController;
            MultiSelectFileOverviewController = multiSelectFileOverviewController;
            SingleSelectionOverviewAutomaticAddController = singleSelectionOverviewAutomaticAddController;
            ChartWrapperController = chartWrapperController;
            MainUI = mainUI;
            CommandParser = commandParser;

            SingleSelectFileOverviewController.onReadLineChartSeriesData += (LineChartSeriesData lineChartSeriesData) => { ChartWrapperController.SetLineChartSeries(lineChartSeriesData); };
            SingleSelectFileOverviewController.onReadRegressiveMethods += (List<RegressiveMethodEntry> entries) => { SingleSelectionOverviewAutomaticAddController.SetRegressiveMethods(entries); };
            SingleSelectFileOverviewController.onRequestDestiantion += () => { SingleSelectFileOverviewController.SetDestination(Destination); };
            SingleSelectFileOverviewController.onRequestReferenceSelection += () => { SingleSelectFileOverviewController.SetRefernceSelection(MultiSelectFileOverviewController.GetSelection()); };
            SingleSelectFileOverviewController.onRemoveChartSeries += (string seriesname) => { ChartWrapperController.RemoveLineChartSeries(seriesname); };

            MultiSelectFileOverviewController.onReadLineChartSeriesData += (LineChartSeriesData lineChartSeriesData) => { ChartWrapperController.SetLineChartSeries(lineChartSeriesData); };
            MultiSelectFileOverviewController.onReadRegressiveMethods += (List<RegressiveMethodEntry> entries) => { SingleSelectionOverviewAutomaticAddController.SetRegressiveMethods(entries); };
            MultiSelectFileOverviewController.onRequestDestiantion += () => { MultiSelectFileOverviewController.SetDestination(Destination); };
            MultiSelectFileOverviewController.onRequestReferenceSelection += () => { MultiSelectFileOverviewController.SetLatestSelection(SingleSelectFileOverviewController.GetSelection()); };
            MultiSelectFileOverviewController.onRemoveChartSeries += (string seriesname) => { ChartWrapperController.RemoveLineChartSeries(seriesname); };
        }

        public void Run(List<string> args)
        {
            var commandData = CommandParser.ParseCommandArgs(args);
            Destination = commandData.DestinationPath;
            //continue with ui
            //continue without ui
            //run down all src files with parsing and eval?
        }
    }
}
