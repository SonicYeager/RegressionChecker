using System;
using System.Collections.Generic;
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

        public void EvaluateForRegression(string dst, List<string> srcFiles)
        {
            throw new NotImplementedException();
        }
    }
}
