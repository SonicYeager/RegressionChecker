using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace tracelogparserlogic
{
    //FrameFilter                                  (?<Frame>\d\d:\d\d:\d\d)
    //[INFO]\t[VideoEngine]Filter                  (?<IsDataLine>([[]{1}INFO[]]{1}\s+[[]{1}VideoEngine[]]{1}))
    //[INFO]\t[PerformanceMeasurement]Filter
    //DurationFilter                               (?<Duration>(\d+.\d+$))

    //fetch data block                             (?<IsDataLine>([[]{1}INFO[]]{1}\s+[[]{1}VideoEngine[]]{1})?(\d\d:\d\d:\d\d)\s(\d+.\d+$))
    //sperate data block                           (?<Frame>\d\d:\d\d:\d\d)|(?<Duration>(\d+.\d+$))
    //parse framecount                             (?<Minute>\d\d):(?<Second>\d\d):(?<Frames>\d\d)

    public class TraceLogParser : ITraceLogParser
    {
        public event OnParsed onParsed;

        bool IsStartFrameTimeLine(string line)
        {
            Regex regex = new("(?<IsStart>[[]{1}INFO[]]{1}\\s+[[]{1}PerformanceMeasurement[]]{1}:\\sStart\\s\\w*\\s[[]\\d[]]\\s[|]\\sFrame:)");
            Match match = regex.Match(line);
            return match.Success;
        }

        bool IsStopFrameTimeLine(string line)
        {
            Regex regex = new("(?<IsStop>[[]{1}INFO[]]{1}\\s+[[]{1}PerformanceMeasurement[]]{1}:\\sStop\\s\\w*\\s[[]\\d[]]->\\s\\w*\\s\\d.\\d*\\sms\\s[|]\\sFrame:)");
            Match match = regex.Match(line);
            return match.Success;
        }

        bool IsStartMethodLine(string line)
        {
            Regex regex = new("(?<IsStart>[[]{1}INFO[]]{1}\\s+[[]{1}PerformanceMeasurement[]]{1}:\\sStart\\s\\w*\\s[[]\\d*[]])");
            Match match = regex.Match(line);
            return match.Success;
        }

        bool IsStopMethodTimeLine(string line)
        {
            Regex regex = new("(?<IsStart>[[]{1}INFO[]]{1}\\s+[[]{1}PerformanceMeasurement[]]{1}:\\sStop\\s\\w*\\s[[]\\d*[]])");
            Match match = regex.Match(line);
            return match.Success;
        }

        string GetFrameCount(string line)
        {
            Regex regex = new("(?<Frame>\\d\\d:\\d\\d:\\d\\d$)");
            Match match = regex.Match(line);
            Group val;
            match.Groups.TryGetValue("Frame", out val);
            return val.Value;
        }

        string GetFrameTime(string line)
        {
            Regex regex = new("(?<FrameTime>(\\d+.\\d+(?=\\sms)))");
            Match match = regex.Match(line);
            Group val;
            match.Groups.TryGetValue("FrameTime", out val);
            return val.Value;
        }

        string GetDuration(string line)
        {
            Regex regex = new("(?<Duration>(\\d+.\\d+(?=\\sms)))");
            Match match = regex.Match(line);
            Group val;
            match.Groups.TryGetValue("Duration", out val);
            return val.Value;
        }

        string GetMethodName(string line)
        {
            Regex regex = new("(?<MethodName>(\\w*(?=\\s[[]\\d*[]])))"); //TODO
            Match match = regex.Match(line);
            Group val;
            match.Groups.TryGetValue("MethodName", out val);
            return val.Value;
        }

        public void ParseTraceLog(TraceLogFile traceLogFile, string dstPath)
        {
            List<List<string>> parsedFrameTime = new();
            List<List<string>> parsedRunTime = new();

            var newFTPath = Path.GetFullPath(dstPath) + Path.GetFileNameWithoutExtension(Path.GetFullPath(traceLogFile.FilePath)) + "_FT" + ".csv";
            var newRTPath = Path.GetFullPath(dstPath) + Path.GetFileNameWithoutExtension(Path.GetFullPath(traceLogFile.FilePath)) + "_RT" + ".csv";

            string frameCount = "";

            foreach (string line in traceLogFile.Lines)
            {
                List<string> frameTimeRes = new();
                List<string> runTimeRes = new();


                if (IsStartFrameTimeLine(line))
                    frameCount = GetFrameCount(line);
                else if(IsStopMethodTimeLine(line) && !IsStopFrameTimeLine(line) && frameCount != "")
                {
                    var name = GetMethodName(line);
                    var dur = GetDuration(line);

                    runTimeRes.Add(frameCount);
                    runTimeRes.Add(name);
                    runTimeRes.Add(dur);

                    parsedRunTime.Add(runTimeRes);
                }
                else if (IsStopFrameTimeLine(line))
                {
                    var dur = GetFrameTime(line);

                    frameTimeRes.Add(frameCount);
                    frameTimeRes.Add(dur);

                    parsedFrameTime.Add(frameTimeRes);

                    frameCount = "";
                }
            }

            onParsed.Invoke(new CSVFile() { Seperator = ';', FilePath = newFTPath, Headers = new List<string>() { GlobalConstants.FrameHeaderText, GlobalConstants.DurationHeaderText }, Elements = parsedFrameTime }); //frameTime
            if(parsedRunTime.Count > 0) 
                onParsed.Invoke(new CSVFile() { Seperator = ';', FilePath = newRTPath, Headers = new List<string>() { GlobalConstants.FrameHeaderText, GlobalConstants.MethodNameHeaderText, GlobalConstants.RunTimeHeaderText }, Elements = parsedRunTime }); //frameTime
        }
    }
}
