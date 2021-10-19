using System;
using System.Collections.Generic;

namespace regressionevallogic
{
    public delegate void OnOutput(string msg);

    public interface ICommandParser
    {
        public ParseCommandData ParseCLIArgs(List<string> args); //throw??

        public event OnOutput onOutput; //handle errors?
    }

    public interface IRegressionEvaluator
    {
        public CSVFile EvaluateRegression(ReferenceData refData, LatestData latestData, string path);
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
        public void EvaluateForRegression(string dst, ReferenceData refData, LatestData latestData);

        public event OnOutput onOutput;
    }
}