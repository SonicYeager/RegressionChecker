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
        int _count = 0;

        public CSVFile GetAverageFrameTimes(List<CSVFile> frameTimes)
        {
            CSVFile averagedFrameTimes = new() { FilePath = "", Seperator = '\0', Elements=new List<List<string>>(), Headers=frameTimes[0].Headers };
            //check for lenght -> use smallest
            int smallestIndex = int.MaxValue;
            foreach (var frameTime in frameTimes)
                if (frameTime.Elements.Count < smallestIndex)
                    smallestIndex = frameTime.Elements.Count;

            //iterate through frametimes and get average
            List<double> averaged = new List<double>();
            for (int i = 0; i < smallestIndex; ++i)
            {
                double sum = 0;
                foreach (var frameTime in frameTimes)
                    sum += Convert.ToDouble(frameTime.Elements[i][1], new NumberFormatInfo() { NumberDecimalSeparator = "." });
                averaged.Add((sum / (double)frameTimes.Count));
            }

            for (int i = 0; i < smallestIndex; ++i)
                averagedFrameTimes.Elements.Add(new List<string>() { frameTimes[0].Elements[i][0], string.Format("{0:N4}",Convert.ToString(averaged[i], new NumberFormatInfo() { NumberDecimalSeparator = "." }))});

            return averagedFrameTimes;
            
        }

        double ConvertToDouble(string str)
        {
            return Convert.ToDouble(str, new NumberFormatInfo() { NumberDecimalSeparator = "." });
        }

        public CSVFile EvaluateRegression(ReferenceData refData, LatestData latestData, string path)
        {
            var newPath = Path.GetFullPath(path) + "RL_" + _count + ".csv";
            ++_count;
            CSVFile evaluated = new() { FilePath = newPath, Seperator = ';', Elements = new List<List<string>>(), Headers = new List<string>() { "Frame", "MethodName", "RunTime" } };

            var averaged = GetAverageFrameTimes(refData.FrameTimes);
            int smallestIndex = Math.Min(averaged.Elements.Count, latestData.FrameTimes.Elements.Count);
            for (int i = 0; i < smallestIndex; ++i)
            {
                //when frametime greater than ref -> Mark the frame index
                if (ConvertToDouble(averaged.Elements[i][1]) < ConvertToDouble(latestData.FrameTimes.Elements[i][1]))
                {
                    string frame = latestData.FrameTimes.Elements[i][0];
                    Predicate<List<string>> matchesGivenFrame = (line) => line[0] == frame;

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

                    evaluated.Elements.Add(new List<string>() {
                        latestData.FrameTimes.Elements[i][0],
                        maxRunTimeEntry[1],
                        maxRunTimeEntry[2],
                    });
                }
            }

            return evaluated;
        }
    }
}
