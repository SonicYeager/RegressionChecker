using System;
using Xunit;
using tracelogparserlogic;
using System.Collections.Generic;

namespace UnitTests
{
    public class TraceLogParser_UnitTests
    {
        [Fact]
        public void ParseTraceLog_FullTraceLogOneFrame_ReturnCSVFilesWithOneEntry()
        {
            TraceLogParser parser = new();
            int _callCount = 0;
            CSVFile actualFrameTime = new();
            CSVFile actualRunTime = new();
            parser.onParsed += (CSVFile file) => { if (_callCount == 0) actualFrameTime = file; else actualRunTime = file; };
            TraceLogFile file = new () 
            { 
                Lines = new ()
                {
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Start RenderFrame [1] | Frame: 00:00:00",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Start GetVideoFrameFromVIP [61]",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Stop GetVideoFrameFromVIP [61]-> needs 0.6304 ms",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Start PaintVideo [61]",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Stop PaintVideo [61]-> needs 0.6196 ms",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Stop RenderFrame [1]-> needs 1.3965 ms | Frame: 00:00:00",
                },
                FilePath= "C:\\msvc\\TestLog001.txt" 
            };
            CSVFile expectedFrameTime = new () 
            {
                Seperator = GlobalConstants.CSVFileSpererator,
                FilePath="D:\\Temp\\TestLog001_FT.csv", 
                Headers = new List<string>()
                {
                    GlobalConstants.FrameHeaderText, 
                    GlobalConstants.DurationHeaderText, 
                }, 
                Elements = new List<List<string>>()
                {
                    new List<string>() { "00:00:00", "1.3965" },
                } 
            };
            CSVFile expectedRunTime = new ()
            {
                Seperator = GlobalConstants.CSVFileSpererator,
                FilePath = "D:\\Temp\\TestLog001_RT.csv",
                Headers = new List<string>() 
                {
                    GlobalConstants.FrameHeaderText,
                    GlobalConstants.MethodNameHeaderText,
                    GlobalConstants.RunTimeHeaderText, 
                },
                Elements = new List<List<string>>()
                { 
                    new List<string>() { "00:00:00", "GetVideoFrameFromVIP", "0.6304" }, 
                    new List<string>() { "00:00:00", "PaintVideo", "0.6196" }, 
                }
            };

            parser.ParseTraceLog(file, "D:\\Temp\\");

            Assert.Equal(expectedFrameTime, actualFrameTime);
            Assert.Equal(expectedRunTime, actualRunTime);
        }

        [Fact]
        public void ParseTraceLog_FrameTimeOnlyTraceLogOneFrame_ReturnCSVFileWithOneEntry()
        {
            TraceLogParser parser = new();
            CSVFile actualFrameTime = new();
            parser.onParsed += (CSVFile file) => { actualFrameTime = file; };
            TraceLogFile file = new()
            {
                Lines = new()
                {
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Start RenderFrame [1] | Frame: 00:00:00",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Stop RenderFrame [1]-> needs 1.3965 ms | Frame: 00:00:00",
                },
                FilePath = "C:\\msvc\\TestLog001.txt"
            };
            CSVFile expectedFrameTime = new()
            {
                Seperator = GlobalConstants.CSVFileSpererator,
                FilePath = "D:\\Temp\\TestLog001_FT.csv",
                Headers = new List<string>()
                {
                    GlobalConstants.FrameHeaderText,
                    GlobalConstants.DurationHeaderText,
                },
                Elements = new List<List<string>>()
                {
                    new List<string>() { "00:00:00", "1.3965" },
                }
            };

            parser.ParseTraceLog(file, "D:\\Temp\\");

            Assert.Equal(expectedFrameTime, actualFrameTime);
        }

