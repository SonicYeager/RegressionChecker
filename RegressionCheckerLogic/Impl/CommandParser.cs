using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegressionCheckerLogic
{
    public class CommandParser : ICommandParser
    {
        private delegate void OnFullFilePaths();
        private delegate void OnFrameTimeOnlyFilePaths();

        ///{ CONSTANTS
        /// ARGS
        private static readonly string ARG_REFERENCE_SHORT = "-r";
        private static readonly string ARG_REFERENCE_LONG = "--reference";
        private static readonly string ARG_LATEST_SHORT = "-l";
        private static readonly string ARG_LATEST_LONG = "--latest";
        /// FILEMARKS
        private static readonly string MARK_FRAMETIME = "_FT";
        private static readonly string MARK_RUNTIME = "_RT";
        ///}

        Tuple<List<string>, List<string>> SeperateFileTypes(bool refBegins, int refStart, int latStart, List<string> args)
        {
            List<string> refFiles = new();
            List<string> latFiles = new();

            if (refBegins)
            {
                for (int i = refStart + 1; i < latStart; ++i)
                    refFiles.Add(args[i]);
                for (int i = latStart + 1; i < args.Count; ++i)
                    latFiles.Add(args[i]);
            }
            else
            {
                for (int i = latStart + 1; i < refStart; ++i)
                    latFiles.Add(args[i]);
                for (int i = refStart + 1; i < args.Count; ++i)
                    refFiles.Add(args[i]);
            }

            return Tuple.Create(refFiles, latFiles);
        }

        bool HasNoGUIFlag(List<string> args)
        {
            return args.Contains("--nogui");
        }

        bool HasRefAndLatFlag(List<string> args)
        {
            return args.Contains("-r") && args.Contains("-l");
        }

        bool HasRefAndLatAndNoGUIFlag(List<string> args)
        {
            return args.Contains("-r") && args.Contains("-l") && args.Contains("--nogui");
        }

        public ParseCommandData ParseCommandArgs(List<string> args)
        {
            if (args.Count <= 1)
            {
                return new ParseCommandData();
            }
            var parsed = new ParseCommandData()
            {
                DestinationPath = args[1],
                LatestFilePaths = "",
                ReferenceFilePaths = new List<string>(), 
                SourceFilePaths = new List<string>(), 
                NoGUI = (HasNoGUIFlag(args)) ? true : false 
            };

            if (HasRefAndLatFlag(args))
            {
                int refStart = args.FindIndex((s) => s == ARG_REFERENCE_SHORT || s == ARG_REFERENCE_LONG);
                int latStart = args.FindIndex((s) => s == ARG_LATEST_SHORT || s == ARG_LATEST_LONG);
                List<string> refFiles;
                List<string> latFiles;
                (refFiles, latFiles) = SeperateFileTypes(refStart < latStart, refStart, latStart, args);
                parsed.LatestFilePaths = latFiles[0];
                parsed.ReferenceFilePaths = refFiles;
            }
            else
            {
                int strtndx = 2;
                if (HasNoGUIFlag(args))
                    ++strtndx;

                for (int i = strtndx; i < args.Count(); ++i)
                {
                    parsed.SourceFilePaths.Add(args[i]);
                }
            }
            return parsed;
        }
    }
}
