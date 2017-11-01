using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Parser
{
    public class Parser
    {
        public Parser()
        {
        }
        public IEnumerable<Token> Parse(string src)
        {
            int pointer = 0;
            var result = new List<Token>();
            src = src
                .Trim(new char[] { '\n', '\r', ' ' })
                .Replace(Environment.NewLine, "");

            while (pointer < src.Length)
            {
                while (src[pointer] == ' ')
                    pointer++;
                var t = GetNextToken(src, ref pointer);
                result.Add(t);
            }
            return result;
        }
        private Token GetNextToken(string src, ref int pointer)
        {
            var buffer = String.Empty;
            if (src[pointer] == '=' || src[pointer] == ';')
            {
                return Token.GetToken(src[pointer++].ToString());
                //pointer++;
            }
            //if (src[pointer] == '=')
            //{
            //    pointer++;
            //    return new Token(Token.TokenType.Assigning, "=");
            //}
            //if (src[pointer] == ';')
            //{
            //    pointer++;
            //    return new Token(Token.TokenType.Semicolon, ";");
            //}
            if (src[pointer] == '"')
            {
                buffer += "\"";
                pointer++;
                while (pointer < src.Length && src[pointer] != '"')
                    buffer += src[pointer++];
                pointer++;
                buffer += "\"";

                return new Token(Token.TokenType.Value, buffer);
            }
            while (pointer < src.Length && src[pointer] != ' ' && src[pointer] != ';' && src[pointer] != '=')
                buffer += src[pointer++];

            return Token.GetToken(buffer.ToUpper());
        }
    }
}
