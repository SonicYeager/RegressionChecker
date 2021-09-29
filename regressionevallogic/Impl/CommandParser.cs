using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace regressionevallogic
{
    public class CommandParser : ICommandParser
    {
        public event OnOutput onOutput;

        public ParseCommandData ParseCLIArgs(List<string> args)
        {
            if (args.Count <= 1)
            {
                onOutput.Invoke("Wrong Input!Try:\regressioneval.exe <destPath> -r <refPath> [refPath] -l <latestPath>");
                return new ParseCommandData();
            }
            var parsed = new ParseCommandData() { DestinationPath = args[1], ReferenceFilePaths = new List<string>(), LatestFilePaths = new List<string>() };

            int refStart = args.FindIndex((s) => s == "-r");
            int latStart = args.FindIndex((s) => s == "-l");

            if (refStart < latStart)
            {
                for (int i = refStart+1; i < latStart; ++i)
                    parsed.ReferenceFilePaths.Add(args[i]);
                parsed.LatestFilePaths.Add(args[latStart+1]);
            }
            else
            {
                parsed.LatestFilePaths.Add(args[latStart + 1]);
                for (int i = refStart+1; i < args.Count; ++i)
                    parsed.ReferenceFilePaths.Add(args[i]);
            }

            return parsed;
        }
    }
}
