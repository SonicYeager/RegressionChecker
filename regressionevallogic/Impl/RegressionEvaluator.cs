using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace regressionevallogic
{
    public class RegressionEvaluator : IRegressionEvaluator
    {
        static int _count = 0;

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
                    sum += Convert.ToDouble(frameTime.Elements[i][1]);
                averaged.Add((sum / (double)frameTimes.Count));
            }

            for (int i = 0; i < smallestIndex; ++i)
                averagedFrameTimes.Elements.Add(new List<string>() { frameTimes[0].Elements[i][0], string.Format("{0:N4}",Convert.ToString(averaged[i]))});

            return averagedFrameTimes;
            
        }
        public CSVFile EvaluateRegression(ReferenceData refData, LatestData latestData, string path)
        {
            var newPath = Path.GetFullPath(path) + Path.GetFileNameWithoutExtension(Path.GetFullPath(latestData.FrameTimes.FilePath)) + "RL_" + _count + ".csv";
            ++_count;

            CSVFile evaluated = new() { FilePath = "", Seperator = '\0', Elements = new List<List<string>>(), Headers = new List<string>() { "Frame", "MethodName", "RunTime" } }; //objektname?
            //get mittelwert from refData
            var averaged = GetAverageFrameTimes(refData.FrameTimes);
            //check for lenght -> use smallest
            //iterate through frametimes
            //compare frametimes
            //when frametime greater than ref -> Mark the frame index
            //return this: compile list with frame index and longest runtime method within that frame
            return evaluated;
        }
    }
}
