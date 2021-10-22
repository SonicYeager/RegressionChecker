using System;
using System.Collections.Generic;

namespace RegressionCheckerLogic
{
    public struct PathViewModel
    {
        public string Path { get; set; }
    }

    public struct LineChartSeriesData
    {
        public string Name { get; set; }
        public List<TimeSpan> Values { get; set; }
    }

    public struct PieChartDataEntry
    {
        public string EntryName { get; set; }
        public double EntryValue { get; set; }
    }

    public struct PieChartSeriesData
    {
        public string Name { get; set; }
        public List<PieChartDataEntry> Values { get; set; }
    }

    public struct RegressiveMethodEntry
    {
        public string MethodName { get; set; }
        public int FrameNumber { get; set; }
        public TimeSpan Runtime { get; set; }
    }

    public struct ParseCommandData
    {
        public bool NoGUI { get; set; }
        public string DestinationPath { get; set; }
        public List<string> SourceFilePaths { get; set; }
    }

    public struct CSVFile
    {
        public List<string> Headers { get; set; }
        public List<List<string>> Elements { get; set; }
        public string Seperator { get; set; }
        public string FilePath { get; set; }
    }
}