using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter.Tables;

namespace Interpreter.Interpreter.Statements
{
    public class QueryStatement : Statement
    {
        public QueryStatement(IList<Token> tokens, MemoryTable table) : base(tokens, table)
        {
        }

        public override void Execute()
        {
            var type = Tokens.First().Type;

            if (type != Token.TokenType.Variable)
            {
                OnExceptionThrown(
                    new RuntimeInterpreterExceptionEventArgs(
                        "No query result assigning, query won't be executed"
                    )
                );
                base.Execute();
                return;
            }
            Variable variable = null;
            try
            {
                variable = _memory.Lookup(Tokens.First().Value);
                if (variable.Type == DataType.Text)
                {
                    OnExceptionThrown(
                            new RuntimeInterpreterExceptionEventArgs(
                              String.Format("{0} is invalid argument type: <{0}>", "text")
                            )
                        );
                    base.Execute();
                    return;
                }
            }
            catch
            {
            }

            //if ()
            var pointer = 2;
            string stWith = String.Empty,
                   endsWith = String.Empty,
                   matchSize = String.Empty,
                   source = String.Empty,
                   template = String.Empty;

            var enumenator = Tokens.GetEnumerator();
            // go to second token
            enumenator.MoveNext();
            enumenator.MoveNext();
            enumenator.MoveNext();
            // start looping
            while (enumenator.Current != null)
            {
                var tmp = "";
                if (enumenator.Current.Type == Token.TokenType.Value)
                {
                    if (!enumenator.Current.Value.StartsWith("\"") && !enumenator.Current.Value.EndsWith("\""))
                    {
                        OnExceptionThrown(
                                new RuntimeInterpreterExceptionEventArgs(
                                  String.Format("Invalid argument type: <{0}> exptected, <{1}> given", "text", "unknown")
                                )
                            );
                        break;
                    }
                    tmp = enumenator.Current.Value.Substring(1, enumenator.Current.Value.Length - 2);
                    if (pointer == 3)
                        template = tmp;
                    if (pointer == 5)
                        source = tmp;
                    if (pointer == 8)
                    {
                        if (Tokens[pointer - 1].Type == Token.TokenType.StartsWith)
                            stWith = tmp;
                        if (Tokens[pointer - 1].Type == Token.TokenType.EndsWith)
                            endsWith = tmp;
                    }
                    if (pointer == 10)
                        if (Tokens[pointer - 1].Type == Token.TokenType.EndsWith)
                            endsWith = tmp;
                }
                else
                {
                    if (enumenator.Current.Type == Token.TokenType.Variable)
                    {
                        try
                        {
                            var @var = _memory.Lookup(enumenator.Current.Value);
                            if (@var.Type != DataType.Text)
                            {
                                OnExceptionThrown(
                                        new RuntimeInterpreterExceptionEventArgs(
                                          String.Format("Invalid argument type: <{0}> exptected, <{1}> given", "text", @var.Type)
                                        )
                                    );
                                break;
                            }
                            tmp = (string)@var.Value;
                        }
                        catch
                        {
                            OnExceptionThrown(
                                new RuntimeInterpreterExceptionEventArgs(
                                    String.Format("Variable not found: {0}", Tokens.Last().Value)
                                )
                            );
                            break;
                        }
                        if (pointer == 3)
                            template = tmp;
                        if (pointer == 5)
                            source = tmp;
                        if (pointer == 8)
                        {
                            if (Tokens[pointer - 1].Type == Token.TokenType.StartsWith)
                                stWith = tmp;
                            if (Tokens[pointer - 1].Type == Token.TokenType.EndsWith)
                                endsWith = tmp;
                        }
                        if (pointer == 10)
                            if (Tokens[pointer - 1].Type == Token.TokenType.EndsWith)
                                endsWith = tmp;
                    }
                    else
                    {
                        if (enumenator.Current.Type == Token.TokenType.Max || enumenator.Current.Type == Token.TokenType.Min)
                        {
                            if (Tokens[pointer - 1].Type == Token.TokenType.Match)
                            {
                                matchSize = enumenator.Current.Value;
                                break;
                            }
                            else
                            {
                                OnExceptionThrown(
                                        new RuntimeInterpreterExceptionEventArgs(
                                          String.Format("Invalid syntax in {0}{1} at {2}",
                                          Environment.NewLine, 
                                          Tokens.ConvertIntoLine(), 
                                          enumenator.Current.Value)
                                        )
                                    );
                                break;
                            }
                        }
                    }

                }
                enumenator.MoveNext();
                pointer++;
            }

            var qparams = new QueryParams(template, source, stWith, endsWith, matchSize);

            var opcode = Tokens[2];

            if (!OpList.Operations.Keys.Contains(opcode.Value))
            {
                OnExceptionThrown(
                    new RuntimeInterpreterExceptionEventArgs(
                        String.Format("No operations with code {0} founded", opcode.Value)
                    )
                );
            }
            else
            {
                var result = OpList.Operations[opcode.Value](qparams);
                var rtype = result.GetType();

                if (variable == null)
                {
                    if (rtype == typeof(bool))
                        _memory.Define(new Variable(DataType.Boolean, Tokens.First().Value, result));

                    if (rtype == typeof(List<string>) || rtype == typeof(List<int>))
                        _memory.Define(new Variable(DataType.ResourceList, Tokens.First().Value, result));

                    if (rtype == typeof(int))
                        _memory.Define(new Variable(DataType.Number, Tokens.First().Value, result));
                }
                else
                {
                    if (rtype == typeof(bool) && variable.Type == DataType.Boolean)
                        variable.Value = result;
                    else
                    {
                        if ((rtype == typeof(List<string>) || rtype == typeof(List<int>)) && variable.Type == DataType.ResourceList)
                            variable.Value = result;
                        else
                        {
                            if (rtype == typeof(int) && variable.Type == DataType.Number)
                                variable.Value = result;
                            else
                            {
                                OnExceptionThrown(
                                    new RuntimeInterpreterExceptionEventArgs(
                                      String.Format("Operation {0} can not be applied between {0} and {1} types", rtype, variable.Type)
                                    )
                                );
                            }
                        }

                    }

                }

            }

            base.Execute();
        }
    }
    //    0       1      2       3      4    5      6           7       8      7          8          7        8          9         10       11    12        13
    // VARNAME    =   OPCODE TEMPLATE FROM SOURCE WHERE STARTSWITH [val/var]/ENDSWITH [val/var]/(STARTSWITH [val/var] + ENDSWITH [val/var]) MATCH MAX/MIN   ;
}
