using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RegressionCheckerLogic
{
    public delegate void OnAdditionalFilePath(string path);
    public delegate void OnMultiFilePathSelection(List<string> paths);
    public delegate void OnSingleFilePathSelection(string path);
    public delegate void OnRegressiveEntrySelection(RegressiveMethodEntry entry, string path);
    public delegate void OnReadLineChartSeriesData(LineChartSeriesData data);
    public delegate void OnReadPieChartSeriesData(PieChartSeriesData data);
    public delegate void OnReadRegressiveMethods(List<RegressiveMethodEntry> regressiveMethodEntries, string path);
    public delegate void OnOpenAddFilePathDialog();
    public delegate void OnRequestReferenceSelection();
    public delegate void OnRequestDestiantion();
    public delegate void OnRequestExit();
    public delegate void OnRemoveChartSeries(string name);

    public interface IMainUI : INotifyPropertyChanged
    {
        public void AddLineChartSeries(LineChartSeriesData seriesData);
        public void AddPieChartSeries(PieChartSeriesData seriesData);
        public void SetRegressiveMethods(List<RegressiveMethodEntry> regressiveMethods);
        public void CloseWindow();
        public void ShowWindow();
    }

    public interface IMainController
    {
        public void Run(List<string> args, OnRequestExit onRequestExit);
    }

    public interface IAddFilePathDialog
    {
        event PropertyChangedEventHandler PropertyChanged;
    }

    public interface ISingleSelectFileOverviewUI
    {
        public void AddFilePath(string path);
        public string GetSelection();

        public event OnSingleFilePathSelection onSingleFilePathSelection;
        public event OnOpenAddFilePathDialog onOpenFilePathSelection;
    }

    public interface ISingleSelectFileOverviewController
    {
        public event OnReadLineChartSeriesData onReadLineChartSeriesData;
        public event OnReadRegressiveMethods onReadRegressiveMethods;
        public event OnRequestReferenceSelection onRequestReferenceSelection;
        public event OnRequestDestiantion onRequestDestiantion;
        public event OnRemoveChartSeries onRemoveChartSeries;

        public void SetRefernceSelection(List<string> refSelection);
        public void SetDestination(string dest);
        public string GetSelection();
        public void AddFilePath(string path);
    }

    public interface ISingleSelectionOverviewAutomaticAddUI
    {
        public void SetRegressiveMethods(List<RegressiveMethodEntry> regressiveMethodEntries, string path);
        public void RemoveRegressiveMethods();

        public event OnRegressiveEntrySelection onRegressiveEntrySelection;
    }

    public interface ISingleSelectionOverviewAutomaticAddController
    {
        public event OnReadPieChartSeriesData onReadPieChartSeriesData;
        public void SetRegressiveMethods(List<RegressiveMethodEntry> regressiveMethodEntries, string path);
        public void RemoveRegressiveMethods();
    }

    public interface IMultiSelectFileOverviewUI
    {
        public void AddFilePath(string path);

        public List<string> GetSelection();

        public event OnMultiFilePathSelection onMultiFilePathSelection;
        public event OnOpenAddFilePathDialog onOpenFilePathSelection;
    }

    public interface IMultiSelectFileOverviewController
    {
        public event OnReadLineChartSeriesData onReadLineChartSeriesData;
        public event OnReadRegressiveMethods onReadRegressiveMethods;
        public event OnRequestReferenceSelection onRequestReferenceSelection;
        public event OnRequestDestiantion onRequestDestiantion;
        public event OnRemoveChartSeries onRemoveChartSeries;

        public void SetLatestSelection(string refSelection);
        public void SetDestination(string dest);
        public List<string> GetSelection();
        public void AddFilePath(string path);
    }

    public interface IChartWrapperUI
    {
        public void SetLineChartSeries(LineChartSeriesData seriesData);
        public void SetPieChartSeries(PieChartSeriesData seriesData);
        public void RemoveLineChartSeries(string seriesname);
    }

    public interface IChartWrapperController
    {
        public void SetLineChartSeries(LineChartSeriesData seriesData);
        public void SetPieChartSeries(PieChartSeriesData seriesData);
        public void RemoveLineChartSeries(string seriesname);

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
        public List<RegressiveMethodEntry> ConvertCSVFileToRegressiveMethodEntries(CSVFile file);
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
