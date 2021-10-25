using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegressionCheckerLogic
{
    public class DataConverter : IDataConverter
    {
        public LineChartSeriesData ConvertCSVFileToLineChartSeriesData(CSVFile file, string name)
        {
            LineChartSeriesData lineChartSeriesData = new LineChartSeriesData() { Name = name };

            //TODO values

            return lineChartSeriesData;
        }

        public PieChartSeriesData ConvertCSVFileToPieChartSeriesData(CSVFile file, int framenumber)
        {
            throw new NotImplementedException();
        }
    }
}
