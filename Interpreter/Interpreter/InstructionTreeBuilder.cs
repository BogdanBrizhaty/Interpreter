using Interpreter.Interpreter.Statements;
using Interpreter.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Interpreter
{
    public class InstructionTreeBuilder
    {
        //static 
        public static Statement Build(IList<Token> source)
        {
            var root = new RootStatement(MemoryTable.Instance);
            var pointer = 0;
            while (pointer < source.Count)
            {
                var tmp = new List<Token>();
                while (pointer < source.Count)
                {
                    tmp.Add(source[pointer]);
                    pointer++;
                    if (source[pointer].Type == Token.TokenType.Semicolon)
                    {
                        tmp.Add(source[pointer]);
                        pointer++;
                        break;
                    }
                }
                if (tmp.Count == 0 || tmp.FirstOrDefault().Type == Token.TokenType.Semicolon)
                    continue;
                if (tmp.FirstOrDefault().Type == Token.TokenType.Writeline)
                    root.Children.Add(new FunctionCallStatement(tmp, MemoryTable.Instance));
                if (tmp.FirstOrDefault().Type == Token.TokenType.Var && tmp.Count == 5)
                    root.Children.Add(new DefineVariableStatement(tmp, MemoryTable.Instance));
                if (tmp.Count > 5)
                    root.Children.Add(new QueryStatement(tmp, MemoryTable.Instance));
            }

            return root;
        }
    }
}
