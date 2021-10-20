using System;
using System.Collections.Generic;
using System.IO;
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
        public override bool Equals(object obj)
        {
            try
            {
                return Equals((TraceLogFile)obj);
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
            foreach (var item in Lines)
                hashCode.Add(item.GetHashCode());

            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            string res = "FilePath: " + FilePath + " | Lines: ";
            foreach (var line in Lines)
                res += line + ", ";
            return res;
        }
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
            catch(Exception)
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
            foreach(var entry in Elements)
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
        public List<string> SourceFilePaths { get; set; }

        public bool Equals(ParseCommandData other)
        {
            return DestinationPath == other.DestinationPath &&
                   SourceFilePaths.SequenceEqual(other.SourceFilePaths);
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
            foreach (var item in SourceFilePaths)
                hashCode.Add(item.GetHashCode());

            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            string str = "DestinationPath: " + DestinationPath + " | SourceFilePaths: ";
            foreach (var item in SourceFilePaths)
                str += item + ", ";

            return str;
        }
    }

    public readonly struct GlobalConstants
    {
        public static char CSVFileSpererator { get; } = ';';
        public static string FrameHeaderText { get; } = "Frame";
        public static string DurationHeaderText { get; } = "Duration";
        public static string MethodNameHeaderText { get; } = "MethodName";
        public static string RunTimeHeaderText { get; } = "RunTime";
    }

}