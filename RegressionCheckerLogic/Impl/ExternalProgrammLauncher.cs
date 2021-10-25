using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RegressionCheckerLogic
{
    public class ExternalProgrammLauncher : IExternalProgrammLauncher
    {
        public void LaunchPorgrammWithArgs(string programmName, List<string> args)
        {
            string arguments = "";
            foreach (var arg in args)
                arguments += arg + " ";
            ProcessStartInfo processStartInfo = new(programmName, arguments);
            using Process process = new Process() { StartInfo = processStartInfo };
            process.Start();
            process.WaitForExit();
            //var ext = process.ExitCode;
        }
    }
}
