using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace regressionevallogic
{
    public class CSVFileReader : ICSVFileReader
    {
        public CSVFile ReadCSVFile(string path)
        {
            CSVFile file = new () { FilePath=Path.GetFullPath(path), Seperator=';', Elements=new List<List<string>>() };
            if (File.Exists(Path.GetFullPath(path)))
            {
                using var reader = File.OpenText(Path.GetFullPath(path));

                string headerLine = reader.ReadLine();

                List<string> headers = new(headerLine.Split(';', StringSplitOptions.RemoveEmptyEntries));
                file.Headers = headers;

                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    List<string> lineEntries = new(line.Split(';', StringSplitOptions.RemoveEmptyEntries));
                    file.Elements.Add(lineEntries);
                }
            }
            return file;
        }
    }
}
