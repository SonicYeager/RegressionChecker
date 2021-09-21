using System;
using System.Collections.Generic;

namespace regressionevallogic
{
    public delegate void OnOutput(string msg);
    public delegate void OnRegressionEvaluation(CSVFile file);

    public interface ICommandParser
    {
        public ParseCommandData ParseCLIArgs(string args); //throw??

        public event OnOutput onOutput; //handle errors?
    }

    public interface IRegressionEvaluator
    {
        public void ParseTraceLog(CSVFile file);

        public event OnRegressionEvaluation onRegressionEvaluation;
    }

    public interface ICSVFileWriter
    {
        public void WriteCSVFile(CSVFile file);
    }

    public interface ICSVFileReader
    {
        public CSVFile ReadCSVFile(string path);
    }

    public interface ICLIUI
    {
        public void Print(string msg);
    }

    public interface IMainController
    {
        public void Run(List<string> args);
    }

    public interface IRegressionEvaluationController
    {
        public void EvaluateForRegression(string dst, List<string> srcFiles);

        public event OnOutput onOutput;
    }
}