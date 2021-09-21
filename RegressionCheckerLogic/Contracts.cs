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

    public interface ISingleSelectFileOverviewUI
    {
        public event OnAdditionalFilePath onAdditionalFilePath;
        public event OnSingleFilePathSelection onSingleFilePathSelection;
    }

    public interface ISingleSelectFileOverviewController
    {
        public LineChartSeriesData GetLineChartSeriesDataFromFile(string path);
        public PieChartSeriesData GetPieChartSeriesDataFromFile(string path);

        public event OnReadLineChartSeriesData onReadLineChartSeriesData;
        public event OnReadPieChartSeriesData onReadPieChartSeriesData;
    }

    public interface IMultiSelectFileOverviewUI
    {
        public event OnAdditionalFilePath onAdditionalFilePath;
        public event OnMultiFilePathSelection onMultiFilePathSelection;
    }

    public interface IMultiSelectFileOverviewController
    {
        public LineChartSeriesData GetLineChartSeriesDataFromFile(string path);
        public PieChartSeriesData GetPieChartSeriesDataFromFile(string path);

        public event OnReadLineChartSeriesData onReadLineChartSeriesData;
        public event OnReadPieChartSeriesData onReadPieChartSeriesData;
    }

    public interface ICommandParser
    {
        public ParseCommandData ParseCommandArgs(List<string> args);
    }

    public interface IDataConverter
    {
        public LineChartSeriesData ConvertCSVFileToLineChartSeriesData(CSVFile file);
        public PieChartSeriesData ConvertCSVFileToPieChartSeriesData(CSVFile file);
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
