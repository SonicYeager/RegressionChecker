using System;
using System.Collections.Generic;

namespace regressionevallogic
{
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