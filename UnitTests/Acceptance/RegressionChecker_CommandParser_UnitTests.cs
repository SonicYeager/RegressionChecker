using System;
using Xunit;
using RegressionCheckerLogic;
using System.Collections.Generic;

namespace UnitTests
{
    public class RegressionChecker_CommandParser_UnitTests
    {

        [Fact]
        public void ParseCommandArgs_ArgsWithDestinationAndOneSourceNoFlags_ReturnCommandDataWithOneEntry()
        {
            CommandParser parser = new();
            List<string> args = new()
            { 
                "tracelogparser.exe", 
                "D:\\", 
                "D:\\video_pro_x_662_original.log"
            };
            ParseCommandData expected = new() 
            { 
                DestinationPath = "D:\\",
                SourceFilePaths = new List<string>() 
                {
                    "D:\\video_pro_x_662_original.log" 
                },
                ReferenceFilePaths = new List<string>(),
                LatestFilePaths = "",
                NoGUI = false,
            };

            var actual = parser.ParseCommandArgs(args);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseCommandArgs_ArgsWithDestinationAndMultipleSourceNoFlags_ReturnCommandDataWithMultipleEntry()
        {
            CommandParser parser = new();
            List<string> args = new()
            {
                "tracelogparser.exe",
                "D:\\",
                "D:\\video_pro_x_662_original.log",
                "D:\\video_pro_x_663_original.log",
                "D:\\video_pro_x_664_original.log"
            };
            ParseCommandData expected = new()
            {
                DestinationPath = "D:\\",
                SourceFilePaths = new List<string>()
                {
                    "D:\\video_pro_x_662_original.log",
                    "D:\\video_pro_x_663_original.log",
                    "D:\\video_pro_x_664_original.log"
                },
                ReferenceFilePaths = new List<string>(),
                LatestFilePaths = "",
                NoGUI = false,
            };

            var actual = parser.ParseCommandArgs(args);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseCommandArgs_ArgsWitNoGUIFlag_ReturnCommandDataWithFlag()
        {
            CommandParser parser = new();
            List<string> args = new()
            {
                "tracelogparser.exe",
                "D:\\",
                "--nogui",
                "D:\\video_pro_x_662_original.log",
                "D:\\video_pro_x_663_original.log",
                "D:\\video_pro_x_664_original.log"
            };
            ParseCommandData expected = new()
            {
                DestinationPath = "D:\\",
                SourceFilePaths = new List<string>()
                {
                    "D:\\video_pro_x_662_original.log",
                    "D:\\video_pro_x_663_original.log",
                    "D:\\video_pro_x_664_original.log"
                },
                ReferenceFilePaths = new List<string>(),
                LatestFilePaths = "",
                NoGUI = true,
            };

            var actual = parser.ParseCommandArgs(args);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseCommandArgs_ArgsWitReferenceAndLatestFlag_ReturnCommandDataWithFlag()
        {
            CommandParser parser = new();
            List<string> args = new()
            {
                "tracelogparser.exe",
                "D:\\",
                "-l",
                "D:\\video_pro_x_662_original.log",
                "-r",
                "D:\\video_pro_x_663_original.log",
                "D:\\video_pro_x_664_original.log"
            };
            ParseCommandData expected = new()
            {
                DestinationPath = "D:\\",
                SourceFilePaths= new List<string>(),
                ReferenceFilePaths = new List<string>()
                {
                    "D:\\video_pro_x_663_original.log",
                    "D:\\video_pro_x_664_original.log",
                },
                LatestFilePaths = "D:\\video_pro_x_662_original.log",
                NoGUI = false,
            };

            var actual = parser.ParseCommandArgs(args);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseCommandArgs_ArgsWitReferenceAndLatestAndNoGUIFlag_ReturnCommandDataWithFlag()
        {
            CommandParser parser = new();
            List<string> args = new()
            {
                "tracelogparser.exe",
                "D:\\",
                "--nogui",
                "-l",
                "D:\\video_pro_x_662_original.log",
                "-r",
                "D:\\video_pro_x_663_original.log",
                "D:\\video_pro_x_664_original.log"
            };
            ParseCommandData expected = new()
            {
                DestinationPath = "D:\\",
                SourceFilePaths = new List<string>(),
                ReferenceFilePaths = new List<string>()
                {
                    "D:\\video_pro_x_663_original.log",
                    "D:\\video_pro_x_664_original.log",
                },
                LatestFilePaths = "D:\\video_pro_x_662_original.log",
                NoGUI = true,
            };

            var actual = parser.ParseCommandArgs(args);

            Assert.Equal(expected, actual);
        }
    }
}
