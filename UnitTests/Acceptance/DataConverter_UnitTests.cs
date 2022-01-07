using System;
using Xunit;
using RegressionCheckerLogic;
using System.Collections.Generic;

namespace UnitTests
{
    public class DataConverter_UnitTests
    {
        [Fact]
        public void ConvertCSVFileToLineChartSeriesData_MultipleEntriesAsReference_ReturnsLincChartSeriesData()
        {
            DataConverter converter = new();
            CSVFile input = new CSVFile()
            {
                Seperator = ';',
                FilePath = "C:\\Parsed_FT.csv",
                Headers = new List<string>() 
                {
                    "Frame",
                    "FrameTime"
                },
                Elements = new List<List<string>>() 
                {
                    new List<string>() 
                    {
                        "00:00:00",
                        "20.456"
                    },
                    new List<string>()
                    {
                        "00:00:01",
                        "23.3457"
                    }
                },
            };
            LineChartSeriesData expected = new LineChartSeriesData()
            {
                Name = "Reference",
                Values = new List<double>
                {
                    20.456,
                    23.3457
                }
            };

            var actual = converter.ConvertCSVFileToLineChartSeriesData(input, "Reference");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConvertCSVFileToLineChartSeriesData_OneEntryAsReference_ReturnsLincChartSeriesData()
        {
            DataConverter converter = new();
            CSVFile input = new CSVFile()
            {
                Seperator = ';',
                FilePath = "C:\\Parsed_FT.csv",
                Headers = new List<string>()
                {
                    "Frame",
                    "FrameTime"
                },
                Elements = new List<List<string>>()
                {
                    new List<string>()
                    {
                        "00:00:00",
                        "20.456"
                    },
                },
            };
            LineChartSeriesData expected = new LineChartSeriesData()
            {
                Name = "Reference",
                Values = new List<double>
                {
                    20.456,
                }
            };

            var actual = converter.ConvertCSVFileToLineChartSeriesData(input, "Reference");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConvertCSVFileToLineChartSeriesData_MultipleEntriesAsLatest_ReturnsLincChartSeriesData()
        {
            DataConverter converter = new();
            CSVFile input = new CSVFile()
            {
                Seperator = ';',
                FilePath = "C:\\Parsed_FT.csv",
                Headers = new List<string>()
                {
                    "Frame",
                    "FrameTime"
                },
                Elements = new List<List<string>>()
                {
                    new List<string>()
                    {
                        "00:00:00",
                        "20.456"
                    },
                    new List<string>()
                    {
                        "00:00:01",
                        "23.3457"
                    }
                },
            };
            LineChartSeriesData expected = new LineChartSeriesData()
            {
                Name = "Latest",
                Values = new List<double>
                {
                    20.456,
                    23.3457
                }
            };

            var actual = converter.ConvertCSVFileToLineChartSeriesData(input, "Latest");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConvertCSVFileToPieChartSeriesData_MultipleEntries_ReturnsLincChartSeriesData()
        {
            DataConverter converter = new();
            CSVFile input = new CSVFile()
            {
                Seperator = ';',
                FilePath = "C:\\Parsed_FT.csv",
                Headers = new List<string>()
                {
                    "Frame",
                    "MethodName",
                    "RunTime"
                },
                Elements = new List<List<string>>()
                {
                    new List<string>()
                    {
                        "00:00:00",
                        "GetVideoFrame",
                        "13.4780"
                    },
                    new List<string>()
                    {
                        "00:00:00",
                        "DrawBMP",
                        "2.5098"
                    },
                    new List<string>()
                    {
                        "00:00:01",
                        "DrawBMP",
                        "2.5098"
                    }
                },
            };
            PieChartSeriesData expected = new PieChartSeriesData()
            {
                Name = "00:00:00",
                Values = new List<PieChartDataEntry>()
                {
                    new PieChartDataEntry() { EntryName = "GetVideoFrame", EntryValue = 13.4780 },
                    new PieChartDataEntry() { EntryName = "DrawBMP", EntryValue = 2.5098 },
                }
            };

            var actual = converter.ConvertCSVFileToPieChartSeriesData(input, "00:00:00");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConvertCSVFileToRegressiveMethodEntries_MultipleEntries_ReturnsListOfRegressiveMethodEntry()
        {
            //public string MethodName { get; set; }
            //public int FrameNumber { get; set; }
            //public double Runtime { get; set; }

            DataConverter converter = new();
            CSVFile input = new CSVFile()
            {
                Seperator = ';',
                FilePath = "C:\\Parsed_FT.csv",
                Headers = new List<string>()
                {
                    "Frame",
                    "MethodName",
                    "RunTime"
                },
                Elements = new List<List<string>>()
                {
                    new List<string>()
                    {
                        "00:00:00",
                        "GetVideoFrame",
                        "13.4780"
                    },
                    new List<string>()
                    {
                        "00:00:00",
                        "DrawBMP",
                        "2.5098"
                    },
                    new List<string>()
                    {
                        "00:00:01",
                        "DrawBMP",
                        "2.5098"
                    }
                },
            };
            List<RegressiveMethodEntry> expected = new List<RegressiveMethodEntry>()
            {
                 new RegressiveMethodEntry()
                 { 
                     MethodName = "GetVideoFrame",
                     FrameNumber = "00:00:00",
                     Runtime = 13.4780
                 },
                 new RegressiveMethodEntry()
                 {
                     MethodName = "DrawBMP",
                     FrameNumber = "00:00:00",
                     Runtime = 2.5098
                 },
                 new RegressiveMethodEntry()
                 {
                     MethodName = "DrawBMP",
                     FrameNumber = "00:00:01",
                     Runtime = 2.5098
                 },
            };

            var actual = converter.ConvertCSVFileToRegressiveMethodEntries(input);

            Assert.Equal(expected, actual);
        }
    }
}
