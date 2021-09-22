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
            return new ParseCommandData() { DestinationPath="", SourceFilePaths = new List<string>() { } };
        }
    }
}
