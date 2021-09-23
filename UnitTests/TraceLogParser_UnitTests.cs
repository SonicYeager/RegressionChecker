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
            TraceLogFile file = new TraceLogFile() { Lines = new List<string>(){ "2021/06/22 23:28:19 [INFO]    [VideoEngine]: 00:00:00 1.0678" }, FilePath= "C:\\msvc\\TestLog001.txt" };
            CSVFile expected = new CSVFile() { Seperator = ';', FilePath="D:\\Temp\\TestLog001.csv", Headers = new List<string>(){ "Frame", "Duration" }, Elements = new List<List<string>>() { new List<string>() { "00:00:00", "1.0678" } } };

            var result = parser.ParseTraceLog(file, "D:\\Temp\\");

            Assert.True(result.Equals(expected));
        }

        [Fact]
        public void ParseTraceLog_GivenMultipleLineTraceLog_ReturnCSVFileWithMultipleEntries()
        {
            TraceLogParser parser = new();
            TraceLogFile file = new TraceLogFile() { Lines = new List<string>() { 
                "2021/06/22 23:28:19 [INFO]    [VideoEngine]: 00:00:00 1.0678",
                "2021/06/22 23:28:19 [INFO]    [VideoEngine]: 00:00:01 205.145" }, FilePath = "C:\\msvc\\TestLog001.txt"};
            CSVFile expected = new CSVFile() { Seperator = ';', FilePath = "D:\\Temp\\TestLog001.csv", Headers = new List<string>() { "Frame", "Duration" }, Elements = new List<List<string>>() { 
                new List<string>() { "00:00:00", "1.0678" },
                new List<string>() { "00:00:01", "205.145" }} };

            var result = parser.ParseTraceLog(file, "D:\\Temp\\");

            Assert.True(result.Equals(expected));
        }

        [Fact]
        public void ParseTraceLog_GivenMultipleLineWithOneFalseTraceLog_ReturnCSVFileWithMultipleEntries()
        {
            TraceLogParser parser = new();
            TraceLogFile file = new TraceLogFile()
            {
                Lines = new List<string>() {
                "2021/06/22 23:28:19 [INFO]    [VideoEngine]: 00:00:00 1.0678",
                "2021/06/22 23:27:58 [INFO]    [UserAction]: Start MAGIX Video Pro X(17.0.0.0)",
                "2021/06/22 23:28:19 [INFO]    [VideoEngine]: 00:00:01 205.145" },
                FilePath = "C:\\msvc\\TestLog001.txt"
            };
            CSVFile expected = new CSVFile()
            {
                Seperator = ';',
                FilePath = "D:\\Temp\\TestLog001.csv",
                Headers = new List<string>() { "Frame", "Duration" },
                Elements = new List<List<string>>() {
                new List<string>() { "00:00:00", "1.0678" },
                new List<string>() { "00:00:01", "205.145" }}
            };

            var result = parser.ParseTraceLog(file, "D:\\Temp\\");

            Assert.True(result.Equals(expected));
        }

        [Fact]
        public void ParseTraceLog_GivenCustomFilePath_ReturnCSVFileWithCorrectFilePathAndName()
        {
            TraceLogParser parser = new();
            TraceLogFile file = new TraceLogFile() { Lines = new List<string>() { "2021/06/22 23:28:19 [INFO]    [VideoEngine]: 00:00:00 1.0678" }, FilePath = "C:\\msvc\\TestLog001.txt" };

            var result = parser.ParseTraceLog(file, "D:\\Temp\\");

            Assert.Equal("D:\\Temp\\TestLog001.csv", result.FilePath);
        }
    }
}