        [Fact]
        public void ParseTraceLog_FullTraceLogMultipleFrames_ReturnCSVFilesWithMultipleEntries()
        {
            TraceLogParser parser = new();
            int _callCount = 0;
            CSVFile actualFrameTime = new();
            CSVFile actualRunTime = new();
            parser.onParsed += (CSVFile file) => { if (_callCount == 0) actualFrameTime = file; else actualRunTime = file; };
            TraceLogFile file = new()
            {
                Lines = new()
                {
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Start RenderFrame [1] | Frame: 00:00:00",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Start GetVideoFrameFromVIP [61]",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Stop GetVideoFrameFromVIP [61]-> needs 0.6304 ms",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Start PaintVideo [61]",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Stop PaintVideo [61]-> needs 0.6196 ms",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Stop RenderFrame [1]-> needs 1.3965 ms | Frame: 00:00:00",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Start RenderFrame [1] | Frame: 00:00:01",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Start GetVideoFrameFromVIP [61]",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Stop GetVideoFrameFromVIP [61]-> needs 0.6304 ms",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Start PaintVideo [61]",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Stop PaintVideo [61]-> needs 0.6196 ms",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Stop RenderFrame [1]-> needs 1.3965 ms | Frame: 00:00:01",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Start RenderFrame [1] | Frame: 00:00:02",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Start GetVideoFrameFromVIP [61]",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Stop GetVideoFrameFromVIP [61]-> needs 0.6304 ms",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Start PaintVideo [61]",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Stop PaintVideo [61]-> needs 0.6196 ms",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Stop RenderFrame [1]-> needs 1.3965 ms | Frame: 00:00:02",
                },
                FilePath = "C:\\msvc\\TestLog001.txt"
            };
            CSVFile expectedFrameTime = new()
            {
                Seperator = GlobalConstants.CSVFileSpererator,
                FilePath = "D:\\Temp\\TestLog001_FT.csv",
                Headers = new List<string>()
                {
                    GlobalConstants.FrameHeaderText,
                    GlobalConstants.DurationHeaderText,
                },
                Elements = new List<List<string>>()
                {
                    new List<string>() { "00:00:00", "1.3965" },
                    new List<string>() { "00:00:01", "1.3965" },
                    new List<string>() { "00:00:02", "1.3965" },
                }
            };
            CSVFile expectedRunTime = new()
            {
                Seperator = GlobalConstants.CSVFileSpererator,
                FilePath = "D:\\Temp\\TestLog001_RT.csv",
                Headers = new List<string>()
                {
                    GlobalConstants.FrameHeaderText,
                    GlobalConstants.MethodNameHeaderText,
                    GlobalConstants.RunTimeHeaderText,
                },
                Elements = new List<List<string>>()
                {
                    new List<string>() { "00:00:00", "GetVideoFrameFromVIP", "0.6304" },
                    new List<string>() { "00:00:00", "PaintVideo", "0.6196" },
                    new List<string>() { "00:00:01", "GetVideoFrameFromVIP", "0.6304" },
                    new List<string>() { "00:00:01", "PaintVideo", "0.6196" },
                    new List<string>() { "00:00:02", "GetVideoFrameFromVIP", "0.6304" },
                    new List<string>() { "00:00:02", "PaintVideo", "0.6196" },
                }
            };

            parser.ParseTraceLog(file, "D:\\Temp\\");

            Assert.Equal(expectedFrameTime, actualFrameTime);
            Assert.Equal(expectedRunTime, actualRunTime);
        }

        [Fact]
        public void ParseTraceLog_FrameTimeOnlyTraceLogMutlipleFrames_ReturnCSVFileWithMultipleEntries()
        {
            TraceLogParser parser = new();
            CSVFile actualFrameTime = new();
            parser.onParsed += (CSVFile file) => { actualFrameTime = file; };
            TraceLogFile file = new()
            {
                Lines = new()
                {
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Start RenderFrame [1] | Frame: 00:00:00",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Stop RenderFrame [1]-> needs 1.3965 ms | Frame: 00:00:00",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Start RenderFrame [1] | Frame: 00:00:01",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Stop RenderFrame [1]-> needs 1.3965 ms | Frame: 00:00:01",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Start RenderFrame [1] | Frame: 00:00:02",
                    "2021/09/29 14:40:44 [INFO]    [PerformanceMeasurement]: Stop RenderFrame [1]-> needs 1.3965 ms | Frame: 00:00:02",
                },
                FilePath = "C:\\msvc\\TestLog001.txt"
            };
            CSVFile expectedFrameTime = new()
            {
                Seperator = GlobalConstants.CSVFileSpererator,
                FilePath = "D:\\Temp\\TestLog001_FT.csv",
                Headers = new List<string>()
                {
                    GlobalConstants.FrameHeaderText,
                    GlobalConstants.DurationHeaderText,
                },
                Elements = new List<List<string>>()
                {
                    new List<string>() { "00:00:00", "1.3965" },
                    new List<string>() { "00:00:01", "1.3965" },
                    new List<string>() { "00:00:02", "1.3965" },
                }
            };

            parser.ParseTraceLog(file, "D:\\Temp\\");

            Assert.Equal(expectedFrameTime, actualFrameTime);
        }
    }
}
