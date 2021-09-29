using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tracelogparserlogic
{
    public class TraceLogParseController : ITraceLogParseController
    {
        public event OnOutput onOutput;

        public ITextFileReader TextFileReader { get; init; }
        public ICSVFileWriter CSVFileWriter { get; init; }
        public ITraceLogParser TraceLogParser { get; init; }

        public TraceLogParseController(ref ITextFileReader textFileReader, ref ICSVFileWriter csvFileWriter, ref ITraceLogParser traceLogParser)
        {
            TextFileReader = textFileReader;
            CSVFileWriter = csvFileWriter;
            TraceLogParser = traceLogParser;

            TraceLogParser.onParsed += (CSVFile file) => { CSVFileWriter.WriteCSVFile(file); };
        }

        public void ParseTraceLogToCSV(string dst, List<string> srcFiles)
        {
            foreach (var file in srcFiles)
            {
                TraceLogFile textFile = TextFileReader.ReadTextFile(file);
                TraceLogParser.ParseTraceLog(textFile, dst);
            }
        }
    }
}
