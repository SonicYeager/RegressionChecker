using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace regressionevallogic
{
    public class RegressionEvaluatorController : IRegressionEvaluationController
    {
        public ICSVFileReader CSVFileReader { get; set; }
        public ICSVFileWriter CSVFileWriter { get; set; }
        public IRegressionEvaluator RegressinEvaluator { get; set; }

        public RegressionEvaluatorController(ref ICSVFileReader fr, ref ICSVFileWriter fw, ref IRegressionEvaluator re)
        {
            CSVFileReader = fr;
            CSVFileWriter = fw;
            RegressinEvaluator = re;
        }

        public event OnOutput onOutput;

        public void EvaluateForRegression(string dst, List<string> refPaths, string latestPathsFT, string latestPathsRT)
        {
            ReferenceData refData = new ReferenceData() { FrameTimes = new List<CSVFile>() };
            LatestData latData = new LatestData();
            foreach (var path in refPaths)
                refData.FrameTimes.Add(CSVFileReader.ReadCSVFile(Path.GetFullPath(path)));
            latData.FrameTimes = CSVFileReader.ReadCSVFile(Path.GetFullPath(latestPathsFT));
            latData.MethodRunTimesPerFrame = CSVFileReader.ReadCSVFile(Path.GetFullPath(latestPathsRT));
            var evaluated = RegressinEvaluator.EvaluateRegression(refData, latData, dst);
            if (evaluated.Elements.Count > 0)
                CSVFileWriter.WriteCSVFile(evaluated);
        }
    }
}
