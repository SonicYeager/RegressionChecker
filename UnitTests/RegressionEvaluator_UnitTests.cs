using System;
using Xunit;
using regressionevallogic;
using System.Collections.Generic;

namespace UnitTests
{
    public class RegressionEvaluator_UnitTests
    {
        [Fact]
        public void EvaluateRegression_NoRegression_EmptyCSVFileWithCorrectPath()
        {
            RegressionEvaluator eval = new();
            CSVFile refFrameTimes = new CSVFile()
            {
                FilePath = "C:\\msvc\\TestLog001_FT.csv",
                Headers = new List<string>() { "Frame", "Duration" },
                Elements = new List<List<string>>() {
                    new List<string>() { "00:00:00", "12.1230" },
                }
            };
            CSVFile refRunTimes = new CSVFile()
            {
                FilePath = "C:\\msvc\\TestLog001_RT.csv",
                Headers = new List<string>() { "Frame", "MethodName", "RunTime" },
                Elements = new List<List<string>>() {
                    new List<string>() { "00:00:00", "RenderFrame", "10.1230" },
                    new List<string>() { "00:00:00", "GetEffectListFromPLE", "2.1230" },
                }
            };
            CSVFile frameTimes = new CSVFile()
            {
                FilePath = "C:\\msvc\\TestLog001_FT.csv",
                Headers = new List<string>() { "Frame", "Duration" },
                Elements = new List<List<string>>() {
                    new List<string>() { "00:00:00", "11.1230" },
                }
            };
            CSVFile runTimes = new CSVFile()
            {
                FilePath = "C:\\msvc\\TestLog001_RT.csv",
                Headers = new List<string>() { "Frame", "MethodName", "RunTime" },
                Elements = new List<List<string>>() {
                    new List<string>() { "00:00:00", "RenderFrame", "10.1230" },
                    new List<string>() { "00:00:00", "GetEffectListFromPLE", "1.1230" },
                }
            };
            CSVFile expected = new CSVFile()
            {
                FilePath = "D:\\Temp\\RL_0.csv",
                Headers = new List<string>() { "Frame", "MethodName", "RunTime" },
                Elements = new List<List<string>>() {
                }
            };

            var result = eval.EvaluateRegression(
                new ReferenceData() { FrameTimes=new List<CSVFile>() { refFrameTimes }, MethodRunTimesPerFrame=new List<CSVFile>() { refRunTimes } },
                new LatestData() { FrameTimes=frameTimes, MethodRunTimesPerFrame=runTimes },
                "D:\\Temp\\");
            
            Assert.Equal(expected, result);
        }

