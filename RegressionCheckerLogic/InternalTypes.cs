using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RegressionCheckerLogic
{
    public struct PathViewModel : IEquatable<PathViewModel>
    {
        public string Path { get; set; }

        public bool Equals(PathViewModel other)
        {
            return Path == other.Path;
        }

        public override bool Equals(object obj)
        {
            try
            {
                return Equals((PathViewModel)obj);
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

    public struct LineChartSeriesData : IEquatable<LineChartSeriesData>
    {
        public string Name { get; set; }
        public List<TimeSpan> Values { get; set; }

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
        public int FrameNumber { get; set; }
        public TimeSpan Runtime { get; set; }

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
                " | FrameNumber: " + Convert.ToString(FrameNumber) +
                " | Runtime:" + Runtime.ToString();

            return str;
        }
    }

    public struct ParseCommandData : IEquatable<ParseCommandData>
    {
        public bool NoGUI { get; set; }
        public string DestinationPath { get; set; }
        public List<string> SourceFilePaths { get; set; }

        public bool Equals(ParseCommandData other)
        {
            return NoGUI == other.NoGUI &&
                DestinationPath == other.DestinationPath &&
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

            hashCode.Add(NoGUI.GetHashCode());
            hashCode.Add(DestinationPath.GetHashCode());
            foreach (var item in SourceFilePaths)
                hashCode.Add(item.GetHashCode());

            return hashCode.ToHashCode();
        }

        public override string ToString()
        {
            string str = "NoGUI: " + NoGUI +
                " | DestinationPath: " + DestinationPath +
                " | SourceFilePaths:";
            foreach (var item in SourceFilePaths)
                str += SourceFilePaths + ", ";

            return str;
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