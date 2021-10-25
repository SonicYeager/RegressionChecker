using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegressionCheckerLogic
{
    public class CSVFileReader : ICSVFileReader
    {

        bool FileExists(string path)
        {
            return File.Exists(Path.GetFullPath(path));
        }
        bool IsEOF(StreamReader reader, out string line)
        {
            return (line = reader.ReadLine()) != null;
        }
        List<List<string>> ReadAllLines(StreamReader reader)
        {
            string line = "";
            List<List<string>> linesWithEntries = new List<List<string>>();
            while (IsEOF(reader, out line))
                linesWithEntries.Add(new(line.Split(';', StringSplitOptions.RemoveEmptyEntries)));
            return linesWithEntries;
        }

        public CSVFile ReadCSVFile(string path)
        {
            CSVFile file = new() { FilePath = Path.GetFullPath(path), Seperator = GLOBALS.CSV_SEPERATOR, Elements = new List<List<string>>() };
            if (FileExists(path))
            {
                using var reader = File.OpenText(Path.GetFullPath(path));

                List<string> headers = new(reader.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries));
                var linesWithEntries = ReadAllLines(reader);

                file.Headers = headers;
                file.Elements = linesWithEntries;

            }
            return file;
        }
    }
}
