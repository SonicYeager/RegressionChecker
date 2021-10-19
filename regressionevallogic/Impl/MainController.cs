using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace regressionevallogic
{
    public class MainController : IMainController
    {
        public IRegressionEvaluationController RegressionEvaluationController { get; set; }
        public ICLIUI CLIUI { get; set; }
        public ICommandParser CommandParser { get; set; }

        public MainController(ref IRegressionEvaluationController rec, ref ICLIUI cui, ref ICommandParser cp)
        {
            RegressionEvaluationController = rec;
            CLIUI = cui;
            CommandParser = cp;

            RegressionEvaluationController.onOutput += (string msg) => CLIUI.Print(msg);
            CommandParser.onOutput += (string msg) => CLIUI.Print(msg);
        }

        public void Run(List<string> args)
        {
            var parsedArgs = CommandParser.ParseCLIArgs(args);
            RegressionEvaluationController.EvaluateForRegression(parsedArgs.DestinationPath, parsedArgs.ReferenceFilePaths, parsedArgs.LatestFilePaths.FrameTimes, parsedArgs.LatestFilePaths.MethodRunTimesPerFrame);
        }
    }
}
