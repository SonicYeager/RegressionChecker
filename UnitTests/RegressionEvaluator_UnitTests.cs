using System;
using Xunit;
using regressionevallogic;
using System.Collections.Generic;

namespace UnitTests
{
    public class RegressionEvaluator_UnitTests
    {
        [Fact]
        public void EvaluateRegression_In_Out()
        {
            //RegressionEvaluator parser = new();
            //CSVFile file = new CSVFile() { Elements = new List<string>() { "2021/06/22 23:28:19 [INFO]    [VideoEngine]: 00:00:00 1.0678" }, FilePath = "C:\\msvc\\TestLog001.txt" };
            //CSVFile expected = new CSVFile() { Seperator = ';', FilePath = "D:\\Temp\\TestLog001.csv", Headers = new List<string>() { "Frame", "Duration" }, Elements = new List<List<string>>() { new List<string>() { "00:00:00", "1.0678" } } };
            //
            //var result = parser.ParseTraceLog(file, "D:\\Temp\\");
            //
            //Assert.True(result.Equals(expected));
        }
    }
}
