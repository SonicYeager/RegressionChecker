using System;
using System.Collections.Generic;
using regressionevallogic;

namespace regressioneval
{
    class Program
    {
        static void Main()
        {
            List<string> listArgs = new List<string>(Environment.GetCommandLineArgs());

            //init all
            ICSVFileReader csvFileReader = new CSVFileReader();
            IRegressionEvaluator regressionEvaluator = new RegressionEvaluator();
            ICSVFileWriter csvFileWriter = new CSVFileWriter();
            IRegressionEvaluationController traceLogParseController = new RegressionEvaluatorController(ref csvFileReader, ref csvFileWriter, ref regressionEvaluator);
            ICommandParser commandParser = new CommandParser();
            ICLIUI ccliUI = new CLIUI();
            IMainController mainController = new MainController(ref ccliUI, ref traceLogParseController, ref commandParser);

            //run
            mainController.Run(listArgs);
        }
    }
}
