using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tracelogparserlogic
{
    public class CommandParser : ICommandParser
    {
        public event OnOutput onOutput;

        public ParseCommandData ParseCLIArgs(List<string> args)
        {
            if (args.Count <= 1)
            {
                onOutput.Invoke("Wrong Input!Try:\ntracelosparser.exe <destPath> <srcPath> [srcPath]");
                return new ParseCommandData();
            }
            var parsed = new ParseCommandData() { DestinationPath = args[1], SourceFilePaths = new List<string>()};
            for (int i = 2; i < args.Count; ++i)
                parsed.SourceFilePaths.Add(args[i]);
            return parsed;
        }
    }
}
