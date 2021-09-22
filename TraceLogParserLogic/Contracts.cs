using System;
using System.Collections.Generic;

namespace tracelogparserlogic
{

    public delegate void OnOutput(string msg);

    public interface ICommandParser
    {
        public ParseCommandData ParseCLIArgs(List<string> args); //throw??

        public event OnOutput onOutput; //handle errors?
    }

    public interface ITraceLogParser
    {
        public CSVFile ParseTraceLog(TraceLogFile traceLogFile, string dstPath);
    }

    public interface ICSVFileWriter
    {
        public void WriteCSVFile(CSVFile file);
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

        public event OnOutput onOutput;
    }
}