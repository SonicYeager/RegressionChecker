using System;
using System.Collections.Generic;
using tracelogparserlogic;

namespace tracelogparser
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> listArgs = new List<string>(args);

            //init all
            ITextFileReader txtFileReader = new TextFileReader();
            ITraceLogParser traceLogParser = new TraceLogParser();
            ICSVFileWriter csvFileWriter = new CSVFileWriter();
            ITraceLogParseController traceLogParseController = new TraceLogParseController(ref txtFileReader, ref csvFileWriter, ref traceLogParser);
            ICommandParser commandParser = new CommandParser();
            ICLIUI ccliUI = new CLIUI();
            IMainController mainController = new MainController(ref ccliUI, ref traceLogParseController, ref commandParser);

            //run
            mainController.Run(listArgs);
        }
    }
}
