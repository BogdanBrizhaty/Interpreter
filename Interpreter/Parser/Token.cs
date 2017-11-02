using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class Token
    {
        public enum TokenType
        {
            Var,
            Variable,
            Value,
            Assigning,
            In, // Include
            Exists,
            Count,
            Match,
            Min,
            Max,
            Indexes,
            Indexof,
            LastIndexof,
            Select,
            From,
            Writeline,
            Where,
            True,
            False,
            StartsWith,
            EndsWith,
            // order of sort
            //Asc,
            //Desc,
            
            
            // loop
            //Each,
            //EndEach,
            // end of instruction
            Semicolon
        }
        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }
        public TokenType Type { get; protected set; }
        public string Value { get; protected set; }
        public override string ToString()
        {
            return "{" + Type + ";" + Value + "}";
        }
        public static Token GetToken(string word)
        {

            if (word.ToUpper() == "TRUE" || word.ToUpper() == "FALSE")
                return new Token(TokenType.Value, word.ToUpper());

            var values = Enum.GetValues(typeof(TokenType)).Cast<TokenType>().ToList();
            var ttype = values.Where(v => v.ToString().ToUpper() == word.ToUpper()).FirstOrDefault();

            if (ttype.ToString().ToUpper() == word.ToUpper())
                return new Token(ttype, word.ToUpper());

            if (word == ";")
                return new Token(TokenType.Semicolon, ";");

            if (word == "=")
                return new Token(TokenType.Assigning, "=");

            double res = 0;

            if (double.TryParse(word, out res))
                return new Token(TokenType.Value, ((int)res).ToString());

            if (word.StartsWith("\"") && word.EndsWith("\""))
                return new Token(Token.TokenType.Value, word);

            return new Token(TokenType.Variable, word);
        }

        public override bool Equals(object obj)
        {
            return this.Type.CompareTo((obj as Token).Type) == 0 && this.Value.CompareTo((obj as Token).Value) == 0;
            //return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
