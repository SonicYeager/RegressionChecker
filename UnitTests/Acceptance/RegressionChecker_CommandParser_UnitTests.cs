using System;
using Xunit;
using RegressionCheckerLogic;
using System.Collections.Generic;

namespace UnitTests
{
    public class RegressionChecker_CommandParser_UnitTests
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
