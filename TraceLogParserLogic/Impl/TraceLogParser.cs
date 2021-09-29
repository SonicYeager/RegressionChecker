using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

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

        bool HasFrameCount(string line)
        {
            Regex regex = new("(?<Frame>\\d\\d:\\d\\d:\\d\\d$)");
            Match match = regex.Match(line);
            return match.Success;
        }

        bool HasFrameTime(string line)
        {
            Regex regex = new("(?<Duration>(\\d+.\\d+(?=\\sms)))");
            Match match = regex.Match(line);
            return match.Success;
        }

        bool HasDuration(string line)
        {
            Regex regex = new("(?<Duration>(\\d+.\\d+(?=\\sms)))");
            Match match = regex.Match(line);
            return match.Success;
        }

        bool HasMethodName(string line)
        {
            Regex regex = new("(?<MethodName>(\\w*(?=\\s[[]\\d*[]])))"); //TODO
            Match match = regex.Match(line);
            return match.Success;
        }

        public void ParseTraceLog(TraceLogFile traceLogFile, string dstPath)
        {
            List<List<string>> parsedFrameTime = new();
            List<List<string>> parsedRunTime = new();

            var newFTPath = Path.GetFullPath(dstPath) + Path.GetFileNameWithoutExtension(Path.GetFullPath(traceLogFile.FilePath)) + "_FT" + ".csv";
            var newRTPath = Path.GetFullPath(dstPath) + Path.GetFileNameWithoutExtension(Path.GetFullPath(traceLogFile.FilePath)) + "_RT" + ".csv";

            foreach (string line in traceLogFile.Lines)
            {
                List<string> results = new();

                Regex regex = new("(?<IsStartFrameTimeLine>([[]{1}INFO[]]{1}\\s+[[]{1}PerformanceMeasurement[]]{1}:)?(?<Data>\\d\\d:\\d\\d:\\d\\d)\\s(\\d+.\\d+$))");
                Match match = regex.Match(line);
                if (match.Success)
                {
                    string datablock = match.Value;
                    regex = new("(?<Frame>\\d\\d:\\d\\d:\\d\\d)");
                    match = regex.Match(datablock);
                    if (match.Success)
                    {
                        var groups = match.Groups;
                        Group group;
                        groups.TryGetValue("Frame", out group);
                        results.Add(group.Value);
                    }
                    regex = new("(?<Duration>(\\d+.\\d+$))");
                    match = regex.Match(datablock);
                    if (match.Success)
                    {
                        var groups = match.Groups;
                        Group group;
                        groups.TryGetValue("Duration", out group);
                        results.Add(group.Value);
                    }
                    parsedFrameTime.Add(results);
                }
            }

            onParsed.Invoke(new CSVFile() { Seperator = ';', FilePath = newFTPath, Headers = new List<string>() { GlobalConstants.FrameHeaderText, GlobalConstants.DurationHeaderText }, Elements = parsedFrameTime }); //frameTime
            onParsed.Invoke(new CSVFile() { Seperator = ';', FilePath = newRTPath, Headers = new List<string>() { GlobalConstants.FrameHeaderText, GlobalConstants.MethodNameHeaderText, GlobalConstants.RunTimeHeaderText }, Elements = parsedRunTime }); //frameTime
        }
    }
}
