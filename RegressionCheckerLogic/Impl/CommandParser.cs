using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegressionCheckerLogic
{
    public class CommandParser : ICommandParser
    {
        public ParseCommandData ParseCommandArgs(List<string> args)
        {
            if (args.Count <= 1)
                return new ParseCommandData();
            string noguiarg = args.Find((string item) => item.StartsWith("-"));
            var parsed = new ParseCommandData() { DestinationPath = "", SourceFilePaths = new List<string>(), NoGUI = false };
            if (noguiarg != null)
            {
                parsed.NoGUI = true;
                parsed.DestinationPath = args[2];
                for (int i = 3; i < args.Count; ++i)
                    parsed.SourceFilePaths.Add(args[i]);
            }
            else
            {
                parsed.DestinationPath = args[1];
                for (int i = 2; i < args.Count; ++i)
                    parsed.SourceFilePaths.Add(args[i]);
            }
            return parsed;
        }
    }
}
