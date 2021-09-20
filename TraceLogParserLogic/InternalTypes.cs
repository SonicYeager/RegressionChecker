using System;
using System.Collections.Generic;

namespace tracelogparserlogic
{
    public struct TraceLogFile
    {
        public List<string> Lines { get; set; }
        public string FilePath { get; set; }
    }

    public struct CSVFile
    {
        public List<string> Headers { get; set; }
        public List<List<string>> Elements { get; set; }
        public string Seperator { get; set; }
        public string FilePath { get; set; }
    }

    public struct ParseCommandData
    {
        public string DestinationPath { get; set; }
        public List<string> SourceFilePaths { get; set; }
    }

}