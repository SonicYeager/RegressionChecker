using System.Collections.Generic;
using System.IO;

namespace tracelogparserlogic
{
    public class CSVFileWriter : ICSVFileWriter
    {

        bool FileExists(string path)
        {
            return File.Exists(Path.GetFullPath(path));
        }

        string CreateHeaderLine(List<string> headers, char seperator)
        {
            string headerLine = "";
            foreach (string header in headers)
                headerLine += header + seperator;
            headerLine.TrimEnd(seperator);
            return headerLine;
        }

        string CreateContentLine(List<string> entryLine, char seperator)
        {
            string line = "";
            foreach (string item in entryLine)
                line += item + seperator;
            line.TrimEnd(seperator);
            return line;
        }

        List<string> CreateAllLines(List<List<string>> elements, char seperator)
        {
            List<string> lines = new();
            foreach (List<string> entry in elements)
                lines.Add(CreateContentLine(entry, seperator));
            return lines;
        }

        public void WriteCSVFile(CSVFile file)
        {
            if (FileExists(file.FilePath))
                File.Delete(Path.GetFullPath(file.FilePath));
            using var writer = File.CreateText(Path.GetFullPath(file.FilePath));

            string headerLine = CreateHeaderLine(file.Headers, file.Seperator);
            var allLines = CreateAllLines(file.Elements, file.Seperator);

            writer.WriteLine(headerLine);
            foreach (var line in allLines)
                writer.WriteLine(line);
        }
    }
}
