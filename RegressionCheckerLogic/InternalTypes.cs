﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RegressionCheckerLogic
{
    public static class GLOBALS
    {
        public static readonly char CSV_SEPERATOR = ';';
    }

    public struct PathModel : IEquatable<PathModel>
    {
        public string Path { get; set; }

        public bool Equals(PathModel other)
        {
            return Path == other.Path;
        }

        public override bool Equals(object obj)
        {
            try
            {
                return Equals((PathModel)obj);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new HashCode();

            hashCode.Add(Path.GetHashCode());

            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            string str = "FilePath: " + Path;

            return str;
        }
    }

    public struct RegressiveMethodEntryModel : IEquatable<RegressiveMethodEntryModel>
    {
        public string MethodName { get; set; }
        public string FrameNumber { get; set; }
        public string Runtime { get; set; }

        public bool Equals(RegressiveMethodEntryModel other)
        {
            return MethodName == other.MethodName &&
                FrameNumber == other.FrameNumber &&
                Runtime == other.Runtime;
        }

        public override bool Equals(object obj)
        {
            try
            {
                return Equals((RegressiveMethodEntryModel)obj);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new HashCode();

            hashCode.Add(MethodName.GetHashCode());
            hashCode.Add(FrameNumber.GetHashCode());
            hashCode.Add(Runtime.GetHashCode());

            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            string str = "MethodName: " + MethodName +
                " | FrameNumber: " + FrameNumber +
                " | Runtime:" + Runtime;

            return str;
        }
    }

    public struct LineChartSeriesData : IEquatable<LineChartSeriesData>
    {
        public string Name { get; set; }
        public List<double> Values { get; set; }

        public bool Equals(LineChartSeriesData other)
        {
            return Name == other.Name &&
                Values.SequenceEqual(other.Values);
        }

        public override bool Equals(object obj)
        {
            try
            {
                return Equals((LineChartSeriesData)obj);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new HashCode();

            hashCode.Add(Name.GetHashCode());
            foreach (var item in Values)
                hashCode.Add(item.GetHashCode());

            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            string str = "Name: " + Name + " | Values: ";
            foreach (var item in Values)
                str += item + ", ";

            return str;
        }
    }

    public struct PieChartDataEntry : IEquatable<PieChartDataEntry>
    {
        public string EntryName { get; set; }
        public double EntryValue { get; set; }

        public bool Equals(PieChartDataEntry other)
        {
            return EntryName == other.EntryName &&
                EntryValue == other.EntryValue;
        }

        public override bool Equals(object obj)
        {
            try
            {
                return Equals((PieChartDataEntry)obj);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new HashCode();

            hashCode.Add(EntryName.GetHashCode());
            hashCode.Add(EntryValue.GetHashCode());

            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            string str = "EntryName: " + EntryName + " | EntryValue: " + Convert.ToString(EntryValue, new NumberFormatInfo() { NumberDecimalSeparator = "." });

            return str;
        }
    }

    public struct PieChartSeriesData : IEquatable<PieChartSeriesData>
    {
        public string Name { get; set; }
        public List<PieChartDataEntry> Values { get; set; }

        public bool Equals(PieChartSeriesData other)
        {
            return Name == other.Name &&
                Values.SequenceEqual(other.Values);
        }

        public override bool Equals(object obj)
        {
            try
            {
                return Equals((PieChartSeriesData)obj);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new HashCode();

            hashCode.Add(Name.GetHashCode());
            foreach (var item in Values)
                hashCode.Add(item.GetHashCode());

            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            string str = "Name: " + Name + " | Values: ";
            foreach (var item in Values)
                str += item.ToString();

            return str;
        }
    }

    public struct RegressiveMethodEntry : IEquatable<RegressiveMethodEntry>
    {
        public string MethodName { get; set; }
        public string FrameNumber { get; set; }
        public double Runtime { get; set; }

        public bool Equals(RegressiveMethodEntry other)
        {
            return MethodName == other.MethodName &&
                FrameNumber == other.FrameNumber &&
                Runtime == other.Runtime;
        }

        public override bool Equals(object obj)
        {
            try
            {
                return Equals((RegressiveMethodEntry)obj);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            HashCode hashCode = new HashCode();

            hashCode.Add(MethodName.GetHashCode());
            hashCode.Add(FrameNumber.GetHashCode());
            hashCode.Add(Runtime.GetHashCode());

            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            string str = "MethodName: " + MethodName +
                " | FrameNumber: " + FrameNumber +
                " | Runtime:" + Runtime.ToString();

            return str;
        }
    }

    public struct ParseCommandData : IEquatable<ParseCommandData>
    {
        public string DestinationPath { get; set; }
        public List<string> SourceFilePaths { get; set; }
        public List<string> ReferenceFilePaths { get; set; }
        public string LatestFilePaths { get; set; }
        public bool NoGUI { get; set; }

        public bool Equals(ParseCommandData other)
        {
            return DestinationPath == other.DestinationPath &&
                   NoGUI == other.NoGUI &&
                   ReferenceFilePaths.SequenceEqual(other.ReferenceFilePaths) &&
                   SourceFilePaths.SequenceEqual(other.SourceFilePaths) &&
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

            hashCode.Add(NoGUI.GetHashCode());
            hashCode.Add(DestinationPath.GetHashCode());
            hashCode.Add(LatestFilePaths.GetHashCode());
            foreach (var item in ReferenceFilePaths)
                hashCode.Add(item.GetHashCode());
            foreach (var item in SourceFilePaths)
                hashCode.Add(item.GetHashCode());

            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            string str = "DestinationPath: " + DestinationPath + " | LatestFilePaths: " + LatestFilePaths.ToString() + " | ReferenceFilePaths: ";
            foreach (var item in ReferenceFilePaths)
                str += item.ToString() + ", ";
            str += " | SourceFilePaths: ";
            foreach(var item in SourceFilePaths)
                str += item.ToString() + ", ";

            return str;
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
}