using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tracelogparserlogic
{
    public class CLIUI : ICLIUI
    {
        public void Print(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
