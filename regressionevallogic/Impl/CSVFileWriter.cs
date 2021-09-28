using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace regressionevallogic
{
    public class CSVFileWriter : ICSVFileWriter
    {
        public void WriteCSVFile(CSVFile file)
        {
            if (File.Exists(Path.GetFullPath(file.FilePath)))
                File.Delete(Path.GetFullPath(file.FilePath));
            using var writer = File.CreateText(Path.GetFullPath(file.FilePath));
            string headerLine = "";
            foreach (string header in file.Headers)
                headerLine += header + file.Seperator;
            headerLine.TrimEnd(file.Seperator);
            writer.WriteLine(headerLine);
            foreach (List<string> entry in file.Elements)
            {
                string line = "";
                foreach (string item in entry)
                    line += item + file.Seperator;
                line.TrimEnd(file.Seperator);
                writer.WriteLine(line);
            }
        }
    }
}
