using System;
using Xunit;
using regressionevallogic;
using System.Collections.Generic;

namespace UnitTests
{
    public class RegressionEvalCommandParser_UnitTests
    {
        [Fact]
        public void ParseCLIArgs_ArgsWithDestinationAndOneSourceFrameTimeFlag_ReturnCSVFileWithOneEntry()
        {
            CommandParser parser = new();
            List<string> args = new() { "regressioneval.exe", "D:\\", "-r", "D:\\TestLog_FT.csv", "-l", "D:\\TestLog001_RT.csv" };
            ParseCommandData expected = new() { DestinationPath = "D:\\", ReferenceFilePaths = new List<string>() { "D:\\TestLog_FT.csv" }, LatestFilePaths = new List<string>() { "D:\\TestLog001_RT.csv" } };

            var actual = parser.ParseCLIArgs(args);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseCLIArgs_ArgsWithDestinationAndMultipleSource_ReturnCSVFileWithMultipleEntry()
        {
            CommandParser parser = new();
            List<string> args = new()
            {
                "regressioneval.exe",
                "D:\\",
                "-r",
                "D:\\video_pro_x_662_original.csv",
                "D:\\video_pro_x_663_original.csv",
                "-l",                         
                "D:\\video_pro_x_664_original.csv",
            };
            ParseCommandData expected = new()
            {
                DestinationPath = "D:\\",
                ReferenceFilePaths = new List<string>()
                {
                "D:\\video_pro_x_662_original.csv",
                "D:\\video_pro_x_663_original.csv",
                },
                LatestFilePaths = new List<string>()
                {
                "D:\\video_pro_x_664_original.csv",
                }
            };

            var actual = parser.ParseCLIArgs(args);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseCLIArgs_IncorrectArgsNoFlags_InvokeOnOutputEventWithErrMsg()
        {
            CommandParser parser = new();
            List<string> args = new()
            {
                "regressioneval.exe"
            };
            string errMsg = "";
            parser.onOutput += (string msg) => { errMsg = msg; };

            var actual = parser.ParseCLIArgs(args);

            Assert.Equal("Wrong Input!Try:\regressioneval.exe <destPath> -r <refPath> [refPath] -l <latestPath>", errMsg);
        }

        //with incorrect -> expect eventcall
    }
}
