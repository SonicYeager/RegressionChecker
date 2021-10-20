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
        private static readonly string MARK_RUNTIME= "_RT";
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

        ToDataFilePaths CreateFullLatestFilePaths(List<string> latFiles)
        {
            string ft = latFiles.Find(s => s.Contains(MARK_FRAMETIME));
            string rt = latFiles.Find(s => s.Contains(MARK_RUNTIME));
            return new ToDataFilePaths() { FrameTimes = ft, MethodRunTimesPerFrame = rt };
        }

        ToDataFilePaths CreateFTOnlyLatestFilePaths(List<string> latFiles)
        {
            string ft = latFiles.Find(s => s.Contains(MARK_FRAMETIME));
            return new ToDataFilePaths() { FrameTimes = ft, MethodRunTimesPerFrame = "" };
        }

        void CreateFullOrFrameTimeOnlyFilePaths(bool ishalf, OnFullFilePaths onFullFilePaths, OnFrameTimeOnlyFilePaths onFrameTimeOnlyFilePaths)
        {
            if (ishalf)
                onFullFilePaths.Invoke();
            else
                onFrameTimeOnlyFilePaths.Invoke();
        }

        List<ToDataFilePaths> GetRefernceFilePaths(List<string> ftlist, List<string> rtlist)
        {
            List<ToDataFilePaths> res = new();

            for (int i = 0; i < ftlist.Count; ++i)
                res.Add(new ToDataFilePaths() { FrameTimes = ftlist[i], MethodRunTimesPerFrame = rtlist[i] });

            return res;
        }

        List<ToDataFilePaths> GetRefernceFilePathsFrameTimesOnly(List<string> ftlist)
        {
            List<ToDataFilePaths> res = new();

            foreach (var ft in ftlist)
                res.Add(new ToDataFilePaths() { FrameTimes = ft, MethodRunTimesPerFrame = "" });

            return res;
        }

        void CreateFullOrFrameFrameTimeOnlyRefPaths(OnFullFilePaths onFullFilePaths, OnFrameTimeOnlyFilePaths onFrameTimeOnlyFilePaths, List<string> ftlist, List<string> rtlist)
        {
            if (rtlist.Count > 0 && ftlist.Count == rtlist.Count)
                onFullFilePaths.Invoke();
            else
                onFrameTimeOnlyFilePaths.Invoke();
        }

        public ParseCommandData ParseCLIArgs(List<string> args)
        {
            if (args.Count <= 1)
            {
                onOutput.Invoke("Wrong Input!");
                return new ParseCommandData();
            }
            var parsed = new ParseCommandData() { DestinationPath = args[1], ReferenceFilePaths=new List<ToDataFilePaths>()};

            int refStart = args.FindIndex((s) => s == ARG_REFERENCE_SHORT || s == ARG_REFERENCE_LONG);
            int latStart = args.FindIndex((s) => s == ARG_LATEST_SHORT || s == ARG_LATEST_LONG);
            List<string> refFiles;
            List<string> latFiles;
            ( refFiles, latFiles ) = SeperateFileTypes(refStart < latStart, refStart, latStart, args); //init still preview hence the three lines instead of one
            CreateFullOrFrameTimeOnlyFilePaths(
                latFiles.Count >= 2,
                () => parsed.LatestFilePaths = CreateFullLatestFilePaths(latFiles), 
                () => parsed.LatestFilePaths = CreateFTOnlyLatestFilePaths(latFiles)
            );
            var ftlist = refFiles.FindAll(s => s.Contains(MARK_FRAMETIME));
            var rtlist = refFiles.FindAll(s => s.Contains(MARK_RUNTIME));
            CreateFullOrFrameFrameTimeOnlyRefPaths(
                () => parsed.ReferenceFilePaths.AddRange(GetRefernceFilePaths(ftlist, rtlist)),
                () => parsed.ReferenceFilePaths.AddRange(GetRefernceFilePathsFrameTimesOnly(ftlist)),
                ftlist,
                rtlist
                );

            return parsed;
        }
    }
}
