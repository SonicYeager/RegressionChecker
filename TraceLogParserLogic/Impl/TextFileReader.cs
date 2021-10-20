using System.Collections.Generic;
using System.IO;

namespace tracelogparserlogic
{
    public class TextFileReader : ITextFileReader
    {

        bool FileExists(string path)
        {
            return File.Exists(Path.GetFullPath(path));
        }

        bool IsLine(StreamReader reader, out string line)
        {
            return (line = reader.ReadLine()) != null;
        }

        public TraceLogFile ReadTextFile(string path)
        {
            List<string> readLines = new();
            TraceLogFile traceLogFile = new() { FilePath = Path.GetFullPath(path), Lines = readLines };

            if (FileExists(path))
            {
                using var reader = File.OpenText(Path.GetFullPath(path));
                string line = "";
                while (IsLine(reader, out line))
                    readLines.Add(line);
                traceLogFile.Lines = readLines;
            }

            return traceLogFile;
        }
    }
}
