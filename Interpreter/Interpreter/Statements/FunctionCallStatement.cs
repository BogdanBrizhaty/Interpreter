using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Tables;

namespace Interpreter.Interpreter.Statements
{
    public class FunctionCallStatement : Statement
    {
        public FunctionCallStatement(IList<Token> tokens, MemoryTable table) : base(tokens, table)
        {
        }

        public override void Execute()
        {
            var type = Tokens.Last().Type;//.Value.StartsWith("\"") ? DataType.Text : DataType.Number;

            if (OpList.Operations.Keys.Contains(Tokens.First().Value))
                if (type == Token.TokenType.Value)
                    OpList.Operations[Tokens.First().Value](Tokens.Last().Value);
                else
                {
                    try
                    {
                        var variable = _memory.Lookup(Tokens.Last().Value);
                        OpList.Operations[Tokens.First().Value](variable.Value);
                    }
                    catch
                    {
                        OnExceptionThrown(
                            new RuntimeInterpreterExceptionEventArgs(
                                String.Format("Variable not found: {0}", Tokens.Last().Value)
                            )
                        );
                    }
                }
            else
                OnExceptionThrown(
                    new RuntimeInterpreterExceptionEventArgs(
                        String.Format("No built-in function with name {0} founded", Tokens.Last().Value)
                    )
                );

            base.Execute();
        }
    }
}
