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
                onOutput.Invoke("Wrong Input!");
                return new ParseCommandData();
            }
            var parsed = new ParseCommandData() { DestinationPath = args[1], ReferenceFilePaths=new List<ToDataFilePaths>()};

            int refStart = args.FindIndex((s) => s == "-r" || s == "--reference");
            int latStart = args.FindIndex((s) => s == "-l" || s == "--latest");

            if (refStart < latStart)
            {
                List<string> refFiles = new();
                List<string> latFiles = new();
                for (int i = refStart+1; i < latStart; ++i)
                    refFiles.Add(args[i]);
                for (int i = latStart + 1; i < args.Count; ++i)
                    latFiles.Add(args[i]);

                if(latFiles.Count >= 2)
                {
                    string ft = latFiles.Find(s => s.Contains("_FT"));
                    string rt = latFiles.Find(s => s.Contains("_RT"));
                    parsed.LatestFilePaths = new ToDataFilePaths() { FrameTimes=ft, MethodRunTimesPerFrame=rt };
                }
                else
                    parsed.LatestFilePaths = new ToDataFilePaths() { FrameTimes=latFiles.Find(s => s.Contains("_FT")), MethodRunTimesPerFrame="" };

                var ftlist = refFiles.FindAll(s => s.Contains("_FT"));
                var rtlist = refFiles.FindAll(s => s.Contains("_RT"));

                if (rtlist.Count > 0 && ftlist.Count == rtlist.Count)
                    for (int i = 0; i < ftlist.Count; ++i)
                        parsed.ReferenceFilePaths.Add(new ToDataFilePaths() { FrameTimes=ftlist[i], MethodRunTimesPerFrame=rtlist[i] });
                else
                    foreach (var ft in ftlist)
                        parsed.ReferenceFilePaths.Add(new ToDataFilePaths() { FrameTimes = ft, MethodRunTimesPerFrame = "" });
            }
            else
            {
                List<string> refFiles = new();
                List<string> latFiles = new();
                for (int i = latStart + 1; i < refStart; ++i)
                    latFiles.Add(args[i]);
                for (int i = refStart + 1; i < args.Count; ++i)
                    refFiles.Add(args[i]);

                if (latFiles.Count >= 2)
                {
                    string ft = latFiles.Find(s => s.Contains("_FT"));
                    string rt = latFiles.Find(s => s.Contains("_RT"));
                    parsed.LatestFilePaths = new ToDataFilePaths() { FrameTimes = ft, MethodRunTimesPerFrame = rt };
                }
                else
                    parsed.LatestFilePaths = new ToDataFilePaths() { FrameTimes = latFiles.Find(s => s.Contains("_FT")), MethodRunTimesPerFrame = "" };

                var ftlist = refFiles.FindAll(s => s.Contains("_FT"));
                var rtlist = refFiles.FindAll(s => s.Contains("_RT"));

                if (rtlist.Count > 0 && ftlist.Count == rtlist.Count)
                    for (int i = 0; i < ftlist.Count; ++i)
                        parsed.ReferenceFilePaths.Add(new ToDataFilePaths() { FrameTimes = ftlist[i], MethodRunTimesPerFrame = rtlist[i] });
                else
                    foreach (var ft in ftlist)
                        parsed.ReferenceFilePaths.Add(new ToDataFilePaths() { FrameTimes = ft, MethodRunTimesPerFrame = "" });
            }

            return parsed;
        }
    }
}
