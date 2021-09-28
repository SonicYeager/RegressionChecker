using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace regressionevallogic
{
    public class CLIUI : ICLIUI
    {
        public void Print(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
