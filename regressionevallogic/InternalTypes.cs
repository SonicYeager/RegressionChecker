﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace regressionevallogic
{
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

    public struct ParseCommandData : IEquatable<ParseCommandData>
    {
        public string DestinationPath { get; set; }
        public List<string> SourceFilePaths { get; set; }

        public bool Equals(ParseCommandData other)
        {
            return DestinationPath == other.DestinationPath &&
                   SourceFilePaths.SequenceEqual(other.SourceFilePaths);
        }
    }
}