using System;
using System.Collections.Generic;
using System.Linq;

namespace regressionevallogic
{

    public static class GLOBALS
    {
        public static readonly char CSV_SEPERATOR = ';';
    }

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
            try
            {
                return Equals((CSVFile)obj);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new HashCode();

            hashCode.Add(FilePath.GetHashCode());
            hashCode.Add(Seperator.GetHashCode());
            foreach (var item in Headers)
                hashCode.Add(item.GetHashCode());
            foreach (var entry in Elements)
                foreach (var item in entry)
                    hashCode.Add(item.GetHashCode());

            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            string str = "FilePath: " + FilePath + " | Seperator: " + Seperator + " | Headers: ";
            foreach (var item in Headers)
                str += item + ", ";
            str += " | Elements: ";
            foreach (var entry in Elements)
            {
                foreach (var item in entry)
                    str += item + ", ";
                str += "; ";
            }

            return str;
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

        public override bool Equals(object obj)
        {
            try
            {
                return Equals((ParseCommandData)obj);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new HashCode();

            hashCode.Add(DestinationPath.GetHashCode());
            hashCode.Add(LatestFilePaths.GetHashCode());
            foreach (var item in ReferenceFilePaths)
                hashCode.Add(item.GetHashCode());

            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            string str = "DestinationPath: " + DestinationPath + " | LatestFilePaths: " + LatestFilePaths.ToString() + " | ReferenceFilePaths: ";
            foreach (var item in ReferenceFilePaths)
                str += item.ToString() + ", ";

            return str;
        }
    }

    public struct ReferenceData : IEquatable<ReferenceData>
    {
        public List<CSVFile> FrameTimes { get; set; }

        public bool Equals(ReferenceData other)
        {
            return FrameTimes.SequenceEqual(other.FrameTimes);
        }

        public override bool Equals(object obj)
        {
            try
            {
                return Equals((LatestData)obj);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new HashCode();

            foreach (var item in FrameTimes)
                hashCode.Add(item.GetHashCode());

            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            string str = "FrameTimes: ";
            foreach (var item in FrameTimes)
                str += item.ToString();

            return str;
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

        public override bool Equals(object obj)
        {
            try
            {
                return Equals((LatestData)obj);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new HashCode();

            hashCode.Add(FrameTimes.GetHashCode());
            hashCode.Add(MethodRunTimesPerFrame.GetHashCode());

            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            string str = "FrameTimes: " + FrameTimes.ToString() + " | MethodRunTimesPerFrame: " + MethodRunTimesPerFrame.ToString();

            return str;
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

        public override bool Equals(object obj)
        {
            try
            {
                return Equals((ToDataFilePaths)obj);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new HashCode();

            hashCode.Add(FrameTimes.GetHashCode());
            hashCode.Add(MethodRunTimesPerFrame.GetHashCode());

            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            string str = "FrameTimes: " + FrameTimes + " | MethodRunTimesPerFrame: " + MethodRunTimesPerFrame;

            return str;
        }
    }
}