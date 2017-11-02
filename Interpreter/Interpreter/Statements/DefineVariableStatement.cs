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
            var varname = Tokens[1].Value;
            var varvalue = Tokens[Tokens.Count - 2];

            if (varvalue.Value.StartsWith("\""))
                _memory.Define(new Variable(DataType.Text, varname, varvalue.Value.Substring(1, varvalue.Value.Length - 2)));
            else
            {
                if (varvalue.Value == ("true").ToUpper() || varvalue.Value == ("false").ToUpper())
                    _memory.Define(new Variable(DataType.Boolean, varname, varvalue.Value));
                else
                    _memory.Define(new Variable(DataType.Number, varname, varvalue.Value));
            }


                base.Execute();
        }
    }
}
