using System;
using Xunit;
using tracelogparserlogic;
using System.Collections.Generic;

namespace UnitTests
{
    public class TraceLogParser_UnitTests
    {
        [Fact]
        public void ParseTraceLog_GivenOneLineTraceLog_ReturnCSVFileWithOneEntry()
        {
            TraceLogParser parser = new();
            TraceLogFile file = new TraceLogFile() { Lines = new List<string>(){ "2021/06/22 23:28:19 [INFO]    [VideoEngine]: 00:00:00 1.0678" }, FilePath="" };
            CSVFile expected = new CSVFile() { Seperator = ";", FilePath="", Headers = new List<string>(){ "Frame", "Duration" }, Elements = new List<List<string>>() { new List<string>() { "00:00:00", "1.0678" } } };

            var result = parser.ParseTraceLog(file);

            Assert.Equal(result, expected);
        }

        //multiline
        //not a correct line
        //large frame number
        //shorter ms count
    }
}
