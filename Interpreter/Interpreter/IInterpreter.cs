using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interpreter
{
    public interface IInterpreter
    {
        object[] Interpretate(string src);
    }
}