        [Fact]
        public void EvaluateRegression_SingleRegression_SingleEntrieCSVFile()
        {
            RegressionEvaluator eval = new();
            CSVFile refFrameTimes = new CSVFile()
            {
                FilePath = "C:\\msvc\\TestLog001_FT.csv",
                Headers = new List<string>() { "Frame", "Duration" },
                Elements = new List<List<string>>() {
                    new List<string>() { "00:00:00", "12.1230" },
                }
            };
            CSVFile refRunTimes = new CSVFile()
            {
                FilePath = "C:\\msvc\\TestLog001_RT.csv",
                Headers = new List<string>() { "Frame", "MethodName", "RunTime" },
                Elements = new List<List<string>>() {
                    new List<string>() { "00:00:00", "RenderFrame", "10.1230" },
                    new List<string>() { "00:00:00", "GetEffectListFromPLE", "2.1230" },
                }
            };
            CSVFile frameTimes = new CSVFile()
            {
                FilePath = "C:\\msvc\\TestLog001_FT.csv",
                Headers = new List<string>() { "Frame", "Duration" },
                Elements = new List<List<string>>() {
                    new List<string>() { "00:00:00", "14.1230" },
                }
            };
            CSVFile runTimes = new CSVFile()
            {
                FilePath = "C:\\msvc\\TestLog001_RT.csv",
                Headers = new List<string>() { "Frame", "MethodName", "RunTime" },
                Elements = new List<List<string>>() {
                    new List<string>() { "00:00:00", "RenderFrame", "13.1230" },
                    new List<string>() { "00:00:00", "GetEffectListFromPLE", "1.1230" },
                }
            };
            CSVFile expected = new CSVFile()
            {
                FilePath = "D:\\Temp\\RL_0.csv",
                Headers = new List<string>() { "Frame", "MethodName", "RunTime" },
                Elements = new List<List<string>>()
                {
                    new List<string>() { "00:00:00", "RenderFrame", "13.1230" },
                }
            };

            var result = eval.EvaluateRegression(
                new ReferenceData() { FrameTimes = new List<CSVFile>() { refFrameTimes }, MethodRunTimesPerFrame = new List<CSVFile>() { refRunTimes } },
                new LatestData() { FrameTimes = frameTimes, MethodRunTimesPerFrame = runTimes },
                "D:\\Temp\\");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void EvaluateRegression_NoRegressionOnMultipleEntries_EmptyCSVFileWithCorrectPath()
        {
            RegressionEvaluator eval = new();
            CSVFile refFrameTimes = new CSVFile()
            {
                FilePath = "C:\\msvc\\TestLog001_FT.csv",
                Headers = new List<string>() { "Frame", "Duration" },
                Elements = new List<List<string>>() {
                    new List<string>() { "00:00:00", "12.1230" },
                    new List<string>() { "00:00:01", "11.4500" },
                    new List<string>() { "00:00:02", "12.1205" },
                }
            };
            CSVFile refRunTimes = new CSVFile()
            {
                FilePath = "C:\\msvc\\TestLog001_RT.csv",
                Headers = new List<string>() { "Frame", "MethodName", "RunTime" },
                Elements = new List<List<string>>() {
                    new List<string>() { "00:00:00", "RenderFrame", "10.1230" },
                    new List<string>() { "00:00:00", "GetEffectListFromPLE", "2.1230" },
                    new List<string>() { "00:00:01", "RenderFrame", "9.1230" },
                    new List<string>() { "00:00:01", "GetEffectListFromPLE", "2.1230" },
                    new List<string>() { "00:00:02", "RenderFrame", "10.1230" },
                    new List<string>() { "00:00:02", "GetEffectListFromPLE", "2.0030" },
                }
            };
            CSVFile frameTimes = new CSVFile()
            {
                FilePath = "C:\\msvc\\TestLog001_FT.csv",
                Headers = new List<string>() { "Frame", "Duration" },
                Elements = new List<List<string>>() {
                    new List<string>() { "00:00:00", "10.1230" },
                    new List<string>() { "00:00:01", "9.1230" },
                    new List<string>() { "00:00:02", "10.0030" },
                }
            };
            CSVFile runTimes = new CSVFile()
            {
                FilePath = "C:\\msvc\\TestLog001_RT.csv",
                Headers = new List<string>() { "Frame", "MethodName", "RunTime" },
                Elements = new List<List<string>>() {
                    new List<string>() { "00:00:00", "RenderFrame", "10.1230" },
                    new List<string>() { "00:00:00", "GetEffectListFromPLE", "2.1230" },
                    new List<string>() { "00:00:01", "RenderFrame", "9.1230" },
                    new List<string>() { "00:00:01", "GetEffectListFromPLE", "2.1230" },
                    new List<string>() { "00:00:02", "RenderFrame", "10.1230" },
                    new List<string>() { "00:00:02", "GetEffectListFromPLE", "2.0030" },
                }
            };
            CSVFile expected = new CSVFile()
            {
                FilePath = "D:\\Temp\\RL_0.csv",
                Headers = new List<string>() { "Frame", "MethodName", "RunTime" },
                Elements = new List<List<string>>()
                {
                }
            };

            var result = eval.EvaluateRegression(
                new ReferenceData() { FrameTimes = new List<CSVFile>() { refFrameTimes }, MethodRunTimesPerFrame = new List<CSVFile>() { refRunTimes } },
                new LatestData() { FrameTimes = frameTimes, MethodRunTimesPerFrame = runTimes },
                "D:\\Temp\\");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void EvaluateRegression_RegressionMultipleEntries_CSVFile()
        {
            RegressionEvaluator eval = new();
            CSVFile refFrameTimes = new CSVFile()
            {
                FilePath = "C:\\msvc\\TestLog001_FT.csv",
                Headers = new List<string>() { "Frame", "Duration" },
                Elements = new List<List<string>>() {
                    new List<string>() { "00:00:00", "12.1230" },
                    new List<string>() { "00:00:01", "11.4500" },
                    new List<string>() { "00:00:02", "12.1205" },
                }
            };
            CSVFile refRunTimes = new CSVFile()
            {
                FilePath = "C:\\msvc\\TestLog001_RT.csv",
                Headers = new List<string>() { "Frame", "MethodName", "RunTime" },
                Elements = new List<List<string>>() {
                    new List<string>() { "00:00:00", "RenderFrame", "10.1230" },
                    new List<string>() { "00:00:00", "GetEffectListFromPLE", "2.1230" },
                    new List<string>() { "00:00:01", "RenderFrame", "9.1230" },
                    new List<string>() { "00:00:01", "GetEffectListFromPLE", "2.1230" },
                    new List<string>() { "00:00:02", "RenderFrame", "10.1230" },
                    new List<string>() { "00:00:02", "GetEffectListFromPLE", "2.0030" },
                }
            };
            CSVFile frameTimes = new CSVFile()
            {
                FilePath = "C:\\msvc\\TestLog001_FT.csv",
                Headers = new List<string>() { "Frame", "Duration" },
                Elements = new List<List<string>>() {
                    new List<string>() { "00:00:00", "13.1230" },
                    new List<string>() { "00:00:01", "9.1230" },
                    new List<string>() { "00:00:02", "13.0030" },
                }
            };
            CSVFile runTimes = new CSVFile()
            {
                FilePath = "C:\\msvc\\TestLog001_RT.csv",
                Headers = new List<string>() { "Frame", "MethodName", "RunTime" },
                Elements = new List<List<string>>() {
                    new List<string>() { "00:00:00", "RenderFrame", "12.1230" },
                    new List<string>() { "00:00:00", "GetEffectListFromPLE", "2.1230" },
                    new List<string>() { "00:00:01", "RenderFrame", "7.1230" },
                    new List<string>() { "00:00:01", "GetEffectListFromPLE", "2.1230" },
                    new List<string>() { "00:00:02", "RenderFrame", "12.1230" },
                    new List<string>() { "00:00:02", "GetEffectListFromPLE", "2.0030" },
                }
            };
            CSVFile expected = new CSVFile()
            {
                FilePath = "D:\\Temp\\RL_0.csv",
                Headers = new List<string>() { "Frame", "MethodName", "RunTime" },
                Elements = new List<List<string>>()
                {
                    new List<string>() { "00:00:00", "RenderFrame", "12.1230" },
                    new List<string>() { "00:00:02", "RenderFrame", "12.1230" },
                }
            };

            var result = eval.EvaluateRegression(
                new ReferenceData() { FrameTimes = new List<CSVFile>() { refFrameTimes }, MethodRunTimesPerFrame = new List<CSVFile>() { refRunTimes } },
                new LatestData() { FrameTimes = frameTimes, MethodRunTimesPerFrame = runTimes },
                "D:\\Temp\\");

            Assert.Equal(expected, result);
        }
    }
}
