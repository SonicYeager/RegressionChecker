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
            List<string> args = new() { "" };
            ParseCommandData expected = new() { DestinationPath = "", SourceFilePaths = new List<string>() { "" } };

            var actual = parser.ParseCLIArgs(args);
        }
    }
}
