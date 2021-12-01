using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RegressionCheckerLogic
{
    public delegate void OnAdditionalFilePath(string path);
    public delegate void OnMultiFilePathSelection(List<string> paths);
    public delegate void OnSingleFilePathSelection(string path);
    public delegate void OnReadLineChartSeriesData(LineChartSeriesData data);
    public delegate void OnReadPieChartSeriesData(PieChartSeriesData data);
    public delegate void OnOpenAddFilePathDialog();

    public interface IMainUI : INotifyPropertyChanged
    {
        public void AddLineChartSeries(LineChartSeriesData seriesData);
        public void AddPieChartSeries(PieChartSeriesData seriesData);
        public void SetRegressiveMethods(List<RegressiveMethodEntry> regressiveMethods);
    }

    public interface IMainController
    {
        public void Run(List<string> args);
    }

    public interface IAddFilePathDialog
    {
    }

    public interface ISingleSelectFileOverviewUI
    {
        public void AddFilePath(string path);

        public event OnSingleFilePathSelection onSingleFilePathSelection;
        public event OnOpenAddFilePathDialog onOpenFilePathSelection;
    }

    public interface ISingleSelectFileOverviewController
    {
        public LineChartSeriesData GetLineChartSeriesDataFromFile(string path);
        public PieChartSeriesData GetPieChartSeriesDataFromFile(string path);

        public event OnReadLineChartSeriesData onReadLineChartSeriesData;
        public event OnReadPieChartSeriesData onReadPieChartSeriesData;
    }

    public interface ISingleSelectionOverviewAutomaticAddUI
    {
        public void AddFilePath(string path);

        public event OnSingleFilePathSelection onSingleFilePathSelection;
    }

    public interface ISingleSelectionOverviewAutomaticAddController
    {
        public LineChartSeriesData GetLineChartSeriesDataFromFile(string path);
        public PieChartSeriesData GetPieChartSeriesDataFromFile(string path);

        public event OnReadLineChartSeriesData onReadLineChartSeriesData;
        public event OnReadPieChartSeriesData onReadPieChartSeriesData;
    }

    public interface IMultiSelectFileOverviewUI
    {
        public void AddFilePath(string path);

        public event OnMultiFilePathSelection onMultiFilePathSelection;
        public event OnOpenAddFilePathDialog onOpenFilePathSelection;
    }

    public interface IMultiSelectFileOverviewController
    {
        public LineChartSeriesData GetLineChartSeriesDataFromFile(string path);
        public PieChartSeriesData GetPieChartSeriesDataFromFile(string path);

        public event OnReadLineChartSeriesData onReadLineChartSeriesData;
        public event OnReadPieChartSeriesData onReadPieChartSeriesData;
    }

    public interface IChartWrapperUI
    {
        public void SetLineChartSeries(LineChartSeriesData seriesData);
        public void SetPieChartSeries(PieChartSeriesData seriesData);
    }

    public interface IChartWrapperController
    {
        //TODO
    }

    public interface ICommandParser
    {
        public ParseCommandData ParseCommandArgs(List<string> args);
        //public List<string> ParseCommandData(RegressionEvaluationCommandData data);
        //public List<string> ParseCommandData(TraceLogParseCommandData data);
    }

    public interface IDataConverter
    {
        public LineChartSeriesData ConvertCSVFileToLineChartSeriesData(CSVFile file, string name);
        public PieChartSeriesData ConvertCSVFileToPieChartSeriesData(CSVFile file, string framenumber);
    }

    public interface ICSVFileReader
    {
        public CSVFile ReadCSVFile(string path);
    }

    public interface IExternalProgrammLauncher
    {
        public void LaunchPorgrammWithArgs(string programmName, List<string> args); //maybe return succ or not
    }
}
