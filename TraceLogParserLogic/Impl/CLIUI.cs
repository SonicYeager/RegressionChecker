using System;

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
