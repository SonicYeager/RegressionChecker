using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace tracelogparserlogic
{
    public class TraceLogParser : ITraceLogParser
    {
        public event OnParsed onParsed;

        private delegate void OnStartFrameTime();
        private delegate void OnStopFrameTime();
        private delegate void OnMethodTime();

        ///{ CONSTANTS 
        ///REGEXGROUPNAME
        private static readonly string GRN_FRAME = "Frame";
        private static readonly string GRN_FRAMETIME = "FrameTime";
        private static readonly string GRN_DURATION = "Duration";
        private static readonly string GRN_METHODNAME = "MethodName";
        ///REGEX
        private static readonly string STARTFRAMETIME = "(?<IsStart>[[]{1}INFO[]]{1}\\s+[[]{1}PerformanceMeasurement[]]{1}:\\sStart\\s\\w*\\s[[]\\d*[]]\\s[|]\\sFrame:)";
        private static readonly string STOPFRAMETIME = "(?<IsStop>[[]{1}INFO[]]{1}\\s+[[]{1}PerformanceMeasurement[]]{1}:\\sStop\\s\\w*\\s[[]\\d*[]]->\\s\\w*\\s\\d.\\d*\\sms\\s[|]\\sFrame:)";
        private static readonly string STARTMETHOD = "(?<IsStart>[[]{1}INFO[]]{1}\\s+[[]{1}PerformanceMeasurement[]]{1}:\\sStart\\s\\w*\\s[[]\\d*[]])";
        private static readonly string STOPMETHOD = "(?<IsStart>[[]{1}INFO[]]{1}\\s+[[]{1}PerformanceMeasurement[]]{1}:\\sStop\\s\\w*\\s[[]\\d*[]])";
        private static readonly string FRAMECOUNT = "(?<" + GRN_FRAME + ">\\d\\d:\\d\\d:\\d\\d$)";
        private static readonly string FRAMETIME = "(?<" + GRN_FRAMETIME + ">(\\d+.\\d+(?=\\sms))|(\\d+(?=\\sms)))";
        private static readonly string DURATION = "(?<" + GRN_DURATION + ">(\\d+.\\d+(?=\\sms))|(\\d+(?=\\sms)))";
        private static readonly string METHODNAME = "(?<" + GRN_METHODNAME + ">(\\w*(?=\\s[[]\\d*[]])))";
        ///}

        bool IsStartFrameTimeLine(string line)
        {
            Regex regex = new(STARTFRAMETIME);
            Match match = regex.Match(line);
            return match.Success;
        }

        bool IsStopFrameTimeLine(string line)
        {
            Regex regex = new(STOPFRAMETIME);
            Match match = regex.Match(line);
            return match.Success;
        }

        bool IsStartMethodLine(string line)
        {
            Regex regex = new(STARTMETHOD);
            Match match = regex.Match(line);
            return match.Success;
        }

        bool IsStopMethodTimeLine(string line)
        {
            Regex regex = new(STOPMETHOD);
            Match match = regex.Match(line);
            return match.Success;
        }

        string GetFrameCount(string line)
        {
            Regex regex = new(FRAMECOUNT);
            Match match = regex.Match(line);
            Group val;
            match.Groups.TryGetValue(GRN_FRAME, out val);
            return val.Value;
        }

        string GetFrameTime(string line)
        {
            Regex regex = new(FRAMETIME);
            Match match = regex.Match(line);
            Group val;
            match.Groups.TryGetValue(GRN_FRAMETIME, out val);
            return val.Value;
        }

        string GetDuration(string line)
        {
            Regex regex = new(DURATION);
            Match match = regex.Match(line);
            Group val;
            match.Groups.TryGetValue(GRN_DURATION, out val);
            return val.Value;
        }

        string GetMethodName(string line)
        {
            Regex regex = new(METHODNAME);
            Match match = regex.Match(line);
            Group val;
            match.Groups.TryGetValue(GRN_METHODNAME, out val);
            return val.Value;
        }

        string GenerateCSVPathWithMarker(string dstPath, string trclFilePath, string marker)
        {
            return Path.GetFullPath(dstPath) + "\\" + Path.GetFileNameWithoutExtension(Path.GetFullPath(trclFilePath)) + marker + ".csv";
        }
        void ExtractRunTime(string line, string frameCount, string name, string dur, ref List<string> runTimeRes, ref List<List<string>> parsedRunTime)
        {
            runTimeRes.Add(frameCount);
            runTimeRes.Add(name);
            runTimeRes.Add(dur);

            parsedRunTime.Add(runTimeRes);
        }
        void ExtractFrameTime(string line, ref string frameCount, ref List<string> frameTimeRes, ref List<List<string>> parsedFrameTime)
        {
            var dur = GetFrameTime(line);

            frameTimeRes.Add(frameCount);
            frameTimeRes.Add(dur);

            parsedFrameTime.Add(frameTimeRes);

            frameCount = "";
        }

        void DetermineLine(string line, ref string frameCount, OnStartFrameTime onStartFrameTime, OnStopFrameTime onStopFrameTime, OnMethodTime onMethodTime)
        {
            if (IsStartFrameTimeLine(line))
                onStartFrameTime.Invoke();
            else if (IsStopMethodTimeLine(line) && !IsStopFrameTimeLine(line) && frameCount != "")
                onMethodTime.Invoke();
            else if (IsStopFrameTimeLine(line))
                onStopFrameTime.Invoke();
        }

        public void ParseTraceLog(TraceLogFile traceLogFile, string dstPath)
        {
            List<List<string>> parsedFrameTime = new();
            List<List<string>> parsedRunTime = new();
            var newFTPath = GenerateCSVPathWithMarker(dstPath, traceLogFile.FilePath, "_FT");
            var newRTPath = GenerateCSVPathWithMarker(dstPath, traceLogFile.FilePath, "_RT");
            string frameCount = "";

            foreach (string line in traceLogFile.Lines)
            {
                List<string> frameTimeRes = new();
                List<string> runTimeRes = new();

                OnMethodTime onMethodTime = () =>
                {
                    var name = GetMethodName(line);
                    var dur = GetDuration(line);
                    ExtractRunTime(line, frameCount, name, dur, ref runTimeRes, ref parsedRunTime);
                };

                DetermineLine(
                    line, 
                    ref frameCount, 
                    () => frameCount = GetFrameCount(line), 
                    () => ExtractFrameTime(line, ref frameCount, ref frameTimeRes, ref parsedFrameTime),
                    onMethodTime
                );
            }

            onParsed.Invoke(new CSVFile() { Seperator = ';', FilePath = newFTPath, Headers = new List<string>() { GlobalConstants.FrameHeaderText, GlobalConstants.DurationHeaderText }, Elements = parsedFrameTime }); //frameTime
            if(parsedRunTime.Count > 0) 
                onParsed.Invoke(new CSVFile() { Seperator = ';', FilePath = newRTPath, Headers = new List<string>() { GlobalConstants.FrameHeaderText, GlobalConstants.MethodNameHeaderText, GlobalConstants.RunTimeHeaderText }, Elements = parsedRunTime }); //frameTime
        }
    }
}
