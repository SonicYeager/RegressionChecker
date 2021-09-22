using System;
using System.Collections.Generic;
using System.Linq;

namespace tracelogparserlogic
{
    public struct TraceLogFile : IEquatable<TraceLogFile>
    {
        public List<string> Lines { get; set; }
        public string FilePath { get; set; }

        public bool Equals(TraceLogFile other)
        {
            return Lines.SequenceEqual(other.Lines) &&
                   FilePath == other.FilePath;
        }
    }

    public struct CSVFile : IEquatable<CSVFile>
    {
        public List<string> Headers { get; set; }
        public List<List<string>> Elements { get; set; }
        public string Seperator { get; set; }
        public string FilePath { get; set; }

        public bool Equals(CSVFile other)
        {
            if (!Headers.SequenceEqual(other.Headers))
                return false;
            if (!(Elements.Count == other.Elements.Count))
                return false;
            for (int i = 0; i < Elements.Count; ++i)
            {
                if (!Elements[i].SequenceEqual(other.Elements[i]))
                    return false;
            }
            return Seperator == other.Seperator &&
                   FilePath == other.FilePath;
        }
    }

    public struct ParseCommandData
    {
        public string DestinationPath { get; set; }
        public List<string> SourceFilePaths { get; set; }
    }

}