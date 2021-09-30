using System;
using Xunit;
using regressionevallogic;
using System.Collections.Generic;

namespace UnitTests
{
    public class RegressionEvalCommandParser_UnitTests
    {

        [Fact]
        public void ParseCLIArgs_FullArgsWithOnlyFrameTimes_ReturnFullCommandData()
        {
            CommandParser parser = new();
            List<string> args = new()
            {
                "regressioneval.exe",
                "D:\\",
                "-r",
                "D:\\TestLog_FT.csv",
                "-l",
                "D:\\TestLog001_FT.csv",
            };
            ParseCommandData expected = new()
            {
                DestinationPath = "D:\\",
                ReferenceFilePaths = new()
                {
                    new ToDataFilePaths()
                    {
                        FrameTimes = "D:\\TestLog_FT.csv",
                        MethodRunTimesPerFrame = ""
                    },
                },
                LatestFilePaths = new ToDataFilePaths()
                {
                    FrameTimes = "D:\\TestLog001_FT.csv",
                    MethodRunTimesPerFrame = "",
                },
            };

            var actual = parser.ParseCLIArgs(args);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseCLIArgs_FullArgsWithShortFlags_ReturnFullCommandData()
        {
            CommandParser parser = new();
            List<string> args = new() 
            { 
                "regressioneval.exe", 
                "D:\\", 
                "-r", 
                "D:\\TestLog_FT.csv", 
                "D:\\TestLog_RT.csv", 
                "-l", 
                "D:\\TestLog001_FT.csv", 
                "D:\\TestLog001_RT.csv", 
            };
            ParseCommandData expected = new() 
            {
                DestinationPath = "D:\\",
                ReferenceFilePaths = new ()
                {
                    new ToDataFilePaths()
                    {
                        FrameTimes="D:\\TestLog_FT.csv",
                        MethodRunTimesPerFrame="D:\\TestLog_RT.csv"
                    },
                },
                LatestFilePaths = new ToDataFilePaths()
                { 
                    FrameTimes="D:\\TestLog001_FT.csv",
                    MethodRunTimesPerFrame ="D:\\TestLog001_RT.csv",
                }, 
            };

            var actual = parser.ParseCLIArgs(args);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseCLIArgs_FullArgsWithLongFlags_ReturnFullCommandData()
        {
            CommandParser parser = new();
            List<string> args = new()
            {
                "regressioneval.exe",
                "D:\\",
                "--reference",
                "D:\\TestLog_FT.csv",
                "D:\\TestLog_RT.csv",
                "--latest",
                "D:\\TestLog001_FT.csv",
                "D:\\TestLog001_RT.csv",
            };
            ParseCommandData expected = new()
            {
                DestinationPath = "D:\\",
                ReferenceFilePaths = new()
                {
                    new ToDataFilePaths()
                    {
                        FrameTimes = "D:\\TestLog_FT.csv",
                        MethodRunTimesPerFrame = "D:\\TestLog_RT.csv"
                    },
                },
                LatestFilePaths = new ToDataFilePaths()
                {
                    FrameTimes = "D:\\TestLog001_FT.csv",
                    MethodRunTimesPerFrame = "D:\\TestLog001_RT.csv",
                },
            };

            var actual = parser.ParseCLIArgs(args);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseCLIArgs_FullArgsWithMutlipleRefs_ReturnFullCommandDataWithMultipleRefs()
        {
            CommandParser parser = new();
            List<string> args = new()
            {
                "regressioneval.exe",
                "D:\\",
                "-r",
                "D:\\TestLog_FT.csv",
                "D:\\TestLog_RT.csv",
                "D:\\TestLog2_FT.csv",
                "D:\\TestLog2_RT.csv",
                "D:\\TestLog3_FT.csv",
                "D:\\TestLog3_RT.csv",
                "-l",
                "D:\\TestLog001_FT.csv",
                "D:\\TestLog001_RT.csv",
            };
            ParseCommandData expected = new()
            {
                DestinationPath = "D:\\",
                ReferenceFilePaths = new()
                {
                    new ToDataFilePaths()
                    {
                        FrameTimes = "D:\\TestLog_FT.csv",
                        MethodRunTimesPerFrame = "D:\\TestLog_RT.csv"
                    },
                    new ToDataFilePaths()
                    {
                        FrameTimes = "D:\\TestLog2_FT.csv",
                        MethodRunTimesPerFrame = "D:\\TestLog2_RT.csv"
                    },
                    new ToDataFilePaths()
                    {
                        FrameTimes = "D:\\TestLog3_FT.csv",
                        MethodRunTimesPerFrame = "D:\\TestLog3_RT.csv"
                    },
                },
                LatestFilePaths = new ToDataFilePaths()
                {
                    FrameTimes = "D:\\TestLog001_FT.csv",
                    MethodRunTimesPerFrame = "D:\\TestLog001_RT.csv",
                },
            };

            var actual = parser.ParseCLIArgs(args);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseCLIArgs_FullArgsWithMutlipleRefsSwitchedFlags_ReturnFullCommandDataWithMultipleRefs()
        {
            CommandParser parser = new();
            List<string> args = new()
            {
                "regressioneval.exe",
                "D:\\",
                "-l",
                "D:\\TestLog001_FT.csv",
                "D:\\TestLog001_RT.csv",
                "-r",
                "D:\\TestLog_FT.csv",
                "D:\\TestLog_RT.csv",
                "D:\\TestLog2_FT.csv",
                "D:\\TestLog2_RT.csv",
                "D:\\TestLog3_FT.csv",
                "D:\\TestLog3_RT.csv",
            };
            ParseCommandData expected = new()
            {
                DestinationPath = "D:\\",
                ReferenceFilePaths = new()
                {
                    new ToDataFilePaths()
                    {
                        FrameTimes = "D:\\TestLog_FT.csv",
                        MethodRunTimesPerFrame = "D:\\TestLog_RT.csv"
                    },
                    new ToDataFilePaths()
                    {
                        FrameTimes = "D:\\TestLog2_FT.csv",
                        MethodRunTimesPerFrame = "D:\\TestLog2_RT.csv"
                    },
                    new ToDataFilePaths()
                    {
                        FrameTimes = "D:\\TestLog3_FT.csv",
                        MethodRunTimesPerFrame = "D:\\TestLog3_RT.csv"
                    },
                },
                LatestFilePaths = new ToDataFilePaths()
                {
                    FrameTimes = "D:\\TestLog001_FT.csv",
                    MethodRunTimesPerFrame = "D:\\TestLog001_RT.csv",
                },
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

            Assert.Equal("Wrong Input!", errMsg);
        }
    }
}
