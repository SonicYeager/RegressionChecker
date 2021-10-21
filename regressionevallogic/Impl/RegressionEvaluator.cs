using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace regressionevallogic
{
    public class RegressionEvaluator : IRegressionEvaluator
    {
        private int _count = 0;
        private delegate void Action(int i);

        private double ConvertToDouble(string str)
        {
            return Convert.ToDouble(str, new NumberFormatInfo() { NumberDecimalSeparator = "." });
        }

        private void IterateTill(int index, Action action)
        {
            for (int i = 0; i < index; ++i)
                action.Invoke(i);
        }

        private static int GetSmallesIndex(List<CSVFile> frameTimes)
        {
            int smallestIndex = int.MaxValue;
            foreach (var frameTime in frameTimes)
                if (frameTime.Elements.Count < smallestIndex)
                    smallestIndex = frameTime.Elements.Count;
            return smallestIndex;
        }

        private List<double> GetAveragedFrameTimes(List<CSVFile> frameTimes, int smallestIndex)
        {
            List<double> averaged = new List<double>();
            for (int i = 0; i < smallestIndex; ++i)
            {
                double sum = 0;
                foreach (var frameTime in frameTimes)
                    sum += ConvertToDouble(frameTime.Elements[i][1]);
                averaged.Add((sum / (double)frameTimes.Count));
            }
            return averaged;
        }

        private static List<string> CreateAveragedEntry(List<CSVFile> frameTimes, List<double> averaged, int i)
        {
            return new List<string>() { frameTimes[0].Elements[i][0], string.Format("{0:N4}", Convert.ToString(averaged[i], new NumberFormatInfo() { NumberDecimalSeparator = "." })) };
        }

        public CSVFile GetAverageFrameTimes(List<CSVFile> frameTimes) //should be public part of the interface and externally called
        {
            CSVFile averagedFrameTimes = new() { FilePath = "", Seperator = '\0', Elements = new List<List<string>>(), Headers = frameTimes[0].Headers };
            int smallestIndex = GetSmallesIndex(frameTimes);
            List<double> averaged = GetAveragedFrameTimes(frameTimes, smallestIndex);
            IterateTill(smallestIndex, (int i) => averagedFrameTimes.Elements.Add(CreateAveragedEntry(frameTimes, averaged, i)));
            
            return averagedFrameTimes;

        }

        private List<string> SelectMaxRunTime(LatestData latestData, Predicate<List<string>> matchesGivenFrame)
        {

            //select highest runtime method!!
            var methodRuntimeList = latestData.MethodRunTimesPerFrame.Elements.FindAll(matchesGivenFrame);
            double maxRuntTime = 0;
            Predicate<List<string>> matchesGivenRunTime = (line) => ConvertToDouble(line[2]) == maxRuntTime;
            foreach (var entry in methodRuntimeList)
            {
                double val = ConvertToDouble(entry[2]);
                if (val > maxRuntTime)
                    maxRuntTime = val;
            }
            var maxRunTimeEntry = latestData.MethodRunTimesPerFrame.Elements.Find(matchesGivenRunTime);
            return maxRunTimeEntry;
        }

        private bool FrameTimeGreaterThanReferenceFrameTime(LatestData latestData, CSVFile averaged, int i)
        {
            return ConvertToDouble(averaged.Elements[i][1]) < ConvertToDouble(latestData.FrameTimes.Elements[i][1]);
        }

        private void AddRegressiveEntry(LatestData latestData, CSVFile evaluated, CSVFile averaged, int i)
        {
            if (FrameTimeGreaterThanReferenceFrameTime(latestData, averaged, i))
            {
                string frame = latestData.FrameTimes.Elements[i][0];
                Predicate<List<string>> matchesGivenFrame = (line) => line[0] == frame;
                List<string> maxRunTimeEntry = SelectMaxRunTime(latestData, matchesGivenFrame);
                evaluated.Elements.Add(new List<string>() {
                        latestData.FrameTimes.Elements[i][0],
                        maxRunTimeEntry[1],
                        maxRunTimeEntry[2],
                    });
            }
        }

        public CSVFile EvaluateRegression(ReferenceData refData, LatestData latestData, string path)
        {
            var newPath = Path.GetFullPath(path) + "RL_" + _count + ".csv";
            ++_count;
            CSVFile evaluated = new() { FilePath = newPath, Seperator = ';', Elements = new List<List<string>>(), Headers = new List<string>() { "Frame", "MethodName", "RunTime" } };

            var averaged = GetAverageFrameTimes(refData.FrameTimes);
            int smallestIndex = Math.Min(averaged.Elements.Count, latestData.FrameTimes.Elements.Count);
            IterateTill(smallestIndex, (int i) => AddRegressiveEntry(latestData, evaluated, averaged, i));

            return evaluated;
        }
    }
}
