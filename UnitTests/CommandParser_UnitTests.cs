using System;
using Xunit;
using tracelogparserlogic;
using System.Collections.Generic;

namespace UnitTests
{
    public class CommandParser_UnitTests
    {
        [Fact]
        public void ParseCLIArgs_ArgsWithDestinationAndOneSourceNoFlags_ReturnCSVFileWithOneEntry()
        {
            CommandParser parser = new();
            List<string> args = new() { "tracelogparser.exe", "D:\\", "D:\\video_pro_x_662_original.log" };
            ParseCommandData expected = new() { DestinationPath = "D:\\", SourceFilePaths = new List<string>() { "D:\\video_pro_x_662_original.log" } };

            var actual = parser.ParseCLIArgs(args);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseCLIArgs_ArgsWithDestinationAndMultipleSourceNoFlags_ReturnCSVFileWithMultipleEntry()
        {
            CommandParser parser = new();
            List<string> args = new() { "tracelogparser.exe", "D:\\", 
                "D:\\video_pro_x_662_original.log", 
                "D:\\video_pro_x_663_original.log", 
                "D:\\video_pro_x_664_original.log" };
            ParseCommandData expected = new() { DestinationPath = "D:\\", SourceFilePaths = new List<string>() { 
                "D:\\video_pro_x_662_original.log",
                "D:\\video_pro_x_663_original.log",
                "D:\\video_pro_x_664_original.log"} };

            var actual = parser.ParseCLIArgs(args);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseCLIArgs_IncorrectArgsNoFlags_InvokeOnOutputEventWithErrMsg()
        {
            CommandParser parser = new();
            List<string> args = new()
            {
                "tracelogparser.exe"
            };
            string errMsg = "";
            parser.onOutput += (string msg) => { errMsg = msg; };

            var actual = parser.ParseCLIArgs(args);

            Assert.Equal("Wrong Input!Try:\ntracelosparser.exe <destPath> <srcPath> [srcPath]", errMsg);
        }

        //with incorrect -> expect eventcall
    }
}
