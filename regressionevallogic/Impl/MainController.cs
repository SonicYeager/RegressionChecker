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
            //TODO
        }

        public void Run(List<string> args)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
