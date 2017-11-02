using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interpreter
{
    public class Interpreter : IInterpreter
    {
        public object[] Interpretate(string src)
        {
            // parse
            var tokens = new Parser.Parser().Parse(src).ToList();
            // build instructions
            var root = InstructionTreeBuilder.Build(tokens);
            root.Execute();
            // execute
            return new object[1];
        }
    }
}
