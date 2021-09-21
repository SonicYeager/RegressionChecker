﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace tracelogparserlogic
{
    //FrameFilter (?<Frame>\d\d:\d\d:\d\d)
    //[INFO]\t[VideoEngine]Filter (?<IsDataLine>([[]{1}INFO[]]{1}\s+[[]{1}VideoEngine[]]{1}))
    //DurationFilter (?<Duration>(\d+.\d+$))

    //fetch data block (?<IsDataLine>([[]{1}INFO[]]{1}\s+[[]{1}VideoEngine[]]{1})?(\d\d:\d\d:\d\d)\s(\d+.\d+$))
    //sperate data block (?<Frame>\d\d:\d\d:\d\d)|(?<Duration>(\d+.\d+$))
    //parse framecount (?<Minute>\d\d):(?<Second>\d\d):(?<Frames>\d\d)
    public class TraceLogParser : ITraceLogParser
    {
        public CSVFile ParseTraceLog(TraceLogFile traceLogFile)
        {
            string fPath = Path.GetFullPath(traceLogFile.FilePath);
            List<List<string>> parsedElements = new();
                 
            foreach (string line in traceLogFile.Lines)
            {
                List<string> results = new();

                Regex regex = new ("(?<IsDataLine>([[]{1}INFO[]]{1}\\s+[[]{1}VideoEngine[]]{1})?(?<Data>\\d\\d:\\d\\d:\\d\\d)\\s(\\d+.\\d+$))");
                Match match = regex.Match(line);
                if (match.Success)
                {
                    string datablock = match.Value;
                    regex = new("(?<Frame>\\d\\d:\\d\\d:\\d\\d)|(?<Duration>(\\d+.\\d+$))");
                    match = regex.Match(datablock);
                    if (match.Success)
                    {
                        var groups = match.Groups;
                        Group group;
                        groups.TryGetValue("Frame", out group);
                        results.Add(group.Value);
                        groups.TryGetValue("Duration", out group);
                        results.Add(group.Value);
                        parsedElements.Add(results);
                    }
                }
            }

            return new CSVFile() { Seperator=";", FilePath="", Headers=new List<string>() { "Frame", "Duration" }, Elements= new List<List<string>>() { new List<string>() { } } };
        }
    }
}
