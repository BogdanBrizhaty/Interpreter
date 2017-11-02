using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interpreter.Statements
{
    public delegate void ExceptionThrownEventHandler(RuntimeInterpreterExceptionEventArgs args);

    public interface IStatement
    {
        event ExceptionThrownEventHandler ExceptionThrown;
        IList<IStatement> Children { get; set; }
        void Execute();
    }
}
