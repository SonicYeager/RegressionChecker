using System;
using System.Collections.Generic;

namespace tracelogparserlogic
{
    public interface ICommandParser
    {
        public ParseCommandData ParseCLIArgs(string args); //throw??
    }

    public interface ITraceLogParser
    {
        //public CSVData ParseTraceLog(TraceLogData);
    }

    public interface ICSVFileWriter
    {
        public void ReadTextFile(CSVFile file);
    }

    public interface ITextFileReader
    {
        public TraceLogFile ReadTextFile(string path);
    }

    public interface ICLIUI
    {
        public void Print(string msg);
    }

    public interface IMainController
    {
        public void Run(List<string> args);
    }

    public interface ITraceLogParseController
    {
        public void ParseTraceLogToCSV(string dst, List<string> srcFiles);
    }
}