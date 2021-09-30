using System.Collections.Generic;

namespace tracelogparserlogic
{
    public class MainController : IMainController
    {

        public ICLIUI CLIUI { get; init; }
        public ITraceLogParseController TraceLogParseController { get; init; }
        public ICommandParser CommandParser { get; init; }

        public MainController(ref ICLIUI ui, ref ITraceLogParseController controller, ref ICommandParser cmdp)
        {
            CLIUI = ui;
            TraceLogParseController = controller;
            CommandParser = cmdp;

            TraceLogParseController.onOutput += (string msg) => CLIUI.Print(msg);
            CommandParser.onOutput += (string msg) => CLIUI.Print(msg);
        }

        public void Run(List<string> args)
        {
            ParseCommandData cmdData = CommandParser.ParseCLIArgs(args);
            TraceLogParseController.ParseTraceLogToCSV(cmdData.DestinationPath, cmdData.SourceFilePaths);
        }
    }
}
