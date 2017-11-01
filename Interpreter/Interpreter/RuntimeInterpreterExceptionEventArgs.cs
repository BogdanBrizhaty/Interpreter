using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interpreter
{

    /// <summary>
    /// Used for detailed info. In case of demo interpreter, fust inherit LogEventArgs
    /// </summary>
    public class RuntimeInterpreterExceptionEventArgs : LogEventArgs
    {
        public RuntimeInterpreterExceptionEventArgs(string msg) : base(msg)
        {
        }
    }
}
