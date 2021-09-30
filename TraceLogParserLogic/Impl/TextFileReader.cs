using System.Collections.Generic;
using System.IO;

namespace tracelogparserlogic
{
    public class TextFileReader : ITextFileReader
    {
        public TraceLogFile ReadTextFile(string path)
        {
            List<string> readLines = new();
            TraceLogFile traceLogFile = new() { FilePath = Path.GetFullPath(path), Lines = readLines };

            if (File.Exists(Path.GetFullPath(path)))
            {
                using var reader = File.OpenText(Path.GetFullPath(path));
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    readLines.Add(line);
                }
                traceLogFile.Lines = readLines;
            }

            return traceLogFile;
        }
    }
}
