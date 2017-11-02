using Interpreter.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class Output
    {
        public static void Log(LogEventArgs args)
        {
            Console.WriteLine(args.ToString());
        }
    }
}
