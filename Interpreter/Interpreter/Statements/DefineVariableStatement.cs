using Interpreter.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interpreter.Statements
{
    public class DefineVariableStatement : Statement
    {
        public DefineVariableStatement(IList<Token> tokens, MemoryTable table) : base(tokens, table)
        {
        }
        public override void Execute()
        {
            var varname = Tokens.First().Value;
            var varvalue = Tokens.Last();
            var type = varvalue.Value.StartsWith("\"") ? DataType.Text : DataType.Number;
            _memory.Define(new Variable(type, varname, varvalue.Value));

            base.Execute();
        }
    }
}
