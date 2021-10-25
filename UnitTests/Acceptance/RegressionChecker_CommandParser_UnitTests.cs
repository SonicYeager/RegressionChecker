using System;
using Xunit;
using RegressionCheckerLogic;
using System.Collections.Generic;

namespace UnitTests
{
    public class RegressionChecker_CommandParser_UnitTests
    {

        [Fact]
        public void ParseCLIArgs_ArgsWithDestinationAndOneSourceNoFlags_ReturnCommandDataWithOneEntry()
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
                NoGUI = false,
            };

            var actual = parser.ParseCommandArgs(args);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseCLIArgs_ArgsWithDestinationAndMultipleSourceNoFlags_ReturnCommandDataWithMultipleEntry()
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
                NoGUI = false,
            };

            var actual = parser.ParseCommandArgs(args);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseCLIArgs_ArgsWitNoGUIFlag_ReturnCommandDataWithFlag()
        {
            CommandParser parser = new();
            List<string> args = new()
            {
                "tracelogparser.exe",
                "--nogui",
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
                NoGUI = true,
            };

            var actual = parser.ParseCommandArgs(args);

            Assert.Equal(expected, actual);
        }
    }
}
