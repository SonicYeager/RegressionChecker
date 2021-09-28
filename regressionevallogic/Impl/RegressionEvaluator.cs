using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace regressionevallogic
{
    public class RegressionEvaluator : IRegressionEvaluator
    {
        public event OnRegressionEvaluation onRegressionEvaluation;

        public void EvaluateRegression(CSVFile file)
        {
            throw new NotImplementedException();
        }
    }
}
