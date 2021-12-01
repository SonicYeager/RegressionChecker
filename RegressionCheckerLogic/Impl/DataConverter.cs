using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegressionCheckerLogic
{
    public class DataConverter : IDataConverter
    {
        private double ConvertToDouble(string str)
        {
            return Convert.ToDouble(str, new NumberFormatInfo() { NumberDecimalSeparator = "." });
        }

        public LineChartSeriesData ConvertCSVFileToLineChartSeriesData(CSVFile file, string name)
        {
            LineChartSeriesData lineChartSeriesData = new LineChartSeriesData() { Name = name };

            List<double> values = new();
            foreach (var val in file.Elements)
                values.Add(ConvertToDouble(val[1]));
            lineChartSeriesData.Values = values;

            return lineChartSeriesData;
        }

        public PieChartSeriesData ConvertCSVFileToPieChartSeriesData(CSVFile file, string framenumber)
        {
            PieChartSeriesData pieChartSeriesData = new PieChartSeriesData() { Name = framenumber };

            List<PieChartDataEntry> values = new();
            foreach (var val in file.Elements)
                if(val[0] == framenumber)
                    values.Add(new PieChartDataEntry() { EntryName = val[1], EntryValue = ConvertToDouble(val[2]) });
            pieChartSeriesData.Values = values;

            return pieChartSeriesData;
        }
    }
}
