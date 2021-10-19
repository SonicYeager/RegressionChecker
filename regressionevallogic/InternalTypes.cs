using System;
using System.Collections.Generic;
using System.Linq;

namespace regressionevallogic
{
    public struct CSVFile : IEquatable<CSVFile>
    {
        public List<string> Headers { get; set; }
        public List<List<string>> Elements { get; set; }
        public char Seperator { get; set; }
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

        public override bool Equals(object obj)
        {
            return this.Equals((CSVFile)obj);
        }

        public override int GetHashCode()
        {
            int code = Seperator.GetHashCode() ^ FilePath.GetHashCode();
            foreach (var header in Headers)
                code ^= header.GetHashCode();
            foreach (var element in Elements)
                foreach (var item in element)
                    code ^= item.GetHashCode();
            return code;
        }

        public override string ToString()
        {
            string msg = "Headers: ";
            foreach (var header in Headers)
                msg += header + ", ";
            msg += "| Elements: ";
            foreach (var element in Elements)
                foreach (var item in element)
                    msg += item + ", ";
            msg += "| Seperator: " + Seperator + "| FilePath: " + FilePath;
            return msg;
        }
    }

    public struct ParseCommandData : IEquatable<ParseCommandData>
    {
        public string DestinationPath { get; set; }
        public List<ToDataFilePaths> ReferenceFilePaths { get; set; }
        public ToDataFilePaths LatestFilePaths { get; set; }

        public bool Equals(ParseCommandData other)
        {
            return DestinationPath == other.DestinationPath &&
                   ReferenceFilePaths.SequenceEqual(other.ReferenceFilePaths) &&
                   Equals(LatestFilePaths, other.LatestFilePaths);
        }
    }

    public struct ReferenceData : IEquatable<ReferenceData>
    {
        public List<CSVFile> FrameTimes { get; set; }

        public bool Equals(ReferenceData other)
        {
            return FrameTimes.SequenceEqual(other.FrameTimes);
        }
    }

    public struct LatestData : IEquatable<LatestData>
    {
        public CSVFile FrameTimes { get; set; }
        public CSVFile MethodRunTimesPerFrame { get; set; }

        public bool Equals(LatestData other)
        {
            return Equals(FrameTimes, other.FrameTimes) &&
                Equals(MethodRunTimesPerFrame, other.MethodRunTimesPerFrame);
        }
    }

    public struct ToDataFilePaths : IEquatable<ToDataFilePaths>
    {
        public string FrameTimes { get; set; }
        public string MethodRunTimesPerFrame { get; set; }

        public bool Equals(ToDataFilePaths other)
        {
            return Equals(FrameTimes, other.FrameTimes) &&
                Equals(MethodRunTimesPerFrame, other.MethodRunTimesPerFrame);
        }
    }
}